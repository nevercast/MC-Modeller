using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCModeller.Minecraft.Compatibility.IO;
using SharpGL;
using SharpGL.Enumerations;

namespace MCModeller.Minecraft.Rendering
{
    public class Tessellator
    {
        public OpenGL GL
        {
            get { return MainForm.GL; }
        }

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

        private static bool TryVBO = false;

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
        private bool HasBrightness = false;
        private bool HsNormals = false;

        private int RawBufferIndex = 0;

        private int AddedVerticies = 0;

        private bool IsColorDisabled = false;

        /// <summary>
        /// Draw mode being used by the Tessellator
        /// </summary>
        public int DrawMode;

        //TODO: Use Vertex if suitable

        public double xOffset;

        public double yOffset;

        public double zOffset;

        // How is an Integer a normal? Eugh
        public int Normal;

        public static Tessellator Instance = new Tessellator(2097152);

        public bool IsDrawing = false;

        private static bool UseVBO = false;

        private static IntBuffer VertexBuffers;

        private int VboIndex = 0;

        private static int VboCount = 10;

        private int BufferSize;

        private Tessellator(int someUselessParameter)
        {

        }

        /// <summary>
        /// Public constructor
        /// </summary>
        public Tessellator()
        {

        }

        static Tessellator()
        {
            Instance.DefaultTexture = true;
            UseVBO = false; // For now, always false. Not used in MC and I'm not sure how to use it either

            if (UseVBO)
            {
                VertexBuffers = new ByteBuffer(VboCount).IntBuffer;
                // ARBVertexBufferObject.glGenBuffersARB(vertexBuffers);
            }
        }

        public int Draw()
        {
            //TODO: IsDrawing should be IsTesselating? makes more sense in this context
            if (!this.IsDrawing)
            {
                throw new InvalidOperationException("Not Tesselating!");
            }
            else
            {
                this.IsDrawing = false;
                int offs = 0;
                while (offs < VertexCount)
                {
                    int vtc = 0;
                    if (DrawMode == 7 && ConvertQuadsToTriangles)
                    {
                        vtc = Math.Min(VertexCount - offs, TrivertsInBuffer);
                    }
                    else
                    {
                        vtc = Math.Min(VertexCount - offs, NativeBufferSize >> 5);
                    }
                    IntBuffer.Clear();
                    IntBuffer.Put(this.RawBuffer, offs * 8, vtc * 8);
                    ByteBuffer.Position = 0;
                    ByteBuffer.Limit = vtc * 32;
                    offs += vtc;

                    if (HasTexture)
                    {
                        FloatBuffer.Position = 3;
                        /* Look ma, I reimplemented FloatBuffer when I could 
                         * have just used Collection of float */
                        GL.TexCoordPointer(2, OpenGL.GL_FLOAT, 32, FloatBuffer);
                        GL.EnableClientState(OpenGL.GL_TEXTURE_COORD_ARRAY);
                    }

                    if(HasBrightness){
                        //TODO: Implement light map
                    }

                    if (HasColor)
                    {
                        ByteBuffer.Position = 20;
                        
                    }
                }
            }
        }
    }
}
