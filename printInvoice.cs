using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VibesFitnessStudio
{
    public partial class printInvoice : Form
    {
        public string date, name, id, mob, role, basicsalary, days, incentive, total;

        private void pictureBox2_maxi_Click(object sender, EventArgs e)
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

        private void pictureBox3_mini_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            employeeSalary ep = new employeeSalary();
            ep.Show();
            this.Hide();
        }

        private void panel1_print_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_exit_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Do you really want to exit?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (check == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            Print(this.panel1_print);
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Rectangle pagearea = e.PageBounds;
            e.Graphics.DrawImage(BM,(pagearea.Width/2)-(this.Width/2),this.panel1_print.Location.Y);
        }

        public Image img = null;
        public printInvoice()
        {
            InitializeComponent();
            date = DateTime.Now.ToString();
        }
        private void Print(Panel a)
        {
            try
            {
                PrinterSettings p = new PrinterSettings();
                panel1_print = a;
                /* Bitmap*/
                BM = new Bitmap(a.Width, a.Height);
                a.DrawToBitmap(BM, new Rectangle(0, 0, a.Width, a.Height));
                printPreviewDialog1.Document = printDocument1;
                printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                printPreviewDialog1.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
            
        }
        private Bitmap BM;

        private void printInvoice_Load(object sender, EventArgs e)
        {
            label23.Text = date;
            pictureBox2_img.Image = img;
            label_name.Text = name;
            label_id.Text = id;
            label_mob.Text = mob;
            label_role.Text = role;
            label_basicsalary.Text = basicsalary;
            label_days.Text = days;
            label_incentive.Text = incentive;
            label_totalsalary.Text = total;
        }
    }
}
