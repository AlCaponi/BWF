
using System;
using System.Runtime.InteropServices;

namespace BWF.Library
{
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    public struct FragenTyp
    {
        public static Guid Skala = new Guid("9c4c7610-fffc-4159-af45-0880f010936c");
        public static Guid MultipleChoice = new Guid("e0285169-1340-4b69-8411-e7272017a28a");
        public static Guid MultilineText = new Guid("707b446e-4c25-4494-a18a-f444480c6e25");
        public static Guid Text = new Guid("65bbbf50-21c0-431b-9f5e-66629c04e2a6");
        public static Guid SingleChoice = new Guid("d1609e72-54a2-4680-bc8b-02bd0ebaecf3");
        public static Guid DropDown = new Guid("c6a0db90-0fe5-4657-afde-450081f7ae9b");
    }
}