using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MeltingScreen
{
    public partial class MainForm : Form
    {

        Bitmap screen;

        //make borderless window movable
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public MainForm()
        {
            InitializeComponent();

            

        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            screen = TakeScreenshot(0);
            pictureBox.Image = screen;

            //go fullscreen;
            SetFullscreen(true);

            //resize picturebox to screen bounds
            pictureBox.Bounds = Screen.PrimaryScreen.Bounds;

            //enable timer
            EnableTimer(true);

        }

        private void SetFullscreen(bool b)
        {
            if (b)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
            }
        }

        private Bitmap TakeScreenshot()
        {
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics grphxBmp = Graphics.FromImage(bmp);
            grphxBmp.CopyFromScreen(0, 0, Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, Screen.PrimaryScreen.Bounds.Size);
            return bmp;
        }

        private Bitmap TakeScreenshot(int index)
        {
            Bitmap[] screenshots = new Bitmap[Screen.AllScreens.Length];
            int i = 0;
            foreach (Screen screen in Screen.AllScreens)
            {
                Bitmap screenshot = new Bitmap(screen.Bounds.Width, screen.Bounds.Height);
                Graphics gfxScreenshot = Graphics.FromImage(screenshot);
                gfxScreenshot.CopyFromScreen( screen.Bounds.X, screen.Bounds.Y, 0, 0, screen.Bounds.Size, CopyPixelOperation.SourceCopy);
                screenshots[i] = screenshot;
                i++;
            }
            return screenshots[index];
        }

        private void EnableTimer(bool b)
        {
            timer.Enabled = b;
        }

        /// <summary>
        /// Shifts stepSize amount of pixels downwards.
        /// </summary>
        /// <param name="stepSize">How much pixels to shift.</param>
        private void ShiftPixels(int stepSize)
        {
            Random rnd = new Random();

            int r = rnd.Next(0, 1920 - 50);

            Color topColor = screen.GetPixel(0, 0);
            for (int k = r; k < r+50; k++)
            {
                for (int i = 1079; i > 0; i--)
                {
                    if (i - stepSize < 0)
                    {
                        screen.SetPixel(k, i, screen.GetPixel(k, 0));
                    }
                    else
                    {
                        screen.SetPixel(k, i, screen.GetPixel(k, i - stepSize));
                    }
                }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {


            Random rnd = new Random();

            ShiftPixels(rnd.Next(1,5));
            pictureBox.Image = screen;
        }

        private int RNG()
        {
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            Byte[] data = new Byte[2];
            rng.GetNonZeroBytes(data);
            int value = Math.Abs(BitConverter.ToInt16(data, 0));
            return value;
        }

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Color gray = Color.FromArgb(65, 64, 59);
            Color orange = Color.FromArgb(233, 117, 68);

            var combo = sender as ComboBox;

            

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(orange), e.Bounds);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(gray), e.Bounds);
            }

            if (e.Index != -1)
            {
                e.Graphics.DrawString(combo.Items[e.Index].ToString(),
                                              e.Font,
                                              new SolidBrush(SystemColors.ControlLightLight),
                                              new Point(e.Bounds.X, e.Bounds.Y));
            }
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.BackgroundImage = MeltingScreen.Properties.Resources.window_controls_close_hover;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackgroundImage = MeltingScreen.Properties.Resources.window_controls_close_normal;
        }

        private void btnClose_MouseClick(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }

        private void btnMax_MouseEnter(object sender, EventArgs e)
        {
            btnMax.BackgroundImage = MeltingScreen.Properties.Resources.window_controls_max_hover;
        }

        private void btnMax_MouseLeave(object sender, EventArgs e)
        {
            btnMax.BackgroundImage = MeltingScreen.Properties.Resources.window_controls_max_normal;
        }

        private void btnMax_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void lblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void btnMin_MouseEnter(object sender, EventArgs e)
        {
            btnMin.BackgroundImage = MeltingScreen.Properties.Resources.window_controls_min_hover;
        }

        private void btnMin_MouseLeave(object sender, EventArgs e)
        {
            btnMin.BackgroundImage = MeltingScreen.Properties.Resources.window_controls_min_normal;
        }

        private void btnMin_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}
