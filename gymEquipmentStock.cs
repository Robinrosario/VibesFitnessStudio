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
    public partial class gymEquipmentStock : Form
    {
        public gymEquipmentStock()
        {
            InitializeComponent();
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd= new OpenFileDialog();
            ofd.Filter= "jpg files (*.jpg)|*.jpg|png files (*.png)|*.png";
            ofd.ShowDialog();
            txt_imageupload.Text= ofd.FileName;
            pictureBox1_pictureupload.Image=Image.FromFile(ofd.FileName);
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
         
        }
        private void db_fetchstock()
        {
            try
            {
                string projectConnection = ConfigurationManager.ConnectionStrings["DevConnection"].ConnectionString;
                SqlConnection con = new SqlConnection(projectConnection);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_fetchEquipments", con);
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

        private void gymEquipmentStock_Load(object sender, EventArgs e)
        {
            db_fetchstock();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_equipname.Text.Trim() != "" && txt_quantity.Text.Trim() != "" && txt_priceperunit.Text.Trim() != "" && txt_imageupload.Text.Trim() != "")
                {

                    string projectConnection = ConfigurationManager.ConnectionStrings["DevConnection"].ConnectionString;
                    SqlConnection con = new SqlConnection(projectConnection);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_AddEquipments", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter para1 = new SqlParameter("@Equipment_Name", SqlDbType.VarChar);
                    cmd.Parameters.Add(para1).Value = txt_equipname.Text.Trim();
                    SqlParameter para2 = new SqlParameter("@Quantity", SqlDbType.VarChar);
                    cmd.Parameters.Add(para2).Value = txt_quantity.Text.Trim();
                    SqlParameter para3 = new SqlParameter("@Price_Per_Unit", SqlDbType.VarChar);
                    cmd.Parameters.Add(para3).Value = txt_priceperunit.Text.Trim();
                    SqlParameter para4 = new SqlParameter("@Image_Upload", SqlDbType.VarChar);
                    cmd.Parameters.Add(para4).Value = txt_imageupload.Text.Trim();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Equipment Added Succesfully");
                        txt_equipname.Clear();
                        txt_quantity.Clear();
                        txt_priceperunit.Clear();
                        txt_imageupload.Clear();
                        pictureBox1_pictureupload.Image= null;
                        db_fetchstock();

                    }
                    else
                    {
                        MessageBox.Show("Added Failed");
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

        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_equipname.Text.Trim() != "" && txt_quantity.Text.Trim() != "" && txt_priceperunit.Text.Trim() != "" && txt_imageupload.Text.Trim() != "")
                {

                    string projectConnection = ConfigurationManager.ConnectionStrings["DevConnection"].ConnectionString;
                    SqlConnection con = new SqlConnection(projectConnection);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_UpdateEquipments", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter para1 = new SqlParameter("@Equipment_Name", SqlDbType.VarChar);
                    cmd.Parameters.Add(para1).Value = txt_equipname.Text.Trim();
                    SqlParameter para2 = new SqlParameter("@Quantity", SqlDbType.VarChar);
                    cmd.Parameters.Add(para2).Value = txt_quantity.Text.Trim();
                    SqlParameter para3 = new SqlParameter("@Price_Per_Unit", SqlDbType.VarChar);
                    cmd.Parameters.Add(para3).Value = txt_priceperunit.Text.Trim();
                    SqlParameter para4 = new SqlParameter("@Image_Upload", SqlDbType.VarChar);
                    cmd.Parameters.Add(para4).Value = txt_imageupload.Text.Trim();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Equipment Updated Succesfully");
                        txt_equipname.Clear();
                        txt_quantity.Clear();
                        txt_priceperunit.Clear();
                        txt_imageupload.Clear();
                        pictureBox1_pictureupload.Image = null;
                        db_fetchstock();

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

        private void btn_fetch_Click(object sender, EventArgs e)
        {
            try
            {
                string projectConnection = ConfigurationManager.ConnectionStrings["DevConnection"].ConnectionString;
                SqlConnection con = new SqlConnection(projectConnection);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_fetchEquipments", con);
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

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                string projectConnection = ConfigurationManager.ConnectionStrings["DevConnection"].ConnectionString;
                SqlConnection con = new SqlConnection(projectConnection);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_searchdata", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter para1 = new SqlParameter("@searchdata", SqlDbType.VarChar);
                cmd.Parameters.Add(para1).Value = txt_searchEquip.Text.Trim();
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
                txt_equipname.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                txt_quantity.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                txt_priceperunit.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                txt_imageupload.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                
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
                    SqlCommand cmd = new SqlCommand("sp_deleteEquipment", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter para1 = new SqlParameter("@Equipment_Name", SqlDbType.VarChar);
                    cmd.Parameters.Add(para1).Value = txt_searchEquip.Text.Trim();
                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        MessageBox.Show("Data Deleted Successfully");
                        db_fetchstock();
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
    }
}
