using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IllTechLibrary
{
    public class FSUtil
    {
        public static bool FilePathHasInvalidChars(string path)
        {
            return (!string.IsNullOrEmpty(path) && path.IndexOfAny(System.IO.Path.GetInvalidPathChars()) >= 0);
        }
    }
}
