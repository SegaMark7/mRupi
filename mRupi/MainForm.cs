using mRupi;
using mRupi.Core;

namespace mRubi
{
    public partial class MainForm : Form
    {
        private Bitmap screenBuffer;
        private bool isFullscreen;

        private const int DISPLAY_WIDTH = 320;
        private const int DISPLAY_HEIGHT = 240;

        private string _cartName;
        private readonly Config.SystemInfo _config = new();

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
                _cartName = oFD_loadRom.FileName;
                string extension = Path.GetExtension(_cartName);

                

                if (!File.Exists(_cartName))
                {
                    Console.WriteLine($"Failed to open {_cartName}");
                    return;
                }

                _config.CartRom = File.ReadAllBytes(_cartName);

                // Определение размера SRAM из заголовка картриджа
                uint sramStart = BitConverter.ToUInt32(_config.CartRom, 0x10);
                uint sramEnd = BitConverter.ToUInt32(_config.CartRom, 0x14);
                uint sramSize = BitConverter.ToUInt32([.. BitConverter.GetBytes(sramEnd).Reverse()], 0) - BitConverter.ToUInt32(BitConverter.GetBytes(sramStart).Reverse().ToArray(), 0) + 1;


                LoadSRAM();

                //Убедитесь, что SRAM имеет правильный размер. Если файл не загружен, он будет заполнен 0xFF.
                //Если файл был загружен, но был меньше размера SRAM, неинициализированные байты будут 0xFF.
                //Если файл был больше, то размер массива будет ограничен
                if (_config.CartSram.Length > sramSize)
                {
                    byte[] newSram = new byte[sramSize];
                    Buffer.BlockCopy(_config.CartSram, 0, newSram, 0, (int)sramSize); // Обрезаем лишние данные
                    _config.CartSram = newSram;
                }
                else if (_config.CartSram.Length < sramSize)
                {
                    byte[] newSram = new byte[sramSize];
                    Buffer.BlockCopy(_config.CartSram, 0, newSram, 0, _config.CartSram.Length); // Копируем старые данные
                    for (int i = _config.CartSram.Length; i < sramSize; i++)
                    {
                        newSram[i] = 0xFF; // Заполняем оставшиеся байты значением 0xFF
                    }
                    _config.CartSram = newSram;
                }

                //Initialize the emulator and all of its subprojects
                Emu.Initialize(_config);
            }
        }

        private void LoadSRAM()
        {
            // Попытка загрузки SRAM из файла
            string CartSramFilePath = Path.GetFileNameWithoutExtension(_cartName) + ".sav";
            if (!File.Exists(CartSramFilePath))
            {
                Console.WriteLine("Warning: SRAM not found");
            }
            else
            {
                Console.WriteLine("Successfully found SRAM");
                _config.CartSram = File.ReadAllBytes(CartSramFilePath);
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
