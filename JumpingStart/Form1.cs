using System;
using System.Drawing;
using System.Windows.Forms;

namespace JumpingStart
{
    public partial class Form1 : Form
    {

        enum WinVersions { XP, Vista, Win7, Win8, Win8_1, Win10 };
        WinVersions version;

        void SetWinVersion()
        {
            int majorVer = Environment.OSVersion.Version.Major;
            int minorVer = Environment.OSVersion.Version.Minor;
            switch (majorVer)
            {
                case 5: version = WinVersions.XP; break;
                case 6:
                    switch (minorVer)
                    {
                        case 0: version = WinVersions.Vista; break;
                        case 1: version = WinVersions.Win7; break;
                        case 2: version = WinVersions.Win8; break;
                        case 3: version = WinVersions.Win8_1; break;
                        case 4: version = WinVersions.Win10; break;
                        default: throw new NotImplementedException("Версия виндоуз не поддерживается");
                    }
                    break;
                case 10: version = WinVersions.Win10; break;
                default: throw new NotImplementedException("Версия виндоуз не поддерживается");
            }
        }

        void AddImage()
        {

            switch (version)
            {
                case WinVersions.XP: pictureBox1.Image = Properties.Resources._2048457; break;
                case WinVersions.Vista: pictureBox1.Image = Properties.Resources.windows_7_windows_vista_logo_microsoft_windows_logos; break;
                case WinVersions.Win7: pictureBox1.Image = Properties.Resources.windows_7_windows_vista_logo_microsoft_windows_logos; break;
                case WinVersions.Win8: pictureBox1.Image = Properties.Resources.Windows_10; break;
                case WinVersions.Win8_1: pictureBox1.Image = Properties.Resources.Windows_10; break;
                case WinVersions.Win10: pictureBox1.Image = Properties.Resources.Windows_10; break;
                default: throw new NotImplementedException("Версия виндоуз не поддерживается");
            }
        }

        public Form1()
        {
            InitializeComponent();
            try
            {
                SetWinVersion();
                AddImage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n Приложение будет закрыто");
                Application.Exit();
            }
            InvisibleForm();
        }

        void InvisibleForm()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.AllowTransparency = true;
            this.TransparencyKey = this.BackColor;
            this.DesktopLocation = new Point(10, 700);
            this.StartPosition = FormStartPosition.WindowsDefaultLocation;
            this.TopMost = true;
            this.ShowInTaskbar = false;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            this.Left = 5;
            this.Top = MaxTop;
        }
        static int MaxTop = Screen.PrimaryScreen.Bounds.Height - 40;//самый низ - макс
        static int MinTop = MaxTop - 300;//самый верх - мин
        bool Moving = false;
        int MovintSign = -1;//-1 вверх, 1 - вниз
        int MovStep = 2;
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text = "" + this.Top;
            if (!Moving)
                return;
            this.Top += MovintSign * MovStep;
            if (this.Top >= MaxTop)//пришли вниз
            {
                Moving = false;//закончили движение
                timer1.Stop();
                MovintSign = -1;//вверх потом пойдем
            }
            if (this.Top <= MinTop)//пришли в верх
            {
                MovintSign = 1;//пойдем вниз
            }

        }
        void StartMoving()
        {
            timer1.Start();
            Moving = true;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            if (!Moving)
                StartMoving();
        }
    }
}
