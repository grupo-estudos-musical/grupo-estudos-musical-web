using AutoMapper;
using GrupoEstudosMusical.Email;
using GrupoEstudosMusical.Email.Services.Generic;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Enums;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.MVC.Helpers;
using GrupoEstudosMusical.MVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GrupoEstudosMusical.MVC.Controllers
{
    public class AlunosController : Controller
    {
        private readonly IBussinesAluno _bussinesAluno;
        private readonly IEmailService _emailService;

        public AlunosController(IBussinesAluno bussinesAluno, IEmailService emailService)
        {
            _bussinesAluno = bussinesAluno;
            _emailService = emailService;
        }

        public async Task<ActionResult> Index()
        {
            var alunosViewModel = Mapper.Map<IList<Aluno>, IList<AlunoVM>>(await _bussinesAluno.ObterTodosAsync());
            return View(alunosViewModel);
        }

        public ActionResult Novo()
        {
            ViewBag.TiposResponsaveis = Enum.GetValues(typeof(TipoResponsavelEnum));
            return View(new AlunoVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Novo(AlunoVM alunoVM)
        {
            try
            {
                var usuarioModel = new Usuario
                {
                    Nome = alunoVM.Nome,
                    Email = alunoVM.Email,
                    NivelAcesso = NivelAcessoEnum.Aluno
                };

                alunoVM.Id = await _bussinesAluno.ObterUltimoIdAsync() + 1;
                alunoVM.ImagemUrl = AlunoHelper.ObterNomeArquivo(alunoVM);
                var alunoModel = Mapper.Map<AlunoVM, Aluno>(alunoVM);

                var senhaAluno = await _bussinesAluno.InserirAsync(alunoModel, usuarioModel);
                AlunoHelper.SalvarImagemAluno(alunoVM.ImagemUpload, AlunoHelper.ObterCaminhoImagemAluno(alunoVM, Server));

                EnviarEmailAluno(alunoVM, senhaAluno);
                return RedirectToRoute("Matricular", new { controller = "Matriculas", idAluno = alunoVM.Id });
            }
            catch (ArgumentException ex)
            {
                ViewBag.TiposResponsaveis = Enum.GetValues(typeof(TipoResponsavelEnum));
                TempData["Mensagem"] = ex.Message;
                return View(alunoVM);
            }
        }

        private void EnviarEmailAluno(AlunoVM alunoVM, string senhaAluno)
        {
            var nomeAluno = alunoVM.Nome.Split(' ')[0];
            _emailService.SendEmailMessage(new EmailMessage
            {
                Subject = "Bem-Vindo ao GEM",
                Title = $"Seja Bem-Vindo ao GEM {nomeAluno}",
                Body = $@"Olá {nomeAluno}, você acaba de entrar para o Grupo de Estudos Musical, 
                            aqui você irá aprender tudo relacionado a música desda teoria até a prática com o instrumento.
                            Desejamos boa sorte a você e aproveite muito está aportunidade.<br/> Para começar, a área do aluno já está
                            disponível para acesso e sua senha é: <b>{senhaAluno}</b>.
                            Aqui você poderá consultar suas informações pessoais, notas, faltas, calendário etc.",
                ToEmails = new List<string> { alunoVM.Email }
            });
        }

        [Route("Alunos/Editar/{id}")]
        public async Task<ActionResult> Editar(int id, string returnUrl = "")
        {
            var alunoVM = Mapper.Map<Aluno, AlunoVM>(await _bussinesAluno.ObterPorIdAsync(id));
            if (alunoVM == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.TiposResponsaveis = Enum.GetValues(typeof(TipoResponsavelEnum));
            return View(alunoVM);
        }

        public async Task<ActionResult> VisaoGeral(int Id)
        {
            var obterDadosDoAlunoVm = Mapper.Map<Aluno, AlunoVM>(await _bussinesAluno.ObterPorIdAsync(Id));
            if (obterDadosDoAlunoVm == null)
                return HttpNotFound();
            return View(obterDadosDoAlunoVm);
        }

        [Route("Alunos/Editar/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(AlunoVM alunoVM, string returnUrl = "")
        {
            try
            {
                if (alunoVM.ImagemUpload != null)
                {
                    alunoVM.ImagemUrl = AlunoHelper.ObterNomeArquivo(alunoVM);
                    AlunoHelper.SalvarImagemAluno(alunoVM.ImagemUpload, AlunoHelper.ObterCaminhoImagemAluno(alunoVM, Server));
                }
                var alunoModel = Mapper.Map<AlunoVM, Aluno>(alunoVM);
                await _bussinesAluno.AlterarAsync(alunoModel);

                if (string.IsNullOrEmpty(returnUrl))
                {
                    TempData["Mensagem"] = "Aluno alterado com sucesso.";
                    return RedirectToAction(nameof(Index));
                }

                return Redirect(returnUrl);
            }
            catch (ArgumentException ex)
            {
                ViewBag.ReturnUrl = returnUrl;
                TempData["Mensagem"] = ex.Message;
                return View(alunoVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Deletar(FormCollection formCollection)
        {
            int.TryParse(formCollection["id"], out int id);
            var alunoModel = await _bussinesAluno.ObterPorIdAsync(id);
            await _bussinesAluno.DeletarAsync(alunoModel);
            TempData["Mensagem"] = "Aluno apagado com sucesso.";
            return RedirectToAction(nameof(Index));
        }
    }
}