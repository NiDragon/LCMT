using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IllTechLibrary.Util
{
    public class BitHelpers
    {
        public static bool GetBit32(int i, int flags)
        {
            BitArray a = new BitArray(BitConverter.GetBytes(flags));
            return a.Get(i);
        }

        public static bool GetBit(int i, ulong flags)
        {
            BitArray a = new BitArray(BitConverter.GetBytes(flags));
            return a.Get(i);
        }

        public static bool IsBitSet(long b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }
    }
}
