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
    public partial class MainForm : Form
    {


        public MainForm()
        {
            InitializeComponent();
            

        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            pictureBox.Image = TakeScreenshot();

            //go fullscreen;
            SetFullscreen(true);

            //resize picturebox to screen bounds
            pictureBox.Bounds = Screen.PrimaryScreen.Bounds;
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
    }
}
