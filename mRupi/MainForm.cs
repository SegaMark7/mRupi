using mRupi;

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
            DialogResult result = oFD_loadRom.ShowDialog();
            if (result == DialogResult.OK)
            {
                string cartName = oFD_loadRom.FileName;
                string extension = Path.GetExtension(cartName);

                // Open cartridge file
                using (var cartFile = new FileStream(cartName, FileMode.Open, FileAccess.Read))
                {
                    if (cartFile.Length == 0)
                    {
                        Console.WriteLine($"Failed to open {cartName}");
                        return;
                    }

                    bool le = cartFile.ReadByte() != 0x0E;//Первый байт означает little endian или big endian 
                    cartFile.Seek(0, SeekOrigin.Begin);

                    if (!le)
                    {
                        //config.Cart.Rom = cartFile.ToList();
                    }
                    else
                    {
                        Console.WriteLine("Found little-endian ROM");

                        using var reader = new BinaryReader(cartFile);
                        long size = cartFile.Length;
                        //cartFile.Seek(0, SeekOrigin.Begin);

                        for (int i = 0; i < size / 2; i++)
                        {
                            ushort t16 = reader.ReadUInt16();
                            t16 = Common.Bswap16(t16);

                            //config.Cart.Rom.Add((byte)(t16 & 0xFF));
                            //config.Cart.Rom.Add((byte)(t16 >> 8));
                        }
                    }
                }

            }
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
