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
            InitializeComponent();
            __openGLContext = openGLViewport.OpenGL;
        }

        #region OpenGL Context
        private static OpenGL __openGLContext;
        public static OpenGL GL
        {
            get
            {
                return __openGLContext;
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

            //  Rotate around the Y axis.
            GL.Rotate(rotation, 0.0f, 1.0f, 0.0f);

            //  Draw a coloured pyramid.
            GL.Begin(OpenGL.GL_TRIANGLES);
            GL.Color(1.0f, 0.0f, 0.0f);
            GL.Vertex(0.0f, 1.0f, 0.0f);
            GL.Color(0.0f, 1.0f, 0.0f);
            GL.Vertex(-1.0f, -1.0f, 1.0f);
            GL.Color(0.0f, 0.0f, 1.0f);
            GL.Vertex(1.0f, -1.0f, 1.0f);
            GL.Color(1.0f, 0.0f, 0.0f);
            GL.Vertex(0.0f, 1.0f, 0.0f);
            GL.Color(0.0f, 0.0f, 1.0f);
            GL.Vertex(1.0f, -1.0f, 1.0f);
            GL.Color(0.0f, 1.0f, 0.0f);
            GL.Vertex(1.0f, -1.0f, -1.0f);
            GL.Color(1.0f, 0.0f, 0.0f);
            GL.Vertex(0.0f, 1.0f, 0.0f);
            GL.Color(0.0f, 1.0f, 0.0f);
            GL.Vertex(1.0f, -1.0f, -1.0f);
            GL.Color(0.0f, 0.0f, 1.0f);
            GL.Vertex(-1.0f, -1.0f, -1.0f);
            GL.Color(1.0f, 0.0f, 0.0f);
            GL.Vertex(0.0f, 1.0f, 0.0f);
            GL.Color(0.0f, 0.0f, 1.0f);
            GL.Vertex(-1.0f, -1.0f, -1.0f);
            GL.Color(0.0f, 1.0f, 0.0f);
            GL.Vertex(-1.0f, -1.0f, 1.0f);
            GL.End();

            //  Nudge the rotation.
            rotation += 3.0f;
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
