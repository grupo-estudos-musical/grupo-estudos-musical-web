using GrupoEstudosMusical.Email;
using GrupoEstudosMusical.Email.Services.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GrupoEstudosMusical.MVC.Controllers
{
    public class NotificacaoController : Controller
    {
        private readonly IEmailService _emailService;

        public NotificacaoController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        // GET: Notificacao
        public ActionResult Index()
        {
            _emailService.SendEmailMessage(new EmailMessage
            {
                Subject = "Teste assunto",
                Title = "Teste Título",
                Body = "Teste corpo da mensagem",
                ToEmails = new List<string> { "nicolassilva114@gmail.com" }
            });
            return View();
        }
    }
}