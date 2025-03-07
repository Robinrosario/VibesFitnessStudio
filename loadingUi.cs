using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VibesFitnessStudio
{
    public partial class loadingUi : Form
    {
        public loadingUi()
        {
            InitializeComponent();
            circularProgressBar1.Value= 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            circularProgressBar1.Value += 1;
            circularProgressBar1.Text=circularProgressBar1.Value.ToString()+"%";
            if(circularProgressBar1.Value==100 )
            {
                timer1.Enabled = false;
                loginpage lp= new loginpage();
                lp.Show();
                this.Hide();    
            }
        }

        private void loadingUi_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void pictureBox1_exit_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Do you really want to exit?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (check == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void pictureBox2_maxi_Click_1(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
        }

        private void pictureBox3_mini_Click_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void circularProgressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
