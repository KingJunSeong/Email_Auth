using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace Email_Auth
{
    public partial class Form1 : Form
    {
        public string number = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsVaildEmail(textBox1.Text) == true)
            {
                textBox2.Enabled = true;
            }
            else
            {
                MessageBox.Show("올바른 이메일 형식이 아닙니다.");
            }
        }
        private bool IsVaildEmail(string email)
        {
            bool valid = Regex.IsMatch(email, @"[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?");
            return valid;
        }
        private string GetRandomString()
        {
            string str = string.Empty;
            Random rand = new((int)DateTime.Now.Ticks);
            int[] Random = new int[6];

            for (int i = 0; i < 6; i++)
            {
                Random[i] = (int)rand.Next(0, 10);
            }
            for (int i = 0; i < 6; i++)
            {
                str += Random[i].ToString();
            }

            return str;
        }
        private void SendEmail(string email)
        {
            MailMessage mail = new();
            number = GetRandomString();
            try
            {
                mail.From = new MailAddress("test@test.com", "테스트 인증메일", Encoding.UTF8);
                mail.To.Add(textBox1.Text);
                mail.Subject = "인증메일입니다.";
                mail.Body = $"<html><body>{number}</body></html>";
                mail.IsBodyHtml = true;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.BodyEncoding = Encoding.UTF8;
                SmtpClient SmtpServer = new("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new System.Net.NetworkCredential("아이디", "패스워드"),
                    EnableSsl = true
                };
                SmtpServer.Send(mail);
            }
            finally
            {
                foreach (var attach in mail.Attachments)
                {
                    attach.ContentStream.Close();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox2.Text == number)
            {
                MessageBox.Show("인증되었습니다");
            }
            else
            {
                MessageBox.Show("일치하지 않습니다.");
            }
        }
    }
}