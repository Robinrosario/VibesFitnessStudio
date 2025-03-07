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
    public partial class employeePage : Form
    {
        public employeePage()
        {
            InitializeComponent();
        }
        private void db_fetchemp()
        {
            try
            {
                string projectConnection = ConfigurationManager.ConnectionStrings["DevConnection"].ConnectionString;
                SqlConnection con = new SqlConnection(projectConnection);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_FetchEmpDetails", con);
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
                SqlCommand cmd = new SqlCommand("sp_FetchEmpDetails", con);
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

        private void btn_back_Click(object sender, EventArgs e)
        {
            dashboardPage dp= new dashboardPage();
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
                if (txt_empname.Text.Trim() != "" && txt_empid.Text != "" && dateTimePicker1_dob.Text.Trim() != "" && txt_mob.Text.Trim() != "" && txt_email.Text.Trim() != "" && txt_address.Text.Trim() != "" && comboBox1_emprole.Text != "" && txt_basicsalary.Text.Trim() != "")
                {
                    bool validphone = IsPhoneNumberValid(txt_mob.Text);
                    string emailAddress = txt_email.Text;
                    bool isValid = IsEmailValid(emailAddress);
                    if(isValid)
                    {
                        if (txt_mob.Text.Length == 10)
                        {
                            if(validphone==true)
                            {
                                string projectConnection = ConfigurationManager.ConnectionStrings["DevConnection"].ConnectionString;
                                SqlConnection con = new SqlConnection(projectConnection);
                                con.Open();
                                SqlCommand cmd = new SqlCommand("sp_AddEmployeeDetails", con);
                                cmd.CommandType = CommandType.StoredProcedure;
                                SqlParameter para1 = new SqlParameter("@EmpName", SqlDbType.VarChar);
                                cmd.Parameters.Add(para1).Value = txt_empname.Text.Trim();
                                SqlParameter para2 = new SqlParameter("@EmpId", SqlDbType.VarChar);
                                cmd.Parameters.Add(para2).Value = txt_empid.Text;
                                SqlParameter para3 = new SqlParameter("@EmpDob", SqlDbType.Date);
                                cmd.Parameters.Add(para3).Value = dateTimePicker1_dob.Text.Trim();
                                SqlParameter para4 = new SqlParameter("@EmpMobile", SqlDbType.VarChar);
                                cmd.Parameters.Add(para4).Value = txt_mob.Text.Trim();
                                SqlParameter para5 = new SqlParameter("@EmpEmail", SqlDbType.VarChar);
                                cmd.Parameters.Add(para5).Value = txt_email.Text.Trim();
                                SqlParameter para6 = new SqlParameter("@EmpAddress", SqlDbType.VarChar);
                                cmd.Parameters.Add(para6).Value = txt_address.Text.Trim();
                                SqlParameter para7 = new SqlParameter("@EmpRole", SqlDbType.VarChar);
                                cmd.Parameters.Add(para7).Value = comboBox1_emprole.SelectedItem.ToString();
                                SqlParameter para8 = new SqlParameter("@EmpBasicSalary", SqlDbType.VarChar);
                                cmd.Parameters.Add(para8).Value = txt_basicsalary.Text.Trim();
                                int i = cmd.ExecuteNonQuery();
                                if (i > 0)
                                {
                                    MessageBox.Show("Employee Details Added Succesfully");
                                    txt_empname.Clear();
                                    txt_empid.Clear();
                                    txt_mob.Clear();
                                    txt_email.Clear();
                                    txt_address.Clear();
                                    txt_basicsalary.Clear();
                                    comboBox1_emprole.SelectedIndex = -1;
                                    db_fetchemp();

                                }
                                else
                                {
                                    MessageBox.Show("Added Failed");
                                }
                                con.Close();
                            }
                            else
                            {
                                MessageBox.Show("Invalid..! Please Enter Valid mobile number");
                            }
                            
                        }
                        else
                        {
                            MessageBox.Show("Mobile number must have 10 digits");
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("Enter emailid in correct format");
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

        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_empname.Text.Trim() != "" && txt_empid.Text != "" && dateTimePicker1_dob.Text.Trim() != "" && txt_mob.Text.Trim() != "" && txt_email.Text.Trim() != "" && txt_address.Text.Trim() != "" && comboBox1_emprole.Text != "" && txt_basicsalary.Text.Trim() != "")
                {

                    string projectConnection = ConfigurationManager.ConnectionStrings["DevConnection"].ConnectionString;
                    SqlConnection con = new SqlConnection(projectConnection);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_UpdateEmployeeDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter para1 = new SqlParameter("@EmpName", SqlDbType.VarChar);
                    cmd.Parameters.Add(para1).Value = txt_empname.Text.Trim();
                    SqlParameter para2 = new SqlParameter("@EmpId", SqlDbType.VarChar);
                    cmd.Parameters.Add(para2).Value = txt_empid.Text;
                    SqlParameter para3 = new SqlParameter("@EmpDob", SqlDbType.Date);
                    cmd.Parameters.Add(para3).Value = dateTimePicker1_dob.Text.Trim();
                    SqlParameter para4 = new SqlParameter("@EmpMobile", SqlDbType.VarChar);
                    cmd.Parameters.Add(para4).Value = txt_mob.Text.Trim();
                    SqlParameter para5 = new SqlParameter("@EmpEmail", SqlDbType.VarChar);
                    cmd.Parameters.Add(para5).Value = txt_email.Text.Trim();
                    SqlParameter para6 = new SqlParameter("@EmpAddress", SqlDbType.VarChar);
                    cmd.Parameters.Add(para6).Value = txt_address.Text.Trim();
                    SqlParameter para7 = new SqlParameter("@EmpRole", SqlDbType.VarChar);
                    cmd.Parameters.Add(para7).Value = comboBox1_emprole.SelectedItem.ToString();
                    SqlParameter para8 = new SqlParameter("@EmpBasicSalary", SqlDbType.VarChar);
                    cmd.Parameters.Add(para8).Value = txt_basicsalary.Text.Trim();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Employee Details Updated Succesfully");
                        txt_empname.Clear();
                        txt_empid.Clear();
                        txt_mob.Clear();
                        txt_email.Clear();
                        txt_address.Clear();
                        txt_basicsalary.Clear();
                        comboBox1_emprole.SelectedIndex = -1;
                        db_fetchemp();

                    }
                    else
                    {
                        MessageBox.Show("Update Failed");
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

        private void btn_reset_Click(object sender, EventArgs e)
        {
            txt_empname.Clear();
            txt_empid.Clear();
            txt_mob.Clear();
            txt_email.Clear();
            txt_address.Clear();
            txt_basicsalary.Clear();
            comboBox1_emprole.SelectedIndex = -1;
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
                    SqlCommand cmd = new SqlCommand("sp_DeleteEmployeeData", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter para1 = new SqlParameter("@EmpName", SqlDbType.VarChar);
                    cmd.Parameters.Add(para1).Value = txt_searchemployee.Text.Trim();
                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        MessageBox.Show("Data Deleted Successfully");
                        db_fetchemp();
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
                SqlCommand cmd = new SqlCommand("sp_SearchEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter para1 = new SqlParameter("@searchdata", SqlDbType.VarChar);
                cmd.Parameters.Add(para1).Value = txt_searchemployee.Text.Trim();
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
                txt_empname.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                txt_empid.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                dateTimePicker1_dob.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                txt_mob.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                txt_email.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                txt_address.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                comboBox1_emprole.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
                txt_basicsalary.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void employeePage_Load(object sender, EventArgs e)
        {
            db_fetchemp();
        }
    }
}
