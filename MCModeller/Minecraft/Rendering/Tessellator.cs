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
        /* Why for you divide, is this obfus' code ? */
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

        private TessellatorVertex[] VertexBuffer;

        private int VertexCount = 0;

        private float TextureU;
        private float TextureV;

        private int Brightness;

        private int Color;

        private bool HasColor = false;

        private bool HasTexture = false;
        private bool HasBrightness = false;
        private bool HasNormals = false;

        private int RawBufferIndex = 0;

        private int AddedVerticies = 0;

        private bool IsColorDisabled = false;

        /// <summary>
        /// Draw mode being used by the Tessellator
        /// </summary>
        public uint DrawMode;

        //TODO: Use Vertex if suitable

        public double xOffset;

        public double yOffset;

        public double zOffset;

        // How is an Integer a normal? Eugh
        public int Normal;

        /* Pointless parameter is pointless */
        public static Tessellator Instance = new Tessellator(2097152);

        public bool IsTessellating = false;

        private static bool UseVBO = false;

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
        }

        /// <summary>
        /// Resets the state
        /// </summary>
        private void Reset()
        {
            this.VertexCount = 0;
            /* TODO: Clear the buffer */
            this.AddedVerticies = 0;
        }

        public int Draw()
        {
            //TODO: IsDrawing should be IsTesselating? makes more sense in this context
            if (!this.IsTessellating)
            {
                throw new InvalidOperationException("Not Tesselating!");
            }
            else
            {
                this.IsTessellating = false;
                int VertexIndex = 0;
                while (VertexIndex < VertexCount)
                {
                    int vtc = 0;
                    if (DrawMode == OpenGL.GL_QUADS && ConvertQuadsToTriangles)
                    {
                        vtc = Math.Min(VertexCount - VertexIndex, TrivertsInBuffer);
                    }
                    else
                    {
                        vtc = Math.Min(VertexCount - VertexIndex, NativeBufferSize >> 5);
                    }
                        
                    VertexIndex += vtc;

                    if (HasTexture)
                    {
                        GL.TexCoordPointer(2, OpenGL.GL_FLOAT, 32, );
                        GL.EnableClientState(OpenGL.GL_TEXTURE_COORD_ARRAY);

                        
                    }

                    if(HasBrightness){
                        //TODO: Implement light map
                    }

                    if (HasColor)
                    {
                        ByteBuffer.Position = 20;
                        GL.NormalPointer(
                    }
                }
                
            }
        }


        public void AddTranslation(float x, float y, float z)
        {
            this.xOffset += x;
            this.yOffset += y;
            this.zOffset += z;
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
            this.IsColorDisabled = true;
        }

        public void SetBrightness(int brightness)
        {
            this.HasBrightness = true;
            this.Brightness = brightness;
        }

        public void SetColorOpaque(int r, int g, int b)
        {
            this.SetColorRGBA(r, g, b, 255);
        }

        public void SetColorOpaque_F(float r, float g, float b)
        {
            this.SetColorOpaque((int)(r * 255.0F), (int)(g * 255.0F), (int)(b * 255.0F));
        }

        public void SetColorOpaque_I(int color)
        {
            //TODO: Confirm this bit order, endianness again
            int red = color >> 16 & 255;
            int green = color >> 8 & 255;
            int blue = color & 255;
            this.SetColorOpaque(red, green, blue);
        }

        public void SetColorRGBA(int r, int g, int b, int a)
        {
            if (!this.IsColorDisabled)
            {
                if (r < 0)
                    r = 0;
                if (g < 0)
                    g = 0;
                if (b < 0)
                    b = 0;
                if (a < 0)
                    a = 0;
                if (a > 255)
                    a = 255;
                if (b > 255)
                    b = 255;
                if (g > 255)
                    g = 255;
                if (r > 255)
                    g = 255;
                this.HasColor = true;
                // TODO: Confirm this bit order code
                // Java is an opossing bit order to C#
                if (BitConverter.IsLittleEndian)
                {
                    this.Color = a << 24 | b << 16 | g << 8 | r;
                }
                else
                {
                    this.Color = r << 24 | g << 16 | b << 8 | a;
                }
            }
        }

        public void SetColorRGBA_F(float r, float g, float b, float a)
        {
            this.SetColorRGBA((int)(r * 255.0F), (int)(g * 255.0F), (int)(b * 255.0F), (int)(a * 255.0F));
        }

        public void SetColorRGBA_I(int color, int alpha)
        {
            //TODO: Confirm this bit order, endian
            int red = color >> 16 & 255;
            int green = color >> 8 & 255;
            int blue = color & 255;
            this.SetColorRGBA(red, green, blue, alpha);
        }

        public void SetNormal(float x, float y, float z)
        {
            //this.HasNormals = true;
            //TODO: Confirm normal code.
        }

        public void SetTextureUV(float textureU, float textureV)
        {
            this.TextureU = textureU;
            this.TextureV = textureV;
        }

        public void SetTranslation(double x, double y, double z)
        {
            this.xOffset = x;
            this.yOffset = y;
            this.zOffset = z;
        }

        public void StartTessellating(uint mode)
        {
            if (this.IsTessellating)
            {
                throw new InvalidOperationException("Already Tesselating!");
            }
            else
            {
                this.IsTessellating = true;
                this.Reset();
                this.DrawMode = mode;
                this.HasNormals = false;
                this.HasColor = false;
                this.HasTexture = false;
                this.HasBrightness = false;
                this.IsColorDisabled = false;
            }
        }

        public void StartTessellatingQuads()
        {
            this.StartTessellating(OpenGL.GL_QUADS);
        }
    }
}
