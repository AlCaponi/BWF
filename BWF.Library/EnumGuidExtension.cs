
using System;
using System.Reflection;

namespace BWF.Library
{
    public static class EnumGuidExtension
    {
        public static Guid GetEnumGuid(this Enum e)
        {
            MemberInfo[] member = e.GetType().GetMember(e.ToString());
            if (member != null && member.Length > 0)
            {
                object[] customAttributes = member[0].GetCustomAttributes(typeof(EnumGuid), false);
                if (customAttributes != null && customAttributes.Length > 0)
                    return ((EnumGuid)customAttributes[0]).Guid;
            }
            throw new ArgumentException("Enum " + e.ToString() + " has no EnumGuid defined!");
        }
    }
}
