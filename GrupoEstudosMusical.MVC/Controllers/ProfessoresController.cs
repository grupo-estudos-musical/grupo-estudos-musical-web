using AutoMapper;
using GrupoEstudosMusical.Email;
using GrupoEstudosMusical.Email.Services.Generic;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Enums;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.MVC.App_Start;
using GrupoEstudosMusical.MVC.Helpers;
using GrupoEstudosMusical.MVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GrupoEstudosMusical.MVC.Controllers
{
    [AuthorizeGem]
    public class ProfessoresController : Controller
    {
        private readonly IBussinesProfessor _bussinesProfessor;
        private readonly IBussinesUsuario _bussinesUsuario;
        private readonly IEmailService _emailService;

        public ProfessoresController(IBussinesProfessor bussinesProfessor, IBussinesUsuario bussinesUsuario, IEmailService emailService)
        {
            _bussinesProfessor = bussinesProfessor;
            _bussinesUsuario = bussinesUsuario;
            _emailService = emailService;
        }

        public async Task<ActionResult> Index()
        {
            IList<Professor> professores = new List<Professor>();
            if (Session["nivelAcesso"]?.ToString() == "Professor")
                professores = await _bussinesProfessor.ObterTodosPorUsuario(int.Parse(Session["idUsuario"].ToString()));
            else
                professores = await _bussinesProfessor.ObterTodosAsync();
            var professoresVM = Mapper.Map<IList<Professor>, IList<ProfessorVM>>(professores);
            return View(professoresVM);
        }

        public ActionResult Novo()
        {
            return View(new ProfessorVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Novo(ProfessorVM professorVM)
        {
            try
            {
                var usuarioModel = new Usuario
                {
                    Nome = professorVM.Nome,
                    Email = professorVM.Email,
                    NivelAcesso = NivelAcessoEnum.Professor
                };

                var professorModel = Mapper.Map<ProfessorVM, Professor>(professorVM);
                var senhaProfessor = await _bussinesProfessor.InserirAsync(professorModel, usuarioModel);

                usuarioModel = await _bussinesUsuario.ObterPorIdAsync(professorModel.UsuarioId);
                
                var tokenHelper = new TokenHelper();
                var token = tokenHelper.GenerateToken(usuarioModel);

                var link = Url.Action("ResetarSenha", "Usuarios", new { token = token, usuarioId = usuarioModel.Id }, Request.Url.Scheme);

                EnviarEmailProfessor(professorVM, link);

                TempData["Mensagem"] = "Professor Cadastrado com Sucesso.";
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View(professorVM);
            }
        }

        private void EnviarEmailProfessor(ProfessorVM professorVM, string link)
        {
            var nomeProfessor = professorVM.Nome.Split(' ')[0];
            _emailService.SendEmailMessage(new EmailMessage
            {
                Subject = "Bem-Vindo ao GEM",
                Title = $"Seja Bem-Vindo ao GEM {nomeProfessor}",
                Body = $@"Olá {nomeProfessor}, você acaba de entrar para o time de instrutores do Grupo de Estudos Musical.
                            Desejamos boa sorte a você e aproveite muito está aportunidade.<br/> Para começar, Para começar, <a href='{link}'>Clique Aqui</a> para definir sua senha de acesso
                            a área do professor.
                            Aqui você poderá consultar suas informações pessoais, administrar suas turmas, lançar notas, faltas etc.",
                ToEmails = new List<string> { professorVM.Email }
            });
        }

        public async Task<ActionResult> Editar(int id)
        {
            var professorVM = Mapper.Map<Professor, ProfessorVM>(await _bussinesProfessor.ObterPorIdAsync(id));
            if (professorVM == null)
            {
                return HttpNotFound();
            }
            return View(professorVM);
        }

        public async Task<ActionResult> PortalProfessor(int professorID)
        {
            var professorLogado = Mapper.Map<Professor,ProfessorVM>(await _bussinesProfessor.ObterPorIdAsync(professorID));
            if (professorLogado == null)
                return HttpNotFound("Professor não encontrado");
            return View(professorLogado);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(ProfessorVM professorVM)
        {
            try
            {
                var professorModel = Mapper.Map<ProfessorVM, Professor>(professorVM);
                await _bussinesProfessor.AlterarAsync(professorModel);
                TempData["Mensagem"] = "Professor Alterado com Sucesso.";
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View(professorVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Deletar(FormCollection formCollection)
        {
            int.TryParse(formCollection["id"].ToString(), out var id);
            var professorModel = await _bussinesProfessor.ObterPorIdAsync(id);
            await _bussinesProfessor.DeletarAsync(professorModel);
            TempData["Mensagem"] = "Professor Apagado com Sucesso.";
            return RedirectToAction(nameof(Index));
        }
    }
}