using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VibesFitnessStudio
{
    public partial class customerDetails : Form
    {
        public customerDetails()
        {
            InitializeComponent();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void txt_mob_MouseClick(object sender, MouseEventArgs e)
        {
            toolTip1.SetToolTip(this.txt_mob, "Put Your 10 digit Mobile Number");
        }

        public bool IsEmailValid(string email)
        {
            string pattern = "^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$";

            Regex r = new Regex(pattern);
            return r.IsMatch(email);
        }


        public static bool IsPhoneNumberValid(string phone)
        {
            bool isValid = false;
            if (!string.IsNullOrWhiteSpace(phone))
            {
                isValid = Regex.IsMatch(phone, @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                     RegexOptions.IgnoreCase);
            }

            return isValid;

        }
        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_fullname.Text.Trim() != "" && dateTimePicker1_DOB.Text != "" && txt_mob.Text.Trim() != "" && txt_email.Text.Trim() != "" && txt_address.Text.Trim() != "" && comboBox1_batch.Text !="")
                {
                    bool validphone = IsPhoneNumberValid(txt_mob.Text.Trim());
                    string emailAddress = txt_email.Text.Trim();
                    bool isValid=IsEmailValid(emailAddress);
                    if (isValid)
                    {
                        if (txt_mob.Text.Length == 10)
                        {
                            if(validphone==true)
                            {
                                string projectConnection = ConfigurationManager.ConnectionStrings["DevConnection"].ConnectionString;
                                SqlConnection con = new SqlConnection(projectConnection);
                                con.Open();
                                SqlCommand cmd = new SqlCommand("sp_addCustomer", con);
                                cmd.CommandType = CommandType.StoredProcedure;
                                SqlParameter para1 = new SqlParameter("@Customer_Name", SqlDbType.VarChar);
                                cmd.Parameters.Add(para1).Value = txt_fullname.Text.Trim();
                                SqlParameter para2 = new SqlParameter("@Customer_Dob", SqlDbType.Date);
                                cmd.Parameters.Add(para2).Value = dateTimePicker1_DOB.Text;
                                SqlParameter para3 = new SqlParameter("@Customer_Mobilenumber", SqlDbType.VarChar);
                                cmd.Parameters.Add(para3).Value = txt_mob.Text.Trim();
                                SqlParameter para4 = new SqlParameter("@Customer_Emailid", SqlDbType.VarChar);
                                cmd.Parameters.Add(para4).Value = txt_email.Text.Trim();
                                SqlParameter para5 = new SqlParameter("@Customer_Address", SqlDbType.VarChar);
                                cmd.Parameters.Add(para5).Value = txt_address.Text.Trim();
                                SqlParameter para6 = new SqlParameter("@Customer_Batch", SqlDbType.VarChar);
                                cmd.Parameters.Add(para6).Value = comboBox1_batch.SelectedItem.ToString();
                                int i = cmd.ExecuteNonQuery();
                                if (i > 0)
                                {
                                    MessageBox.Show("Customer Details Added Succesfully");
                                    txt_fullname.Clear();
                                    txt_mob.Clear();
                                    txt_email.Clear();
                                    txt_address.Clear();
                                    comboBox1_batch.SelectedIndex = -1;
                                    db_fetchcustomer();

                                }
                                else
                                {
                                    MessageBox.Show("Added Failed");
                                }
                                con.Close();
                            }
                            else
                            {
                                MessageBox.Show("Invalid..! Please check your Mobile Number");
                            }
                            
                        }
                        else
                        {
                            MessageBox.Show("Mobile Number Must have 10 digits");
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Email in Correct Format");
                    }

                    
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

        private void btn_reset_Click(object sender, EventArgs e)
        {
            txt_fullname.Clear();
            dateTimePicker1_DOB.CustomFormat = "";
            txt_mob.Clear();
            txt_email.Clear();
            txt_address.Clear();
            comboBox1_batch.SelectedIndex = -1;
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_fullname.Text.Trim() != "" && dateTimePicker1_DOB.Text != "" && txt_mob.Text.Trim() != "" && txt_email.Text.Trim() != "" && txt_address.Text.Trim() != "" && comboBox1_batch.Text != "")
                {
                    bool validphone = IsPhoneNumberValid(txt_mob.Text.Trim());
                    string emailAddress = txt_email.Text.Trim();
                    bool isValid = IsEmailValid(emailAddress);
                    if (isValid)
                    {
                        if (txt_mob.Text.Length == 10)
                        {
                            if(validphone==true)
                            {
                                string projectConnection = ConfigurationManager.ConnectionStrings["DevConnection"].ConnectionString;
                                SqlConnection con = new SqlConnection(projectConnection);
                                con.Open();
                                SqlCommand cmd = new SqlCommand("sp_updateCustomer", con);
                                cmd.CommandType = CommandType.StoredProcedure;
                                SqlParameter para1 = new SqlParameter("@Customer_Name", SqlDbType.VarChar);
                                cmd.Parameters.Add(para1).Value = txt_fullname.Text.Trim();
                                SqlParameter para2 = new SqlParameter("@Customer_Dob", SqlDbType.Date);
                                cmd.Parameters.Add(para2).Value = dateTimePicker1_DOB.Text;
                                SqlParameter para3 = new SqlParameter("@Customer_Mobilenumber", SqlDbType.VarChar);
                                cmd.Parameters.Add(para3).Value = txt_mob.Text.Trim();
                                SqlParameter para4 = new SqlParameter("@Customer_Emailid", SqlDbType.VarChar);
                                cmd.Parameters.Add(para4).Value = txt_email.Text.Trim();
                                SqlParameter para5 = new SqlParameter("@Customer_Address", SqlDbType.VarChar);
                                cmd.Parameters.Add(para5).Value = txt_address.Text.Trim();
                                SqlParameter para6 = new SqlParameter("@Customer_Batch", SqlDbType.VarChar);
                                cmd.Parameters.Add(para6).Value = comboBox1_batch.SelectedItem.ToString();
                                int i = cmd.ExecuteNonQuery();
                                if (i > 0)
                                {
                                    MessageBox.Show("Customer Details Updated Succesfully");
                                    txt_fullname.Clear();
                                    dateTimePicker1_DOB.Text = "";
                                    txt_mob.Clear();
                                    txt_email.Clear();
                                    txt_address.Clear();
                                    comboBox1_batch.SelectedIndex = -1;
                                    db_fetchcustomer();

                                }
                                else
                                {
                                    MessageBox.Show("Update Failed");
                                }
                                con.Close();
                            }
                            else
                            {
                                MessageBox.Show("Invalid..! Please Check your mobile number");
                            }
                            
                        }
                        else
                        {
                            MessageBox.Show("Mobile Number Must Have 10 digits");
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("Enter Emailid in correct format");
                    }


                    
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
        private void db_fetchcustomer()
        {
            try
            {
                string projectConnection = ConfigurationManager.ConnectionStrings["DevConnection"].ConnectionString;
                SqlConnection con = new SqlConnection(projectConnection);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_fetchCustomer", con);
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
        private void btn_fetch_Click(object sender, EventArgs e)
        {
            try
            {
                string projectConnection = ConfigurationManager.ConnectionStrings["DevConnection"].ConnectionString;
                SqlConnection con = new SqlConnection(projectConnection);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_fetchCustomer", con);
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

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult check = MessageBox.Show("Do you want to Delete Employee details?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (check == DialogResult.Yes)
                {
                    string projectConnection = ConfigurationManager.ConnectionStrings["DevConnection"].ConnectionString;
                    SqlConnection con = new SqlConnection(projectConnection);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_deleteCustomer", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter para1 = new SqlParameter("@Customer_Name", SqlDbType.VarChar);
                    cmd.Parameters.Add(para1).Value = txt_searchcustomer.Text.Trim();
                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        MessageBox.Show("Data Deleted Successfully");
                        db_fetchcustomer();
                        txt_searchcustomer.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Data Not deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                string projectConnection = ConfigurationManager.ConnectionStrings["DevConnection"].ConnectionString;
                SqlConnection con = new SqlConnection(projectConnection);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_searchCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter para1 = new SqlParameter("@searchdata", SqlDbType.VarChar);
                cmd.Parameters.Add(para1).Value = txt_searchcustomer.Text.Trim();
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
                txt_fullname.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                dateTimePicker1_DOB.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                txt_mob.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                txt_email.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                txt_address.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                comboBox1_batch.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void customerDetails_Load(object sender, EventArgs e)
        {
            db_fetchcustomer();
        }
    }
}
