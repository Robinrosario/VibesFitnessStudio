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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VibesFitnessStudio
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
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
        private void btn_register_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (txt_name.Text.Trim() != "" && txt_emailid.Text != "" && txt_mobilenumber.Text.Trim() != "" && txt_username.Text.Trim() != "" && txt_password.Text != "" && txt_cpassword.Text.Trim() != "")
            //    {
            //        bool validphone = IsPhoneNumberValid(txt_mobilenumber.Text);
            //        string emailAddress = txt_emailid.Text;
            //        bool isValid = IsEmailValid(emailAddress);
            //        if (isValid)
            //        {
            //            if (txt_mobilenumber.Text.Length == 10)
            //            {
            //                if (validphone == true)
            //                {
            //                    string projectConnection = ConfigurationManager.ConnectionStrings["DevConnection"].ConnectionString;
            //                    SqlConnection con = new SqlConnection(projectConnection);
            //                    con.Open();
            //                    SqlCommand cmd = new SqlCommand("sp_newuser", con);
            //                    cmd.CommandType = CommandType.StoredProcedure;
            //                    SqlParameter para1 = new SqlParameter("@name", SqlDbType.VarChar);
            //                    cmd.Parameters.Add(para1).Value = txt_name.Text.Trim();
            //                    SqlParameter para2 = new SqlParameter("@emailid", SqlDbType.VarChar);
            //                    cmd.Parameters.Add(para2).Value = txt_emailid.Text;
            //                    SqlParameter para3 = new SqlParameter("@mobilenumber", SqlDbType.VarChar);
            //                    cmd.Parameters.Add(para3).Value = txt_mobilenumber.Text.Trim();
            //                    SqlParameter para4 = new SqlParameter("@username", SqlDbType.VarChar);
            //                    cmd.Parameters.Add(para4).Value = txt_username.Text.Trim();
            //                    SqlParameter para5 = new SqlParameter("@password", SqlDbType.VarChar);
            //                    cmd.Parameters.Add(para5).Value = txt_password.Text.Trim();
            //                    SqlParameter para6 = new SqlParameter("@cpassword", SqlDbType.VarChar);
            //                    cmd.Parameters.Add(para6).Value = txt_cpassword.Text.Trim();
                                
            //                    int i = cmd.ExecuteNonQuery();
            //                    if (i > 0)
            //                    {
            //                        MessageBox.Show("Registered Succesfully");
            //                        loginpage lp= new loginpage();
            //                        lp.Show();
            //                        this.Hide();    
                                   

            //                    }
            //                    else
            //                    {
            //                        MessageBox.Show("Registration Failed");
            //                    }
            //                    con.Close();
            //                }
            //                else
            //                {
            //                    MessageBox.Show("Invalid..! Please Enter Valid mobile number");
            //                }

            //            }
            //            else
            //            {
            //                MessageBox.Show("Mobile number must have 10 digits");
            //            }

            //        }
            //        else
            //        {
            //            MessageBox.Show("Enter emailid in correct format");
            //        }


            //    }
            //    else
            //    {
            //        MessageBox.Show("Please provide all the credentials");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

            try
            {
                if (txt_name.Text.Trim() != "" && txt_emailid.Text != "" && txt_mobilenumber.Text.Trim() != "" && txt_username.Text.Trim() != "" && txt_password.Text != "" && txt_cpassword.Text.Trim() != "")
                {
                    bool validphone = IsPhoneNumberValid(txt_mobilenumber.Text);
                    string emailAddress = txt_emailid.Text;
                    bool isValid = IsEmailValid(emailAddress);
                    if (isValid)
                    {
                        if (txt_mobilenumber.Text.Length == 10)
                        {
                            if (validphone == true)
                            {
                                string projectConnection = ConfigurationManager.ConnectionStrings["DevConnection"].ConnectionString;
                                SqlConnection con = new SqlConnection(projectConnection);
                                con.Open();
                                SqlCommand cmd = new SqlCommand("sp_newuser", con);
                                cmd.CommandType = CommandType.StoredProcedure;
                                SqlParameter para1 = new SqlParameter("@name", SqlDbType.VarChar);
                                cmd.Parameters.Add(para1).Value = txt_name.Text.Trim();
                                SqlParameter para2 = new SqlParameter("@emailid", SqlDbType.VarChar);
                                cmd.Parameters.Add(para2).Value = txt_emailid.Text;
                                SqlParameter para3 = new SqlParameter("@mobilenumber", SqlDbType.VarChar);
                                cmd.Parameters.Add(para3).Value = txt_mobilenumber.Text.Trim();
                                string key = "b14ca5898a4e4133bbce2ea2315a1916";
                                SqlParameter p4 = new SqlParameter("@username", SqlDbType.VarChar);
                                string encrypUsername = EncryptString(key, txt_username.Text);
                                cmd.Parameters.Add(p4).Value = encrypUsername;
                                SqlParameter p5 = new SqlParameter("@password", SqlDbType.VarChar);
                                string encrypsetpassword = EncryptString(key, txt_password.Text);
                                cmd.Parameters.Add(p5).Value = encrypsetpassword;
                                SqlParameter p6 = new SqlParameter("@cpassword", SqlDbType.VarChar);
                                string encrypcpassword = EncryptString(key, txt_cpassword.Text);
                                cmd.Parameters.Add(p6).Value = encrypcpassword;

                                int i = cmd.ExecuteNonQuery();
                                if (i > 0)
                                {
                                    MessageBox.Show("Registered Succesfully");
                                    loginpage lp = new loginpage();
                                    lp.Show();
                                    this.Hide();


                                }
                                else
                                {
                                    MessageBox.Show("Registration Failed");
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

        private void linkLabel1_login_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            loginpage loginpage = new loginpage();
            loginpage.Show();
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

        private void btn_reset_Click(object sender, EventArgs e)
        {
            txt_name.Text = "";
            txt_emailid.Text = "";
            txt_mobilenumber.Text = "";
            txt_username.Text = "";
            txt_password.Text = "";
            txt_cpassword.Text = "";
        }
    }
}
