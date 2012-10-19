using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
using SharpGL.Enumerations;

namespace MCModeller.Minecraft.Rendering
{
    public class Tessellator : ITessellator
    {
        public OpenGL GL
        {
            get { return MainForm.GL; }
        }

        /* Why so damn big */
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


        //TODO: Optimize the buffer system
        /// <summary>
        /// GL Allocation Buffer
        /// </summary>
        private static Buffer ByteBuffer = new byte[NativeBufferSize * 4];
        private static int[] IntBuffer = new int[NativeBufferSize];
        private static float[] FloatBuffer = new float[NativeBufferSize];
        private static short[] ShortBuffer = new short[NativeBufferSize * 2];

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

        private static int[] VertexBuffers;

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
                VertexBuffers = new int[VboCount];
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
                    Array.Copy(this.RawBuffer, offs *8, IntBuffer, 
                        
                    offs += vtc;

                    if (HasTexture)
                    {
                        /* Look ma, I reimplemented FloatBuffer when I could 
                         * have just used Collection of float */
                        GL.TexCoordPointer(2, OpenGL.GL_FLOAT, 32, );
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


        public void AddTranslation(float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        public void AddVertex(double x, double y, double z)
        {
            throw new NotImplementedException();
        }

        public void AddVertexWithUV(double x, double y, double z, double textureU, double textureV)
        {
            throw new NotImplementedException();
        }

        public void DisableColor()
        {
            throw new NotImplementedException();
        }

        public void SetBrightness(int brightness)
        {
            throw new NotImplementedException();
        }

        public void SetColorOpaque(int r, int g, int b)
        {
            throw new NotImplementedException();
        }

        public void SetColorOpaque_F(float r, float g, float b)
        {
            throw new NotImplementedException();
        }

        public void SetColorOpaque_I(int color)
        {
            throw new NotImplementedException();
        }

        public void SetColorRGBA(int r, int g, int b, int a)
        {
            throw new NotImplementedException();
        }

        public void SetColorRGBA_F(float r, float g, float b, float a)
        {
            throw new NotImplementedException();
        }

        public void SetColorRGBA_I(int color, int alpha)
        {
            throw new NotImplementedException();
        }

        public void SetNormal(float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        public void SetTextureUV(double textureU, double textureV)
        {
            throw new NotImplementedException();
        }

        public void SetTranslation(double x, double y, double z)
        {
            throw new NotImplementedException();
        }

        public void StartDrawing(int mode)
        {
            throw new NotImplementedException();
        }

        public void StartDrawingQuads()
        {
            throw new NotImplementedException();
        }
    }
}
