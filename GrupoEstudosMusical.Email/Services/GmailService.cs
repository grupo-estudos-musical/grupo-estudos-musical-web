﻿using GrupoEstudosMusical.Email.Services.Generic;
using Newtonsoft.Json;
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
        }

        private SmtpClient ConfigureClientSmtp()
        {
            var smtpClient = new SmtpClient
            {
                Host = "smtpi.gemcosmopolita.com.br",
                Port = 587,
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };

            smtpClient.Credentials = new NetworkCredential(_userName, _password);
            return smtpClient;
        }

        private void ObterCredenciais()
        {
            var definition = new { gmail = new { address = "", password = "" } };
            var pathCredentials = Path.Combine(_pathApplication, "ConfiguracaoEmail\\credentials.json");
            using (var stream = new StreamReader(pathCredentials))
            {
                var json = stream.ReadToEndAsync().Result;
                var credentials = JsonConvert.DeserializeAnonymousType(json, definition);
                _userName = credentials.gmail.address;
                _password = credentials.gmail.password;
            }
        }

        private string CarregarMensagemHtml()
        {
            var pathTemplate = Path.Combine(_pathApplication, "ConfiguracaoEmail\\template.html");
            using (var stream = new StreamReader(pathTemplate))
            {
                return stream.ReadToEndAsync().Result;
            }
        }

        public void SendEmailMessage(EmailMessage emailMessage)
        {
            using (var client = ConfigureClientSmtp())
            {
                var html = CarregarMensagemHtml();
                html = html.Replace("@titulo", emailMessage.Title);
                html = html.Replace("@mensagem", emailMessage.Body);
                html = html.Replace("@preview_text", $"{emailMessage.Title} - {emailMessage.Body}");

                var mailMessage = new MailMessage
                {
                    Subject = emailMessage.Subject,
                    Body = html,
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8,
                    Priority = MailPriority.High
                    
                };
                ConverterAnexos(mailMessage, emailMessage);

                mailMessage.Headers.Add("Importance", "High");
                mailMessage.From = new MailAddress(_userName);
                emailMessage.ToEmails.ForEach(email => mailMessage.To.Add(email));

                client.Send(mailMessage);
            }
        }

        private void ConverterAnexos(MailMessage mailMessage, EmailMessage emailMessage)
        {
            if(emailMessage != null && emailMessage.Anexos.Count > 0)
                foreach (var anexo in emailMessage.Anexos)
                {
                    MemoryStream anexoEmMemoria = new MemoryStream(anexo.Arquivo);

                    mailMessage.Attachments.Add(new Attachment(anexoEmMemoria, anexo.NomeDoArquivo));
                }
        }
    }
}
