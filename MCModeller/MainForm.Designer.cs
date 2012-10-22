namespace MCModeller
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openGLViewport = new SharpGL.OpenGLControl();
            this.panelLeftPane = new System.Windows.Forms.Panel();
            this.panelRightPane = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.openGLViewport)).BeginInit();
            this.SuspendLayout();
            // 
            // openGLViewport
            // 
            this.openGLViewport.BackColor = System.Drawing.Color.Black;
            this.openGLViewport.BitDepth = 24;
            this.openGLViewport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openGLViewport.DrawFPS = false;
            this.openGLViewport.FrameRate = 20;
            this.openGLViewport.Location = new System.Drawing.Point(200, 0);
            this.openGLViewport.Name = "openGLViewport";
            this.openGLViewport.RenderContextType = SharpGL.RenderContextType.NativeWindow;
            this.openGLViewport.Size = new System.Drawing.Size(469, 557);
            this.openGLViewport.TabIndex = 0;
            this.openGLViewport.OpenGLInitialized += new System.EventHandler(this.openGLViewport_OpenGLInitialized);
            this.openGLViewport.OpenGLDraw += new System.Windows.Forms.PaintEventHandler(this.openGLViewport_OpenGLDraw);
            this.openGLViewport.Resized += new System.EventHandler(this.openGLViewport_Resized);
            // 
            // panelLeftPane
            // 
            this.panelLeftPane.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeftPane.Location = new System.Drawing.Point(0, 0);
            this.panelLeftPane.Name = "panelLeftPane";
            this.panelLeftPane.Size = new System.Drawing.Size(200, 557);
            this.panelLeftPane.TabIndex = 1;
            // 
            // panelRightPane
            // 
            this.panelRightPane.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRightPane.Location = new System.Drawing.Point(669, 0);
            this.panelRightPane.Name = "panelRightPane";
            this.panelRightPane.Size = new System.Drawing.Size(200, 557);
            this.panelRightPane.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 557);
            this.Controls.Add(this.openGLViewport);
            this.Controls.Add(this.panelRightPane);
            this.Controls.Add(this.panelLeftPane);
            this.Name = "MainForm";
            this.Text = "MC Modeller";
            ((System.ComponentModel.ISupportInitialize)(this.openGLViewport)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SharpGL.OpenGLControl openGLViewport;
        private System.Windows.Forms.Panel panelLeftPane;
        private System.Windows.Forms.Panel panelRightPane;
    }
}

