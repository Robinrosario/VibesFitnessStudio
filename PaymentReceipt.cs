using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Forms;

namespace VibesFitnessStudio
{
    public partial class PaymentReceipt : Form
    {
        public string date, name, mob, email, amount, payment,paymentdate;

        private void btn_back_Click(object sender, EventArgs e)
        {
            FeePaymentSlip fp = new FeePaymentSlip();
            fp.Show();
            this.Hide();
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            Print(this.panel1_receipt);
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Rectangle pagearea = e.PageBounds;
            e.Graphics.DrawImage(BM, (pagearea.Width / 2) - (this.Width / 2), this.panel1_receipt.Location.Y);
            
            
        }

        private void pictureBox3_mini_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void panel1_receipt_Paint(object sender, PaintEventArgs e)
        {

        }

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

        private void pictureBox1_exit_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Do you really want to exit?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (check == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        public PaymentReceipt()
        {
            InitializeComponent();
            date = DateTime.Now.ToString();
        }
        private  Bitmap BM;
        private void Print(Panel a)
        {
            try
            {
                PrinterSettings p = new PrinterSettings();
                BM = new Bitmap(a.Width, a.Height);
                a.DrawToBitmap(BM, new Rectangle(0, 0, a.Width, a.Height));
                printPreviewDialog1.Document = printDocument1;
                printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                printPreviewDialog1.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void PaymentReceipt_Load(object sender, EventArgs e)
        {
            label7.Text = date;
            label_name.Text = name;
            label_mobnum.Text = mob;
            label_emailid.Text = email;
            label_amount.Text = amount;
            label_paymenttype.Text = payment;
            label_paymentdate.Text = paymentdate;
           
        }
    }
}
