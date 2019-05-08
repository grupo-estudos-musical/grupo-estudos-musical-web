using GrupoEstudosMusical.Email.Services.Generic;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace GrupoEstudosMusical.Email.Services
{
    public class GmailService : IEmailService
    {
        private readonly string _pathApplication;
        private string _userName;
        private string _password;

        public GmailService(string pathApplication)
        {
            _pathApplication = pathApplication;
            ObterCredenciais();
            //_userName = credentials["email"];
            //_password = credentials["password"];
        }

        private SmtpClient ConfigureClientSmtp()
        {
            var smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };

            smtpClient.Credentials = new NetworkCredential(_userName, _password);
            return smtpClient;
        }

        private void ObterCredenciais()
        {
            var definition = new { gmail = new { email = "", password = "" } };
            var pathCredentials = Path.Combine(_pathApplication, "credentials.json");
            using (var stream = new StreamReader(pathCredentials))
            {
                var json = stream.ReadToEndAsync().Result;
                var credentials = JsonConvert.DeserializeAnonymousType(json, definition);
                _userName = credentials.gmail.email;
                _password = credentials.gmail.password;
            }
        }

        public bool SendEmailMessage(EmailMessage emailMessage)
        {
            try
            {
                using (var client = ConfigureClientSmtp())
                {
                    var mailMessage = new MailMessage
                    {
                        Subject = emailMessage.Subject,
                        Body = emailMessage.Body,
                        BodyEncoding = Encoding.UTF8
                    };

                    mailMessage.From = new MailAddress(_userName);
                    emailMessage.ToEmails.ForEach(email => mailMessage.To.Add(email));
            
                    client.Send(mailMessage);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
