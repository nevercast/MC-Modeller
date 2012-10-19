using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MCModeller.Minecraft.Rendering
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 32)]
    public struct TessellatorVertex
    {
        public float x;
        public float y;
        public float z;
        public float textureU;
        public float textureY;
        public int color; /* Red,green,blue,alpha */
        public float normalX;
        public float normalY;
        public float normalZ;
    }
}
