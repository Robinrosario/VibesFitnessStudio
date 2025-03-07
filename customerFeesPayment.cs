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
    public partial class customerFeesPayment : Form
    {
        public customerFeesPayment()
        {
            InitializeComponent();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            dashboardPage dp = new dashboardPage(); 
            dp.Show();
            this.Hide();
        }

        private void pictureBox1_exit_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Do you really want to exit?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (check == DialogResult.Yes)
            {
                Application.Exit();
            }
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

        private void pictureBox3_mini_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void txt_amount_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void customerFeesPayment_Load(object sender, EventArgs e)
        {
            txt_amount.Text = "750";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_paynow_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_custname.Text.Trim() != "" && txt_custmob.Text != "" && txt_custemail.Text.Trim() != "" && txt_amount.Text.Trim() != "" && comboBox1_payment.Text.Trim() != "" )
                {

                    string projectConnection = ConfigurationManager.ConnectionStrings["DevConnection"].ConnectionString;
                    SqlConnection con = new SqlConnection(projectConnection);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_payCustomerFees", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter para1 = new SqlParameter("@Customer_Name", SqlDbType.VarChar);
                    cmd.Parameters.Add(para1).Value = txt_custname.Text.Trim();
                    SqlParameter para2 = new SqlParameter("@Customer_Mobilenumber", SqlDbType.VarChar);
                    cmd.Parameters.Add(para2).Value = txt_custmob.Text;
                    SqlParameter para3 = new SqlParameter("@Customer_Emailid", SqlDbType.VarChar);
                    cmd.Parameters.Add(para3).Value = txt_custemail.Text.Trim();
                    SqlParameter para4 = new SqlParameter("@Fees_Amount", SqlDbType.VarChar);
                    cmd.Parameters.Add(para4).Value = txt_amount.Text.Trim();
                    SqlParameter para5 = new SqlParameter("@Payment_Mode", SqlDbType.VarChar);
                    cmd.Parameters.Add(para5).Value = comboBox1_payment.SelectedItem.ToString();
                   
                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        MessageBox.Show(txt_custname.Text+" Fees Paid Succesfully");

                        DialogResult check = MessageBox.Show("Do you want to Print Receipt?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (check == DialogResult.Yes)
                        {
                            FeePaymentSlip i = new FeePaymentSlip();                           
                            i.Show();
                            this.Hide();
                        }
                        else
                        {
                            txt_custname.Clear();
                            txt_custmob.Clear();
                            txt_custemail.Clear();
                            txt_amount.Clear();
                            comboBox1_payment.SelectedIndex = -1;

                        }

                    }
                    else
                    {
                        MessageBox.Show("Payment Failed");
                    }
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Please provide all the credentials");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_fetch_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_fetchdetails_Click(object sender, EventArgs e)
        {
            try
            {
                string projectConnection = ConfigurationManager.ConnectionStrings["DevConnection"].ConnectionString;
                SqlConnection con = new SqlConnection(projectConnection);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_getcustMobEmail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter para1 = new SqlParameter("@Customer_Name", SqlDbType.VarChar);
                cmd.Parameters.Add(para1).Value = txt_custname.Text.Trim();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                txt_custmob.Text = ds.Tables[0].Rows[0][0].ToString();
                txt_custemail.Text = ds.Tables[0].Rows[0][1].ToString();
               


                con.Close();
            }
            catch
            {
                MessageBox.Show("Invalid Emp Name or User not Found");

                txt_custname.Clear();
                txt_custmob.Clear();
                txt_custemail.Clear();
                txt_amount.Clear();
                comboBox1_payment.SelectedIndex = -1;
                

            }
        }

        private void linkLabel1_viewacc_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            viewFeePayment vp = new viewFeePayment();
            vp.Show();
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FeePaymentSlip fp = new FeePaymentSlip();
            fp.Show();
            this.Hide();
        }
    }
}
