using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCModeller.Minecraft.MathClasses;
using MCModeller.Minecraft.Rendering;
using MCModeller.Minecraft.Rendering.Modelling;
using MCModeller.Minecraft.Rendering.Modelling.Implementations;
using SharpGL;

namespace MCModeller
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            Instance = this;
            InitializeComponent();
            lilPerson  = new ModelBiped();
        }

        #region OpenGL Context

        public static MainForm Instance { get; private set; }

        public static OpenGL GL
        {
            get
            {
                return Instance.openGLViewport.OpenGL;
            }
        }

        #endregion


        static ModelBiped lilPerson;
        bool drawing = false;

        private void openGLViewport_OpenGLDraw(object sender, PaintEventArgs e)
        {
            if (drawing) return;
            drawing = true;
            //  Clear the color and depth buffer.
            GL.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            GL.PushMatrix();
            //  Load the identity matrix.
        //    GL.LoadIdentity();
            GL.PolygonMode(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_FILL);

            var tessellator = Minecraft.Rendering.Tessellator.Instance;

            GL.ColorMask(1, 1, 1, 1);

            float oneOverSixteen = 0.0625F;
            //  Draw a coloured pyramid.
           // GL.Begin(OpenGL.GL_TRIANGLES);
           // TextureManager.BindTexture("player");



        /*    tessellator.StartTessellating(OpenGL.GL_TRIANGLES);
            tessellator.SetColorOpaque(255, 0, 0);
            tessellator.AddVertex(1, 1, 0);
            tessellator.AddVertex(-1, 0, 0);
            tessellator.AddVertex(1, 0, 0);
            tessellator.Draw(); */

            GL.Scale(-1f, -1f, 1f);
            GL.Translate(0.0f, -24.0f * oneOverSixteen - 0.0078125f, 0.0f);

            float playerScale = 0.9375F;
            GL.Scale(playerScale, playerScale, playerScale);

            GL.Enable(OpenGL.GL_TEXTURE_2D);
            try
            {
                lilPerson.render(tessellator, 0f, 0f, 0f, 0f, 0f, oneOverSixteen);
                GL.Disable(OpenGL.GL_TEXTURE_2D);
                GL.PolygonMode(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_LINE);
                lilPerson.render(tessellator, 0f, 0f, 0f, 0f, 0f, 1f / 16f);
            }
            catch (Exception ex) { }
            GL.Finish();

            GL.PopMatrix();
            drawing = false;
        }

        private void openGLViewport_Resized(object sender, EventArgs e)
        {
            //  Set the projection matrix.
            GL.MatrixMode(OpenGL.GL_PROJECTION);

            //  Load the identity.
            GL.LoadIdentity();

            //  Create a perspective transformation.
            GL.Viewport(0, 0, openGLViewport.Width, openGLViewport.Height);
            GL.Perspective(60.0f, (double)openGLViewport.Width / (double)openGLViewport.Height, 0.01, 500.0);

            //  Use the 'look at' helper function to position and aim the camera.
            GL.LookAt(5, 2.64, -5, 0, 0, 0, 0, 1, 0);
            //GL.perspe
            //  Set the modelview matrix.
            GL.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        private void openGLViewport_OpenGLInitialized(object sender, EventArgs e)
        {
            TextureManager.InitTexture("char.png", "player");
            //  Set the projection matrix.
            GL.MatrixMode(OpenGL.GL_PROJECTION);

            //  Load the identity.
            GL.LoadIdentity();

            //  Create a perspective transformation.
            GL.Viewport(0, 0, Width, Height);
            GL.Perspective(70.0f, (double)Width / (double)Height, 0.05, 500.0);

            //  Use the 'look at' helper function to position and aim the camera.
            GL.LookAt(5, 5, 5, 0, 0, 0, 0, 1, 0);

            //  Set the modelview matrix.
            GL.MatrixMode(OpenGL.GL_MODELVIEW);

           GL.ShadeModel(OpenGL.GL_FLAT);
           GL.Enable(OpenGL.GL_BLEND);
           GL.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
           GL.DepthMask(1);
           GL.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_NEAREST);
           GL.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_NEAREST);
            //  Set the clear color.
            GL.ClearColor(0, 0, 0, 0);
        }
    }
}
