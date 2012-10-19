using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MCModeller.Minecraft.Rendering
{
    /* Yeah it's unsafe, but it's okay Visual Studio, I got this ;) */
    /// <summary>
    /// 4 byte sized memory block that can be referenced as multiple value types
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct FastDataConverter
    {
        [FieldOffset(0)]
        public int IntValue;
        [FieldOffset(0)]
        public float FloatValue;
        [FieldOffset(0)]
        public byte ByteValue0;
        [FieldOffset(1)]
        public byte ByteValue1;
        [FieldOffset(2)]
        public byte ByteValue2;
        [FieldOffset(3)]
        public byte ByteValue3;
        [FieldOffset(0)]
        public short ShortValue;
        [FieldOffset(0)]
        public fixed byte Array[4];
    }

}
