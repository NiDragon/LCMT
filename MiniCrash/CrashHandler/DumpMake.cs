using System;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Security.Cryptography;
using System.IO.Compression;

namespace MiniCrash.CrashHandler
{
    internal class ProcessDumper
    {
        internal enum MINIDUMP_TYPE
        {
            MiniDumpNormal = 0x00000000,
            MiniDumpWithDataSegs = 0x00000001,
            MiniDumpWithFullMemory = 0x00000002,
            MiniDumpWithHandleData = 0x00000004,
            MiniDumpFilterMemory = 0x00000008,
            MiniDumpScanMemory = 0x00000010,
            MiniDumpWithUnloadedModules = 0x00000020,
            MiniDumpWithIndirectlyReferencedMemory = 0x00000040,
            MiniDumpFilterModulePaths = 0x00000080,
            MiniDumpWithProcessThreadData = 0x00000100,
            MiniDumpWithPrivateReadWriteMemory = 0x00000200,
            MiniDumpWithoutOptionalData = 0x00000400,
            MiniDumpWithFullMemoryInfo = 0x00000800,
            MiniDumpWithThreadInfo = 0x00001000,
            MiniDumpWithCodeSegs = 0x00002000
        }

        [DllImport("dbghelp.dll")]
        static extern bool MiniDumpWriteDump(
            IntPtr hProcess,
            Int32 ProcessId,
            IntPtr hFile,
            MINIDUMP_TYPE DumpType,
            IntPtr ExceptionParam,
            IntPtr UserStreamParam,
            IntPtr CallackParam);

        private int m_id = -1;
        private Process m_process = null;
        private string m_dumpName = string.Empty;
        private string m_hashStr = string.Empty;

        internal ProcessDumper(int ProcessId)
        {
            m_id = ProcessId;

            m_process = Process.GetProcessById(m_id);

            if (!Directory.Exists("./Dumps"))
            {
                Directory.CreateDirectory("./Dumps");
            }
        }

        internal bool IsAlive()
        {
            if (m_process != null)
            {
                return !m_process.HasExited;
            }
            else
            {
                return false;
            }
        }

        internal void DoProcessExit()
        {
            if (m_process != null && !m_process.HasExited)
                m_process.Kill();
        }

        internal string GetFileName()
        {
            if (m_process == null || m_process.HasExited)
                return string.Empty;

            return Path.GetFileName(m_process.MainModule.FileName);
        }

        internal string GetProcessName()
        {
            if (m_process == null || m_process.HasExited)
                return string.Empty;

            return m_process.ProcessName;
        }

        internal string GetVersion()
        {
            if (m_process != null && !m_process.HasExited)
            {
                FileVersionInfo fvi = m_process.MainModule.FileVersionInfo;

                if (fvi != null && fvi.FileVersion != null)
                {
                    return $"({Path.GetFileName(fvi.FileVersion.ToString())})";
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        internal bool CreateDump()
        {
            if (m_process.HasExited)
                return false;

            DateTime time = DateTime.Now;

            using (SHA256 mySHA256 = SHA256.Create())
            {
                byte[] hash = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(time.ToString()));
                m_hashStr = time.ToString("MMddyyyyHHmmss");//ByteArrayToString(hash);
            }

            string fileToDump = $"./Dumps/{m_process.ProcessName}_{m_hashStr}.dmp";

            FileStream fsToDump = null;

            if (File.Exists(fileToDump))
                fsToDump = File.Open(fileToDump, FileMode.Append);
            else
                fsToDump = File.Create(fileToDump);

            MiniDumpWriteDump(m_process.Handle, m_process.Id,
                fsToDump.SafeFileHandle.DangerousGetHandle(), MINIDUMP_TYPE.MiniDumpNormal,
                IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);

            fsToDump.Close();

            m_dumpName = fileToDump;

            return true;
        }

        internal bool CreateFinal(string message)
        {
            if (m_process.HasExited)
                return false;

            string messageFile = $"./Dumps/Message.txt";

            using (TextWriter textWriter = new StreamWriter(File.Open(messageFile, FileMode.Create, FileAccess.Write)))
            {
                textWriter.Write(message);
                textWriter.Flush();
                textWriter.Close();
            }

            using (ZipArchive zipArchive = new ZipArchive(File.Open($"./Dumps/{m_process.ProcessName}_{m_hashStr}.zip", FileMode.Create), ZipArchiveMode.Create))
            {
                ZipArchiveEntry zipEntry = zipArchive.CreateEntry("Message.txt", CompressionLevel.Optimal);

                byte[] buff = File.ReadAllBytes(messageFile);

                Stream zipStream = zipEntry.Open();
                
                zipStream.Write(buff, 0, buff.Length);
                zipStream.Flush();
                zipStream.Close();

                zipEntry = zipArchive.CreateEntry($"{m_process.ProcessName}_{m_hashStr}.dmp", CompressionLevel.Optimal);

                buff = File.ReadAllBytes(m_dumpName);

                zipStream = zipEntry.Open();

                zipStream.Write(buff, 0, buff.Length);
                zipStream.Flush();
                zipStream.Close();
            }

            if (File.Exists(messageFile))
                File.Delete(messageFile);

            if (File.Exists(m_dumpName))
                File.Delete(m_dumpName);

            return true;
        }

        internal static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }
}
