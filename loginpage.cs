using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VibesFitnessStudio
{
    public partial class loginpage : Form
    {
        public loginpage()
        {
            InitializeComponent();
        }

        public static string EncryptString(string key, string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public static string DecryptString(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
           


            try
            {
                string uname =txt_username.Text;
                string pwd =txt_password.Text;
                if (uname.Equals("Admin") || pwd.Equals("admin"))
                {
                    MessageBox.Show("Welcome Admin");
                    dashboardPage dp = new dashboardPage();
                    dp.Show();    
                    this.Hide();
                }
                else
                {
                    if (txt_username.Text.Trim() == "" || txt_password.Text.Trim() == "")
                    {
                        MessageBox.Show("Please provide both Username and Password");

                    }
                    else
                    {

                        string projectConnection = ConfigurationManager.ConnectionStrings["DevConnection"].ConnectionString;
                        SqlConnection con = new SqlConnection(projectConnection);
                        con.Open();
                        SqlCommand cmd = new SqlCommand("sp_loginusers", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter p1 = new SqlParameter("@username", SqlDbType.VarChar);
                        string key = "b14ca5898a4e4133bbce2ea2315a1916";
                        string encrypusername = EncryptString(key, txt_username.Text);
                        cmd.Parameters.Add(p1).Value = encrypusername;
                        SqlParameter p2 = new SqlParameter("@cpassword", SqlDbType.VarChar);
                        string encrypcpassword = EncryptString(key, txt_password.Text);
                        cmd.Parameters.Add(p2).Value = encrypcpassword;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        string username = ds.Tables[0].Rows[0][0].ToString();
                        string decrypusername = DecryptString(key, username);
                        MessageBox.Show("Welcome :" + decrypusername);
                        if (decrypusername != "")
                        {
                            MessageBox.Show("Login Successfull");

                            customerFeedback cf = new customerFeedback();
                            cf.Name1 = txt_username.Text;
                            cf.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Invalid Username or Password");
                        }
                        con.Close();
                    }
                }
               
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void linkLabel1_forgotpassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            forgotPassword fp=new forgotPassword();
            fp.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void loginpage_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_showpassword_CheckedChanged(object sender, EventArgs e)
        {
            txt_password.PasswordChar = checkBox1_showpassword.Checked ? '\0' : '*';
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

        private void linkLabel1_newregister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register rs = new Register();
            rs.Show();
            this.Hide();    
        }
    }
}
