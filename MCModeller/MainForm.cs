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
            GL.LoadIdentity();
            GL.PolygonMode(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_FILL);
            GL.CullFace(OpenGL.GL_NONE);

            var tessellator = Minecraft.Rendering.Tessellator.Instance;

            GL.ColorMask(1, 1, 1, 1);

            lilPerson.bipedHead.isHidden = false;

            //  Draw a coloured pyramid.
           // GL.Begin(OpenGL.GL_TRIANGLES);
           // TextureManager.BindTexture("player");

            GL.Enable(OpenGL.GL_TEXTURE_2D);
            try
            {
                lilPerson.onGround = 1.0f;
                lilPerson.render(tessellator, 0f, 0f, 0f, 0f, 0f, 0.1f);
                GL.Disable(OpenGL.GL_TEXTURE_2D);
                GL.PolygonMode(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_LINE);
                lilPerson.render(tessellator, 0f, 0f, 0f, 0f, 0f, 0.1f);
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
            GL.Viewport(0, 0, Width, Height);
            GL.Perspective(60.0f, (double)Width / (double)Height, 0.01, 500.0);

            //  Use the 'look at' helper function to position and aim the camera.
            GL.LookAt(5, -5, -5, 2, 0, 0, 0, -1, 0);

            //  Set the modelview matrix.
            GL.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        private void openGLViewport_OpenGLInitialized(object sender, EventArgs e)
        {
            TextureManager.InitTexture("char.png", "player");


           GL.ShadeModel(OpenGL.GL_FLAT);
           GL.Disable(OpenGL.GL_BLEND);
           GL.DepthMask(1);
           GL.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_NEAREST);
           GL.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_NEAREST);
            //  Set the clear color.
            GL.ClearColor(0, 0, 0, 0);
        }
    }
}
