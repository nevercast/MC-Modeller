using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCModeller.Minecraft.Compatibility.IO;

namespace MCModeller.Minecraft.Rendering
{
    public class Tessellator
    {
        private static int NativeBufferSize = 0x200000;
        private static int TrivertsInBuffer = (NativeBufferSize / 48) * 6;

        public static bool RenderingWorldRenderer = false;
        public bool DefaultTexture = false;
        
        private int RawBufferSize = 0;

        public int TextureID = 0;

        /// <summary>
        /// Boolean used to check whether quads should be drawn as two triangles
        /// </summary>
        private static bool ConvertQuadsToTriangles = false;

        /// <summary>
        /// GL Allocation Buffer
        /// </summary>
        private static ByteBuffer ByteBuffer = ByteBuffer.Allocate(NativeBufferSize * 4);
        private static IntBuffer IntBuffer = ByteBuffer.IntBuffer;
        private static FloatBuffer FloatBuffer = ByteBuffer.FloatBuffer;
        private static ShortBuffer ShortBuffer = ByteBuffer.ShortBuffer;

        /// <summary>
        /// Raw Integer Array
        /// </summary>
        private int[] RawBuffer;

        private int VertexCount = 0;

        private double TextureU;
        private double TextureV;

        private int Brightness;

        private int Color;

        private bool HasColor = false;

        private bool HasTexture = false;
        private bool HsBrightness = false;
        private bool HsNormals = false;

        private int RawBufferIndex = 0;
    }
}
