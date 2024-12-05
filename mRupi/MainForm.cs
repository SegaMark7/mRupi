namespace mRubi
{
    public partial class MainForm : Form
    {
        private Bitmap screenBuffer;
        private bool isFullscreen;

        private const int DISPLAY_WIDTH = 320;
        private const int DISPLAY_HEIGHT = 240;

        public MainForm(string[] args)
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            MinimumSize = Size;

            //this.Text =

        }

        private void LoadRomToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
            else if (e.KeyCode == Keys.F11)
            {
                ToggleFullscreen();
            }
        }

        private void ToggleFullscreen()
        {
            isFullscreen = !isFullscreen;
            if (isFullscreen)
            {
                this.WindowState = FormWindowState.Maximized;
                this.FormBorderStyle = FormBorderStyle.None;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.Sizable;
            }
        }
    }
}
