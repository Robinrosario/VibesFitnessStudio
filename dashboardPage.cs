using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VibesFitnessStudio
{
    public partial class dashboardPage : Form
    {
        public dashboardPage()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_empdetails_Click(object sender, EventArgs e)
        {
            employeePage ep = new employeePage();
            ep.Show();
            this.Hide();
        }

        private void CUSTOMER_Click(object sender, EventArgs e)
        {
            customerDetails cd= new customerDetails();
            cd.Show();
            this.Hide();
        }

        private void btn_viewfeedback_Click(object sender, EventArgs e)
        {
            viewCustomerfeedback vcf = new viewCustomerfeedback();
            vcf.Show();
            this.Hide();
        }

        private void btn_equipmentdetails_Click(object sender, EventArgs e)
        {
            gymEquipmentStock gymEquipmentStock = new gymEquipmentStock();
            gymEquipmentStock.Show();
            this.Hide();
        }


       

        private void dashboardPage_Load(object sender, EventArgs e)
        {
            btn_employee_Click(sender, e);

            


        }

        private void btn_employee_Click(object sender, EventArgs e)
        {
            groupBox1_employee.Visible = true;
            groupBox1_customer.Visible = false;
            groupBox1_stock.Visible = false;
            groupBox1_accounts.Visible = false;
            groupBox1_user.Visible = false;
        }

        private void btn_customer_Click(object sender, EventArgs e)
        {
            groupBox1_employee.Visible = false;
            groupBox1_customer.Visible = true;
            groupBox1_stock.Visible = false;
            groupBox1_accounts.Visible = false;
            groupBox1_user.Visible = false;
        }

        private void btn_stock_Click(object sender, EventArgs e)
        {
            groupBox1_employee.Visible = false;
            groupBox1_customer.Visible = false;
            groupBox1_stock.Visible = true;
            groupBox1_accounts.Visible = false;
            groupBox1_user.Visible = false;
        }

        private void btn_accounts_Click(object sender, EventArgs e)
        {
            groupBox1_employee.Visible = false;
            groupBox1_customer.Visible = false;
            groupBox1_stock.Visible = false;
            groupBox1_accounts.Visible = true;
            groupBox1_user.Visible = false;
        }

        private void btn_user_Click(object sender, EventArgs e)
        {
            groupBox1_employee.Visible = false;
            groupBox1_customer.Visible = false;
            groupBox1_stock.Visible = false;
            groupBox1_accounts.Visible = false;
            groupBox1_user.Visible = true;
        }

        private void btn_viewuser_Click(object sender, EventArgs e)
        {
            viewUser vu = new viewUser();
            vu.Show();
            this.Hide();
        }

        private void btn_empsalary_Click(object sender, EventArgs e)
        {
            employeeSalary es = new employeeSalary();
            es.Show();
            this.Hide();
        }

        private void btn_feepayment_Click(object sender, EventArgs e)
        {
            customerFeesPayment feesPayment = new customerFeesPayment();
            feesPayment.Show();
            this.Hide();
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            try
            {
                DialogResult check = MessageBox.Show("Do you really want to Logout?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (check == DialogResult.Yes)
                {
                    MessageBox.Show("Logout Successfully");
                    loginpage loginPage = new loginpage();
                    loginPage.Show();
                    this.Hide();
                }
            }
           catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
