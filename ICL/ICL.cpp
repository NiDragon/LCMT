// ICL.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "ICL.h"

#include <shlobj.h>
#include <Windows.h>

const char m_LookUp[] = { 'a', 'c', 'd', 'c', 'i', 'd', 'g', 'x', 'o', 'b', 'i', 'n', 't', 'g', 'c', };

const uint32_t InternalKey = 0x49434C30;

bool Decrypt_Password(char* crypt, uint32_t AuthKey) {
	if (AuthKey != InternalKey)
		return false;

	size_t len = strlen(crypt);

	for (unsigned int i = 0; i < len; i++)
	{
		crypt[i] = (crypt[i] - 12) ^ m_LookUp[i % sizeof(m_LookUp)];
	}

	return true;
}

bool Encrypt_Password(char* crypt, uint32_t AuthKey) {
	if (AuthKey != InternalKey)
		return false;

	size_t len = strlen(crypt);

	for (unsigned int i = 0; i < len; i++)
	{
		crypt[i] = (crypt[i] ^ m_LookUp[i % sizeof(m_LookUp)]) + 12;
	}

	return true;
}

static int CALLBACK BrowseCallbackProc(HWND hwnd, UINT uMsg, LPARAM lParam, LPARAM lpData)
{
	if (uMsg == BFFM_INITIALIZED)
	{
		std::string tmp = (const char *)lpData;
		SendMessage(hwnd, BFFM_SETSELECTION, TRUE, lpData);
	}

	return 0;
}

int BrowseFolder(wchar_t* result_path, wchar_t* saved_path)
{
	int result = 0;

	HRESULT hr;
	IFileOpenDialog *pOpenFolderDialog;

	CoInitializeEx(NULL, COINIT_APARTMENTTHREADED);

	// CoCreate the dialog object.
	hr = CoCreateInstance(CLSID_FileOpenDialog,
		NULL,
		CLSCTX_INPROC_SERVER,
		IID_PPV_ARGS(&pOpenFolderDialog));

	if (SUCCEEDED(hr))
	{
		pOpenFolderDialog->SetOptions(FOS_PICKFOLDERS);
		pOpenFolderDialog->SetTitle(L"Select Client Directory...");
		pOpenFolderDialog->SetOkButtonLabel(L"Select");

		// Show the dialog
		hr = pOpenFolderDialog->Show(NULL);

		if (SUCCEEDED(hr))
		{
			// Obtain the result of the user's interaction with the dialog.
			IShellItem *psiResult;
			hr = pOpenFolderDialog->GetResult(&psiResult);

			if (SUCCEEDED(hr))
			{
				// Do something with the result.
				LPWSTR pwsz = NULL;

				hr = psiResult->GetDisplayName(SIGDN_FILESYSPATH, &pwsz);

				if (SUCCEEDED(hr)) 
				{
					std::wstring sp = pwsz;

					memset(result_path, 0, MAX_PATH);

					wcscpy_s(result_path, MAX_PATH, pwsz);

					::CoTaskMemFree(pwsz);
				}
				else
				{
					result = 4;
				}

				psiResult->Release();
			}
			else
			{
				result = 3;
			}
		}
		else 
		{
			result = 2;
		}

		pOpenFolderDialog->Release();
	}
	else
	{
		result = 1;
	}

	// Sucesss
	return result;
}