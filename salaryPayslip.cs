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
    public partial class salaryPayslip : Form
    {
        public salaryPayslip()
        {
            InitializeComponent();
        }

        private void btn_generate_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Do you want to Print Playslip?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (check == DialogResult.Yes)
            {
                printInvoice i = new printInvoice();
                i.img = pictureBox_image.Image;
                i.name = txt_name.Text;
                i.id = txt_empid.Text;
                i.mob = txt_mobilenumber.Text;
                i.role = txt_role.Text;
                i.basicsalary = txt_basicsalary.Text;
                i.days = txt_days.Text;
                i.incentive = txt_incentive.Text;
                i.total = txt_totalsalary.Text;
                i.Show();
                this.Hide();
            }
            
        }

        private void btn_browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files |*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            ofd.Title = "Select an Employee Image";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                string filepath=ofd.FileName;
                pictureBox_image.Image=Image.FromFile(filepath);
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

        private void btn_fetch_Click(object sender, EventArgs e)
        {
            try
            {
                string projectConnection = ConfigurationManager.ConnectionStrings["DevConnection"].ConnectionString;
                SqlConnection con = new SqlConnection(projectConnection);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_FetchEmpSalary", con);
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
                txt_name.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                txt_empid.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                txt_mobilenumber.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                txt_role.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                txt_basicsalary.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                txt_days.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                txt_incentive.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
                txt_totalsalary.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
                


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void salaryPayslip_Load(object sender, EventArgs e)
        {

        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            employeeSalary es= new employeeSalary();
            es.Show();
            this.Hide();
        }
    }
}
