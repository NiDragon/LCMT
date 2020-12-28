using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IllTechLibrary.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.All,
                   AllowMultiple = false,
                   Inherited = false)]
    class LocaleAttribute : System.Attribute
    {
        private String Language;

        public LocaleAttribute(String Language)
        {
            this.Language = Language;
        }

        public static implicit operator String(LocaleAttribute attrib)
        {
            return attrib.Language;
        }

        public override bool Match(object obj)
        {
            return ((string)obj) == Language;
        }
    }
}
