using System;

namespace BWF.Library
{
    internal class EnumGuid : Attribute
    {
        public Guid Guid;

        public EnumGuid(Guid guid)
        {
            this.Guid = guid;
        }
    }
}