using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IllTechLibrary
{
    public class Interrupt
    {
        public static Interrupt AbortRequested = new Interrupt();

        public Interrupt()
        {
            InterruptRequested = false;
        }

        public void Reset()
        {
            InterruptRequested = false;
        }

        public void Set()
        {
            InterruptRequested = true;
        }

        public bool GetStatus()
        {
            return InterruptRequested;
        }

        private bool InterruptRequested;
    }
}
