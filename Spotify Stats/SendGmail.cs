using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spotify_Stats
{
    public partial class SendGmail : Form
    {

        private GmailService _gmailService;

        string path = "";
        string fileContent = "";


        public SendGmail(string path)
        {
            
            InitializeComponent();

            this.path = path;

            FileContent();


        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            AuthorizeAndInitializeGmail();
            
            panelLogic.Visible = true;
            panelLogin.Visible = false;
        }


        private void AuthorizeAndInitializeGmail()
        {
            string[] scopes = { GmailService.Scope.GmailSend };
            string appName = "My First Project";

            UserCredential credential;

            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            _gmailService = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = appName,
            });
        }
        private void SendEmail(string to, string subject, string body)
        {
            var mailMessage = new MailMessage();
            mailMessage.To.Add(to);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.From = new MailAddress("tu_correo@gmail.com");

            using (var memoryStream = new MemoryStream())
            {
                var smtpClient = new SmtpClient();
                smtpClient.UseDefaultCredentials = false;

                var mimeWriter = new StreamWriter(memoryStream);
                mimeWriter.WriteLine($"To: {to}");
                mimeWriter.WriteLine($"Subject: {subject}");
                mimeWriter.WriteLine("Content-Type: text/plain; charset=utf-8");
                mimeWriter.WriteLine();
                mimeWriter.WriteLine(body);
                mimeWriter.Flush();

                var rawMessage = Convert.ToBase64String(memoryStream.ToArray())
                    .Replace('+', '-')
                    .Replace('/', '_')
                    .Replace("=", "");

                Google.Apis.Gmail.v1.Data.Message message = new Google.Apis.Gmail.v1.Data.Message
                {
                    Raw = rawMessage
                };

                _gmailService.Users.Messages.Send(message, "me").Execute();
                MessageBox.Show("Correo enviado exitosamente.");
            }
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
           
            if (string.IsNullOrEmpty(txtboxTo.Text) || !IsValidEmail(txtboxTo.Text))
            {
                MessageBox.Show("Please Select a valid Email Adress.");
                return;
            }

            SendEmail(txtboxTo.Text, "Hello, Here's the information", fileContent);
            txtboxTo.Text = "";

        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


       public void FileContent()
        {

           
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                MessageBox.Show("File not found or path is empty.");
                return;
            }


             fileContent = File.ReadAllText(path);
            txtboxContent.Text = fileContent;


           



           
        }

    }
}
