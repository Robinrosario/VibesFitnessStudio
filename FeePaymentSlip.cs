using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VibesFitnessStudio
{
    public partial class FeePaymentSlip : Form
    {
        public FeePaymentSlip()
        {
            InitializeComponent();
        }

        private void btn_fetch_Click(object sender, EventArgs e)
        {
            try
            {
                string projectConnection = ConfigurationManager.ConnectionStrings["DevConnection"].ConnectionString;
                SqlConnection con = new SqlConnection(projectConnection);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_FetchCustomerFees", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                txt_custname.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                txt_custmob.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                txt_custemail.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                txt_amount.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                txt_paymentmode.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                txt_paymentdate.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_generate_Click(object sender, EventArgs e)
        {
            PaymentReceipt i = new PaymentReceipt();
            i.name = txt_custname.Text;
            i.mob = txt_custmob.Text;
            i.email = txt_custemail.Text;
            i.amount = txt_amount.Text;
            i.payment = txt_paymentmode.Text;
            i.paymentdate = txt_paymentdate.Text;
            i.Show();
            this.Hide();
        }

        private void pictureBox3_mini_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
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

        private void btn_back_Click(object sender, EventArgs e)
        {
            customerFeesPayment cp = new customerFeesPayment();
            cp.Show();
            this.Hide();
        }
    }
}
