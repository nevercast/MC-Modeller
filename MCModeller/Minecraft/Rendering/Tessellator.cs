using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

        /* Initial size of the vertex buffer */
        private const int BUFFER_INITIALSIZE = 1000;

        private TessellatorVertex Vertex;

        public static bool RenderingWorldRenderer = false;
        public bool DefaultTexture = false;

        public int TextureID = 0;

        /// <summary>
        /// Boolean used to check whether quads should be drawn as two triangles
        /// </summary>
        private static bool ConvertQuadsToTriangles = false;


        /// <summary>
        /// GL Allocation Buffer
        /// </summary>
        TessellatorVertex[] VertexBuffer;

        private int VertexCount = 0;

        private float TextureU;
        private float TextureV;

        private int Brightness;

        private int Color;

        private bool HasColor = false;

        private bool HasTexture = false;
        private bool HasBrightness = false;
        private bool HasNormals = false;

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

        public int Normal;

        /* Pointless parameter is pointless */
        public static Tessellator Instance = new Tessellator(2097152);

        public bool IsTessellating = false;
        
        private Tessellator(int someUselessParameter) :this()
        {
            
        }

        /// <summary>
        /// Public constructor
        /// </summary>
        public Tessellator()
        {
            /* Buffer Initialized ? */
            if (VertexBuffer == null)
            {
                /* Create Buffer */
                VertexBuffer = new TessellatorVertex[BUFFER_INITIALSIZE];
            }
        }

        static Tessellator()
        {
            Instance.DefaultTexture = true;
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
                /* Pin buffer */
                var bufferHandle = GCHandle.Alloc(VertexBuffer, (GCHandleType.Pinned));
                var stride = Marshal.SizeOf(typeof(TessellatorVertex)); /* 32 */
                var pointer = bufferHandle.AddrOfPinnedObject();

                this.IsTessellating = false;
                if (this.HasTexture)
                {
                    GL.TexCoordPointer(2, OpenGL.GL_FLOAT, stride, pointer + 12);
                    GL.EnableClientState(OpenGL.GL_TEXTURE_COORD_ARRAY);
                }
                if (this.HasBrightness)
                {
                    //TODO: Brightness
                }
                if (this.HasColor)
                {
                    GL.ColorPointer(4, OpenGL.GL_UNSIGNED_BYTE, stride, pointer + 20);
                    GL.EnableClientState(OpenGL.GL_COLOR_ARRAY);
                }
                if (this.HasNormals)
                {
                    GL.NormalPointer(OpenGL.GL_UNSIGNED_BYTE, stride, pointer + 24);
                    GL.EnableClientState(OpenGL.GL_NORMAL_ARRAY);
                }
                GL.VertexPointer(3, OpenGL.GL_FLOAT, stride, pointer);
                GL.EnableClientState(OpenGL.GL_VERTEX_ARRAY);

                if (this.DrawMode == OpenGL.GL_QUADS && ConvertQuadsToTriangles)
                {
                    GL.DrawArrays(OpenGL.GL_TRIANGLES, 0, VertexCount);
                }
                else
                {
                    GL.DrawArrays(DrawMode, 0, VertexCount);
                }

                GL.DisableClientState(OpenGL.GL_VERTEX_ARRAY);
                if (this.HasTexture)
                {
                    GL.DisableClientState(OpenGL.GL_TEXTURE_COORD_ARRAY);
                }
                if (this.HasBrightness)
                {
                    //TODO: Brightness
                }
                if (this.HasColor)
                {
                    GL.DisableClientState(OpenGL.GL_COLOR_ARRAY);
                }
                if (this.HasNormals)
                {
                    GL.DisableClientState(OpenGL.GL_NORMAL_ARRAY);
                }

                bufferHandle.Free();
                var vtc = VertexCount;
                this.Reset();
                return vtc;                
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
            /* Buffer too small */
            if (VertexCount > VertexBuffer.Length - 1)
            {
                var tmpBuffer = new TessellatorVertex[VertexBuffer.Length * 2];
                Array.Copy(VertexBuffer, tmpBuffer, VertexBuffer.Length);
                VertexBuffer = tmpBuffer;
            }
            ++AddedVerticies;

            Vertex = new TessellatorVertex();

            /* Convert 4 verticies to a quad */
            if (this.DrawMode == OpenGL.GL_QUADS && ConvertQuadsToTriangles && AddedVerticies % 4 == 0)
            {
                //TODO: Implement all the quad codes
            }

            if (HasTexture)
            {
                Vertex.textureU = this.TextureU;
                Vertex.textureV = this.TextureV;
            }
            if (HasBrightness)
            {
                Vertex.brightness = this.Brightness;
            }
            if (HasColor)
            {
                Vertex.color = this.Color;
            }
            if (this.HasNormals)
            {
                Vertex.normal = this.Normal;
            }
            Vertex.x = (float)(xOffset + x);
            Vertex.y = (float)(yOffset + y);
            Vertex.z = (float)(zOffset + z);

            VertexBuffer[VertexCount++] = Vertex;
        }

        public void AddVertexWithUV(double x, double y, double z, float textureU, float textureV)
        {
            this.SetTextureUV(textureU, textureV);
            this.AddVertex(x, y, z);
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
            this.HasNormals = true;
            byte xByte = (byte)(x * 255);
            byte yByte = (byte)(y * 255);
            byte zByte = (byte)(z * 255);
            this.Normal = xByte & 255 | (yByte & 255) << 8 | (zByte & 255) << 16;
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
