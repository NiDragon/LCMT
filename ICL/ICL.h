// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the ICL_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// ICL_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef ICL_EXPORTS
#define ICL_API __declspec(dllexport)
#else
#define ICL_API __declspec(dllimport)
#endif

#include <stdint.h>
#include <string>

extern "C" ICL_API int BrowseFolder(wchar_t* result_path, wchar_t* saved_path);

extern "C" ICL_API bool Decrypt_Password(char* crypt, uint32_t AuthKey);

extern "C" ICL_API bool Encrypt_Password(char* crypt, uint32_t AuthKey);