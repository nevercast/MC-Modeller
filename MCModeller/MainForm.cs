using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpGL;

namespace MCModeller
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            Instance = this;
            InitializeComponent();
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

        float rotation;

        private void openGLViewport_OpenGLDraw(object sender, PaintEventArgs e)
        {
            //  Clear the color and depth buffer.
            GL.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            //  Load the identity matrix.
            GL.LoadIdentity();
            
            var tessellator = Minecraft.Rendering.Tessellator.Instance;

            //  Draw a coloured pyramid.
           // GL.Begin(OpenGL.GL_TRIANGLES);
            try
            {
                tessellator.StartTessellating(OpenGL.GL_TRIANGLES);
                tessellator.SetColorRGBA(255, 255, 255, 255);

                tessellator.AddVertex(0, 1, 0);
                tessellator.AddVertex(-1, 0, 0);
                tessellator.AddVertex(1, 0, 0);
                tessellator.Draw();
            }
            catch (Exception ex) { }
            GL.Finish();
        }

        private void openGLViewport_Resized(object sender, EventArgs e)
        {
            //  Set the projection matrix.
            GL.MatrixMode(OpenGL.GL_PROJECTION);

            //  Load the identity.
            GL.LoadIdentity();

            //  Create a perspective transformation.
            GL.Perspective(60.0f, (double)Width / (double)Height, 0.01, 100.0);

            //  Use the 'look at' helper function to position and aim the camera.
            GL.LookAt(-5, 5, -5, 0, 0, 0, 0, 1, 0);

            //  Set the modelview matrix.
            GL.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        private void openGLViewport_OpenGLInitialized(object sender, EventArgs e)
        {
            //  Set the clear color.
            GL.ClearColor(0, 0, 0, 0);
        }
    }
}
