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
            for (int i = 0; i < Screen.AllScreens.Length; i++)
            {
                ShowScreenForm ssf = new ShowScreenForm();
                ssf.Start(i, 1);
            }
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
            notifyIcon.Visible = true;
            this.Hide();
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            this.Show();
            notifyIcon.Visible = false;
        }
    }

}
