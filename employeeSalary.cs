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
    public partial class employeeSalary : Form
    {
        public employeeSalary()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
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
            dashboardPage dp= new dashboardPage();
            dp.Show();
            this.Hide();
        }

        private void btn_searchemp_Click(object sender, EventArgs e)
        {

            try
            {
                string projectConnection = ConfigurationManager.ConnectionStrings["DevConnection"].ConnectionString;
                SqlConnection con = new SqlConnection(projectConnection);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_FetchEmpIdMobSalary", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter para1 = new SqlParameter("@EmpName", SqlDbType.VarChar);
                cmd.Parameters.Add(para1).Value = txt_empname.Text.Trim();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                txt_empid.Text = ds.Tables[0].Rows[0][0].ToString();
                txt_empmob.Text= ds.Tables[0].Rows[0][1].ToString();
                txt_emprole.Text= ds.Tables[0].Rows[0][2].ToString();
                txt_basicsalary.Text = ds.Tables[0].Rows[0][3].ToString();


                con.Close();
            }
            catch
            {
                MessageBox.Show("Invalid Emp Name or User not Found");
                
                txt_empid.Clear();
                txt_empmob.Clear();
                txt_emprole.Clear();
                txt_incentive.Clear();
                txt_basicsalary.Clear();
                comboBox1_attendance.SelectedIndex = -1;

            }
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                string projectConnection = ConfigurationManager.ConnectionStrings["DevConnection"].ConnectionString;
                SqlConnection con = new SqlConnection(projectConnection);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_AddEmployeesalary", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter para1 = new SqlParameter("@EmpName", SqlDbType.VarChar);
                cmd.Parameters.Add(para1).Value = txt_empname.Text;
                SqlParameter para2 = new SqlParameter("@EmpId", SqlDbType.VarChar);
                cmd.Parameters.Add(para2).Value = txt_empid.Text;
                SqlParameter para3 = new SqlParameter("@EmpMobile", SqlDbType.VarChar);
                cmd.Parameters.Add(para3).Value = txt_empmob.Text.Trim();
                SqlParameter para4 = new SqlParameter("@EmpRole", SqlDbType.VarChar);
                cmd.Parameters.Add(para4).Value = txt_emprole.Text.Trim();
                SqlParameter para5 = new SqlParameter("@NoOfDays", SqlDbType.Int);
                cmd.Parameters.Add(para5).Value = comboBox1_attendance.SelectedItem.ToString();
                SqlParameter para6 = new SqlParameter("@EmpBasicSalary", SqlDbType.VarChar);
                cmd.Parameters.Add(para6).Value = txt_basicsalary.Text.Trim();
                SqlParameter para7 = new SqlParameter("@EmpIncentiveOT", SqlDbType.VarChar);
                cmd.Parameters.Add(para7).Value = txt_incentive.Text.Trim();
                SqlParameter para8 = new SqlParameter("@TotalSalary", SqlDbType.VarChar);
                cmd.Parameters.Add(para8).Value = txt_totalsalary.Text.Trim();
                SqlParameter para9 = new SqlParameter("@EmpImage", SqlDbType.VarChar);
                cmd.Parameters.Add(para9).Value = pictureBox1_empImage.Image.ToString();

                int a = cmd.ExecuteNonQuery();
                if (a > 0)
                {
                    MessageBox.Show("Employee Salary Added Succesfully");
                    DialogResult check = MessageBox.Show("Do you want to Print Playslip?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (check == DialogResult.Yes)
                    {
                        printInvoice i = new printInvoice();
                        i.img = pictureBox1_empImage.Image;
                        i.name = txt_empname.Text;
                        i.id = txt_empid.Text;
                        i.mob = txt_empmob.Text;
                        i.role = txt_emprole.Text;
                        i.basicsalary = txt_basicsalary.Text;
                        if (comboBox1_attendance.SelectedIndex == -1)
                        {
                            i.days = "";
                        }
                        else
                        {
                            i.days = comboBox1_attendance.SelectedItem.ToString();
                        }
                        i.incentive = txt_incentive.Text;
                        i.total = txt_totalsalary.Text;
                        i.Show();
                        this.Hide();
                    }
                    else
                    {
                        txt_empname.Clear();
                        txt_empid.Clear();
                        txt_empmob.Clear();
                        txt_emprole.Clear();
                        txt_incentive.Clear();
                        txt_basicsalary.Clear();
                        comboBox1_attendance.SelectedIndex = -1;
                        txt_totalsalary.Clear();
                    }


                }
                else
                {
                    MessageBox.Show("Added Failed");
                }
                con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void txt_incentive_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void linkLabel1_viewacc_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            viewEmpSalary vs = new viewEmpSalary();
            vs.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {



        }

        private void btn_browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files |*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            ofd.Title = "Select an Employee Image";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filepath = ofd.FileName;
                pictureBox1_empImage.Image = Image.FromFile(filepath);
            }
        }

        private void btn_generate_Click(object sender, EventArgs e)
        {
            salaryPayslip sp = new salaryPayslip();
            sp.Show();
            this.Hide();
        }

        private void txt_incentive_TextChanged_1(object sender, EventArgs e)
        {

            if (txt_incentive.Text.Length > 0)
            {
                txt_totalsalary.Text = (Convert.ToInt32(txt_basicsalary.Text) + Convert.ToInt32(txt_incentive.Text)).ToString();
            }
        }
    }
}
