using AutoMapper;
using GrupoEstudosMusical.Email;
using GrupoEstudosMusical.Email.Services.Generic;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.MVC.Models;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace GrupoEstudosMusical.MVC.Controllers
{
    public class NotificacaoController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly IBussinesTurma _bussinesTurma;
        private readonly IBussinesMatricula _bussinesMatricula;

        public NotificacaoController(IEmailService emailService, IBussinesTurma bussinesTurma,
            IBussinesMatricula bussinesMatricula)
        {
            _emailService = emailService;
            _bussinesTurma = bussinesTurma;
            _bussinesMatricula = bussinesMatricula;
        }

        public ActionResult Index()
        {
            var turmasAtivas = _bussinesTurma.ObterTurmasAtivas();
            ViewBag.Turmas = Mapper.Map<IList<Turma>, IList<TurmaVM>>(turmasAtivas);
            return View();
        }

        [HttpPost]
        public ActionResult EnviarEmails(EnvioEmailVM envioEmailVM)
        {
            var success = false;
            envioEmailVM.IdsTurma.ForEach(id =>
            {
                var matriculas = _bussinesMatricula.ObterMatriculasPorTurma(int.Parse(id)).Result;
                var emailsAlunos = new List<string>();
                matriculas.ForEach(matricula => emailsAlunos.Add(matricula.Aluno.Nome));

                var emailMessage = new EmailMessage
                {
                    Subject = envioEmailVM.Assunto,
                    Title = envioEmailVM.Titulo,
                    Body = envioEmailVM.Mensagem,
                    ToEmails = emailsAlunos
                };

                success = _emailService.SendEmailMessage(emailMessage);
            });
            return success ? new HttpStatusCodeResult(HttpStatusCode.OK) :
                new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }
    }
}