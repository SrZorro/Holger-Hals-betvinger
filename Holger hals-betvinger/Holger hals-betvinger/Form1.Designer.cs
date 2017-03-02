namespace Holger_hals_betvinger
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Screen = new System.Windows.Forms.PictureBox();
            this.GameTick = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Screen)).BeginInit();
            this.SuspendLayout();
            // 
            // Screen
            // 
            this.Screen.BackColor = System.Drawing.Color.MidnightBlue;
            this.Screen.Location = new System.Drawing.Point(0, 0);
            this.Screen.Name = "Screen";
            this.Screen.Size = new System.Drawing.Size(500, 400);
            this.Screen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Screen.TabIndex = 0;
            this.Screen.TabStop = false;
            this.Screen.Click += new System.EventHandler(this.Screen_Click);
            this.Screen.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Screen_MouseMove);
            // 
            // GameTick
            // 
            this.GameTick.Interval = 50;
            this.GameTick.Tick += new System.EventHandler(this.GameTick_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(784, 462);
            this.Controls.Add(this.Screen);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(400, 250);
            this.Name = "Form1";
            this.Text = "Holger hals-betvinger";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.Screen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Screen;
        private System.Windows.Forms.Timer GameTick;
    }
}

