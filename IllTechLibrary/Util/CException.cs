using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace IllTechLibrary.Util
{
    public class CException
    {
        // Get stack trace for the exception with source file information
        private static StackTrace st = null;

        private static string GetModule()
        {
            // Get the top stack frame
            var frame = st.GetFrame(st.GetFrames().Count() - 1);
            var module = frame.GetMethod().Module;

            return module.Name;
        }

        private static string GetFile()
        {
            // Get the top stack frame
            var frame = st.GetFrame(st.GetFrames().Count() - 1);
            var file = Path.GetFileName(frame.GetFileName());

            return file;
        }

        private static string GetClass()
        {
            // Get the top stack frame
            var frame = st.GetFrame(st.GetFrames().Count() - 1);
            var tmp = frame.GetMethod().ReflectedType.Name;
            var cl = tmp.Substring(0, tmp.Length-2);

            return cl;
        }

        private static string GetMethod()
        {
            // Get the top stack frame
            var frame = st.GetFrame(st.GetFrames().Count() - 1);
            var method = frame.GetMethod();

            return method.Name;
        }

        /// <summary>
        /// Get the line of the exception from the top most module
        /// </summary>
        /// <returns></returns>
        private static int GetLine()
        {
            // Get the top stack frame
            var frame = st.GetFrame(st.GetFrames().Count() - 1);
            var line = frame.GetFileLineNumber();

            return line;
        }

        public static string GetStackTrace(Exception e)
        {
            string stacktrace = "-- Begin Stack Trace (EXCEPTION) --\n";

            stacktrace += e.StackTrace;

            return stacktrace;
        }

        public static string GetStackNoException()
        {
            StackTrace t = new StackTrace();

            string stacktrace = "-- Begin Stack Trace (NO EXCEPTION) --\n";

            // Only use up to 10 frames to keep this to the point
            for(int i = 0; (i < t.FrameCount) && (i < 10); i++)
            {
                stacktrace += $"{t.GetFrame(i)}\n";
            }

            return stacktrace;
        }

        public static string GetLastFrame(Exception e, bool addMessage)
        {
            st = new StackTrace(e, true);

            string msg = addMessage ? e.Message : "";

#if DEBUG
            return $"File: {GetFile()} At Line: {GetLine()}\n{msg}";
#else
            return $"\n{e.Message}";
#endif
        }
    }
}
