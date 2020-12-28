using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IllTechLibrary.Attributes
{
    /// <summary>
    /// Minimum Allowed Version for this entry
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.All,
                   AllowMultiple = true,
                   Inherited = false)]
    class RequiredVersion : System.Attribute
    {
        private int Version;

        public RequiredVersion(int Version)
        {
            this.Version = Version;
        }

        public static implicit operator int(RequiredVersion attrib)
        {
            return attrib.Version;
        }

        public override bool Match(object obj)
        {
            return ((RequiredVersion)obj) == Version;
        }
    }
}
