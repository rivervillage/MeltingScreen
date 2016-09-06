using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MeltingScreen
{
    public partial class MainForm : Form
    {

        Bitmap screen;

        public MainForm()
        {
            InitializeComponent();

            

        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            screen = TakeScreenshot();
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
    }
}
