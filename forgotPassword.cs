using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace VibesFitnessStudio
{
    public partial class forgotPassword : Form
    {
        string randomcode;

        public forgotPassword()
        {
            InitializeComponent();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            loginpage loginPage = new loginpage();
            loginPage.Show();
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

        private void btn_sendotp_Click(object sender, EventArgs e)
        {
            string from, pass, messageBody, to;
            Random random = new Random();
            randomcode = (random.Next(999999)).ToString();
            MailMessage message = new MailMessage();
            to = (txt_enteremailid.Text).ToString();
            from = "mjenterprisescheyyur@gmail.com";
            pass = "kimh xgqt qrlg ylus";
            messageBody = "Your OTP verification code is :" + randomcode;
            message.To.Add(to);
            message.From = new MailAddress(from);
            message.Body = messageBody;
            message.Subject = "Vibes Fitness Studio Password Verification";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(from, pass);
            try
            {
                smtp.Send(message);
                MessageBox.Show("OTP sent Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (randomcode == txt_otp.Text.ToString())
                {
                    MessageBox.Show("OTP Verified Succesfully");
                    ChangePassword cp = new ChangePassword();
                    cp.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid OTP");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void forgotPassword_Load(object sender, EventArgs e)
        {

        }
    }
}
