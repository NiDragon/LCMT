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
    class AliasAttribute : System.Attribute
    {
    }
}
