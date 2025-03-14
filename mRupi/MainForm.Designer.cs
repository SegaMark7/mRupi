namespace mRubi
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            mS_main = new MenuStrip();
            файлToolStripMenuItem = new ToolStripMenuItem();
            открытьРомToolStripMenuItem = new ToolStripMenuItem();
            t_main = new System.Windows.Forms.Timer(components);
            oFD_loadRom = new OpenFileDialog();
            pictureBox1 = new PictureBox();
            mS_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // mS_main
            // 
            mS_main.Items.AddRange(new ToolStripItem[] { файлToolStripMenuItem });
            mS_main.Location = new Point(0, 0);
            mS_main.Name = "mS_main";
            mS_main.Size = new Size(624, 24);
            mS_main.TabIndex = 0;
            mS_main.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { открытьРомToolStripMenuItem });
            файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            файлToolStripMenuItem.Size = new Size(37, 20);
            файлToolStripMenuItem.Text = "File";
            // 
            // открытьРомToolStripMenuItem
            // 
            открытьРомToolStripMenuItem.Name = "открытьРомToolStripMenuItem";
            открытьРомToolStripMenuItem.Size = new Size(180, 22);
            открытьРомToolStripMenuItem.Text = "Load Rom";
            открытьРомToolStripMenuItem.Click += LoadRomToolStripMenuItem_Click;
            // 
            // t_main
            // 
            t_main.Interval = 16;
            // 
            // oFD_loadRom
            // 
            oFD_loadRom.FileName = "openFileDialog1";
            oFD_loadRom.Filter = "ROM Files|*.rom;*.bin|All Files|*.*";
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(0, 24);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(624, 417);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(624, 441);
            Controls.Add(pictureBox1);
            Controls.Add(mS_main);
            MainMenuStrip = mS_main;
            Name = "MainForm";
            Text = "mRubi";
            Load += MainForm_Load;
            KeyDown += MainForm_KeyDown;
            mS_main.ResumeLayout(false);
            mS_main.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip mS_main;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem открытьРомToolStripMenuItem;
        private System.Windows.Forms.Timer t_main;
        private OpenFileDialog oFD_loadRom;
        private PictureBox pictureBox1;
    }
}
