using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MCModeller.Minecraft.Rendering
{
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public unsafe struct TessellatorVertex
    {
        [FieldOffset(0)]
        public float x; // 0
        [FieldOffset(4)]
        public float y; // 1
        [FieldOffset(8)]
        public float z; // 2
        [FieldOffset(12)]
        public float textureU; // 3
        [FieldOffset(16)]
        public float textureV; // 4
        [FieldOffset(20)]
        public int color; /* 5 Packed, Red,green,blue,alpha */
        [FieldOffset(24)]
        public int normal; /* 6 Packed */
        [FieldOffset(28)]
        public int brightness; /* 7 */

        /* Buffer that refers to the vertex entirely as an int array, awesomesauce */
        [FieldOffset(0)]
        public fixed int IntegerBuffer[8];
    }
}
