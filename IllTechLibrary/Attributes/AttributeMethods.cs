using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IllTechLibrary.Attributes
{
    internal enum ValidateState
    {
        Keep,
        Remove
    }

    internal static class AttributeMethods
    {
        internal static ValidateState Validate<T>(MemberInfo info, Object Expect)
        {
            if (System.Attribute.IsDefined(info, typeof(T)))
            {
                if (!System.Attribute.GetCustomAttribute(info, typeof(T)).Match(Expect))
                {
                    return ValidateState.Remove;
                }
            }

            return ValidateState.Keep;
        }
    }
}
