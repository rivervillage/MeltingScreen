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
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics grphxBmp = Graphics.FromImage(bmp);
            grphxBmp.CopyFromScreen(0, 0, Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, Screen.PrimaryScreen.Bounds.Size);
            pictureBox1.Image = bmp;
            pictureBox1.Refresh();

            //go fullscreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            //resize picturebox to screen bounds
            pictureBox1.Bounds = Screen.PrimaryScreen.Bounds;
        }
    }
}
