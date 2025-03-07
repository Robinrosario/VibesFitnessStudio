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
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace VibesFitnessStudio
{
    public partial class ChangePassword : Form
    {
        public ChangePassword()
        {
            InitializeComponent();
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
            forgotPassword forgotPassword = new forgotPassword();
            forgotPassword.Show();
            this.Hide();
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


        private void btn_setpassword_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_newpassword.Text.Trim() == txt_cpassword.Text.Trim())
                {
                    string projectConnection = ConfigurationManager.ConnectionStrings["DevConnection"].ConnectionString;
                    SqlConnection con = new SqlConnection(projectConnection);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("changepassword_sp", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    string key = "b14ca5898a4e4133bbce2ea2315a1916";
                    SqlParameter p1 = new SqlParameter("@username", SqlDbType.VarChar);
                    string encrypUsername = EncryptString(key, txt_username.Text);
                    cmd.Parameters.Add(p1).Value = encrypUsername;
                    SqlParameter p2 = new SqlParameter("@password", SqlDbType.VarChar);
                    string encrypsetpassword = EncryptString(key, txt_newpassword.Text);
                    cmd.Parameters.Add(p2).Value = encrypsetpassword;
                    SqlParameter p3 = new SqlParameter("@cpassword", SqlDbType.VarChar);
                    string encrypcpassword = EncryptString(key, txt_cpassword.Text);
                    cmd.Parameters.Add(p3).Value = encrypcpassword;

                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Password Changed Successfully");
                        loginpage login = new loginpage();
                        login.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Password update failed");
                        txt_username.Clear();
                        txt_newpassword.Clear();
                        txt_cpassword.Clear();
                    }
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Password Mismatch");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }




        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            txt_username.Text = "";
            txt_newpassword.Text = "";
            txt_cpassword.Text = "";
        }
    }
}