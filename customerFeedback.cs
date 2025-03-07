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
    public partial class customerFeedback : Form
    {
        public customerFeedback()
        {
            InitializeComponent();
        }

    

        private void btn_logout_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Do you really want to Logout?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (check == DialogResult.Yes)
            {
                MessageBox.Show("Logout Successfull");
                loginpage lp= new loginpage();
                lp.Show();
                this.Hide();
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

        private void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                string projectConnection = ConfigurationManager.ConnectionStrings["DevConnection"].ConnectionString;
                SqlConnection con = new SqlConnection(projectConnection);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_addcusFeedback", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter para1 = new SqlParameter("@Customer_Name", SqlDbType.VarChar);
                cmd.Parameters.Add(para1).Value = txt_fullname.Text.Trim();
                SqlParameter para2 = new SqlParameter("@Customer_Mobilenumber", SqlDbType.VarChar);
                cmd.Parameters.Add(para2).Value = txt_mobilenumber.Text;
                SqlParameter para3 = new SqlParameter("@Customer_Emailid", SqlDbType.VarChar);
                cmd.Parameters.Add(para3).Value = txt_emailid.Text.Trim();
                SqlParameter para4 = new SqlParameter("@Trainer_Name", SqlDbType.VarChar);
                cmd.Parameters.Add(para4).Value = txt_trainername.Text.Trim();
                SqlParameter para5 = new SqlParameter("@Trainer_Id", SqlDbType.VarChar);
                cmd.Parameters.Add(para5).Value = txt_trainerid.Text.Trim();
                SqlParameter para6 = new SqlParameter("@Ratings", SqlDbType.VarChar);
                cmd.Parameters.Add(para6).Value = comboBox1_ratings.SelectedItem.ToString();
                SqlParameter para7 = new SqlParameter("@Describe", SqlDbType.VarChar);
                cmd.Parameters.Add(para7).Value = txt_describefeedback.Text.Trim();

                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Thank you for your valuable Feedback");
                    txt_fullname.Clear();
                    txt_mobilenumber.Clear();
                    txt_emailid.Clear();
                    txt_trainername.Clear();
                    comboBox1_ratings.SelectedIndex = -1;
                    txt_trainerid.Clear();
                    txt_describefeedback.Clear();


                }
                else
                {
                    MessageBox.Show("Feedback Added Failed");
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            txt_fullname.Clear();
            txt_mobilenumber.Clear();
            txt_emailid.Clear();
            txt_trainername.Clear();
            comboBox1_ratings.SelectedIndex = -1;
            txt_trainerid.Clear();
            txt_describefeedback.Clear();
        }

        private void btn_searchid_Click(object sender, EventArgs e)
        {
            try
            {
                string projectConnection = ConfigurationManager.ConnectionStrings["DevConnection"].ConnectionString;
                SqlConnection con = new SqlConnection(projectConnection);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_getTrainerId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter para1 = new SqlParameter("@EmpName", SqlDbType.VarChar);
                cmd.Parameters.Add(para1).Value = txt_trainername.Text.Trim();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                txt_trainerid.Text = ds.Tables[0].Rows[0][0].ToString();
                con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private string name; // field

        public string Name1   // property
        {
            get { return name; }   // get method
            set { name = value; }  // set method
        }
        private void customerFeedback_Load(object sender, EventArgs e)
        {
            txt_fullname.Text = Name1;
        }

        private void btn_fetch_Click(object sender, EventArgs e)
        {
            try
            {
                string projectConnection = ConfigurationManager.ConnectionStrings["DevConnection"].ConnectionString;
                SqlConnection con = new SqlConnection(projectConnection);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_fetchdetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter para1 = new SqlParameter("@name", SqlDbType.VarChar);
                cmd.Parameters.Add(para1).Value = txt_fullname.Text.Trim();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                txt_mobilenumber.Text = ds.Tables[0].Rows[0][0].ToString();
                txt_emailid.Text = ds.Tables[0].Rows[0][1].ToString();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
