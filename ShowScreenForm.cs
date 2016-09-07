using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MeltingScreen
{
    public partial class ShowScreenForm : Form
    {
        Bitmap screen;

        public ShowScreenForm()
        {
            InitializeComponent();
        }

        public void Start(int index)
        {
            screen = TakeScreenshot(index);
            pictureBox.Image = screen;
            Show();
            Screen[] screens = Screen.AllScreens;
            Location = new Point(screens[index].Bounds.X, screens[index].Bounds.Y);
            Bounds = screens[index].Bounds;
            pictureBox.Location = new Point(0, 0); ;
            pictureBox.Size = new Size(screens[index].Bounds.Width, screens[index].Bounds.Height);

            timer.Enabled = true;
        }

        private Bitmap TakeScreenshot(int index)
        {
            Bitmap[] screenshots = new Bitmap[Screen.AllScreens.Length];
            int i = 0;
            foreach (Screen screen in Screen.AllScreens)
            {
                Bitmap screenshot = new Bitmap(screen.Bounds.Width, screen.Bounds.Height);
                Graphics gfxScreenshot = Graphics.FromImage(screenshot);
                gfxScreenshot.CopyFromScreen(screen.Bounds.X, screen.Bounds.Y, 0, 0, screen.Bounds.Size, CopyPixelOperation.SourceCopy);
                screenshots[i] = screenshot;
                i++;
            }
            return screenshots[index];
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
            for (int k = r; k < r + 50; k++)
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

            ShiftPixels(rnd.Next(1, 5));
            pictureBox.Image = screen;
        }
    }
}
