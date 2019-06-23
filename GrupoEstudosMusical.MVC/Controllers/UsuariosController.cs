using AutoMapper;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Enums;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.MVC.App_Start;
using GrupoEstudosMusical.MVC.Helpers;
using GrupoEstudosMusical.MVC.Models;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GrupoEstudosMusical.MVC.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IBussinesUsuario _bussinesUsuario;
        private readonly IBussinesAluno _bussinesAluno;
        private readonly IBussinesProfessor _bussinesProfessor;

        public UsuariosController(IBussinesUsuario bussinesUsuario, IBussinesAluno bussinesAluno, IBussinesProfessor bussinesProfessor)
        {
            _bussinesUsuario = bussinesUsuario;
            _bussinesAluno = bussinesAluno;
            _bussinesProfessor = bussinesProfessor;
        }

        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Logar(AutenticarVM autenticarVM)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _bussinesUsuario.AutenticarAsync(autenticarVM.Email, autenticarVM.Senha);
                if (usuario != null)
                {
                    Session.Timeout = 30;
                    Session["idUsuario"] = usuario.Id;
                    Session["nivelAcesso"] = usuario.NivelAcesso.ToString();
                    Session["nomeUsuario"] = usuario.Nome;
                    if (usuario.NivelAcesso == NivelAcessoEnum.Aluno)
                    {
                        var aluno = await _bussinesAluno.ObterPorUsuario(usuario.Id);
                        Session["imagemUrl"] = aluno.ImagemUrl;
                        Session["AlunoID"] = aluno.Id;
                        return RedirectToAction("VisaoGeral", "Alunos", new { Id = aluno.Id });
                    }
                    if(usuario.NivelAcesso == NivelAcessoEnum.Professor)
                    {
                        var professor = Mapper.Map<IList<Professor>, IList<ProfessorVM>>(await _bussinesProfessor.ObterTodosPorUsuario(usuario.Id));
                        Session["ProfessorID"] = professor.FirstOrDefault().Id;
                    }
                    return Redirect("/");
                }

                TempData["mensagem"] = "Usuário ou senha inválidos. Tente novamente.";
                TempData["tipo"] = "error";
            }

            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return Redirect("/login");
        }

        [AuthorizeGem]
        public ActionResult AlterarSenha()
        {
            return View();
        }

        [HttpPost]
        [AuthorizeGem]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AlterarSenha(AlteraSenhaVM alteraSenhaVM)
        {
            if (ModelState.IsValid)
            {
                await _bussinesUsuario.AlterarSenha(int.Parse(Session["idUsuario"].ToString()), alteraSenhaVM.NovaSenha);
                TempData["mensagem"] = "Senha alterada com sucesso!";
                TempData["tipo"] = "success";
                return RedirectToAction(nameof(AlterarSenha));
            }

            return View(alteraSenhaVM);
        }

        public async Task<ActionResult> ResetarSenha(string token, int? usuarioId)
        {
            if (string.IsNullOrEmpty(token) && !usuarioId.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);

            var usuarioModel = await _bussinesUsuario.ObterPorIdAsync(usuarioId.Value);
            if (usuarioModel == null)
                return HttpNotFound("Aluno não encontrado.");

            var tokenHelper = new TokenHelper();
            if (!tokenHelper.ValidateToken(usuarioModel, token))
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);

            var alterarSenhaVM = new AlteraSenhaVM { Id = usuarioModel.Id };


            return View(alterarSenhaVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetarSenha(AlteraSenhaVM alteraSenhaVM)
        {
            if (ModelState.IsValid)
            {
                await _bussinesUsuario.AlterarSenha(alteraSenhaVM.Id, alteraSenhaVM.NovaSenha);
                var usuario = await _bussinesUsuario.ObterPorIdAsync(alteraSenhaVM.Id);
                Session.Timeout = 30;
                Session["idUsuario"] = usuario.Id;
                Session["nivelAcesso"] = usuario.NivelAcesso.ToString();
                Session["nomeUsuario"] = usuario.Nome;
                if (usuario.NivelAcesso == NivelAcessoEnum.Aluno)
                {
                    var aluno = await _bussinesAluno.ObterPorUsuario(usuario.Id);
                    Session["imagemUrl"] = aluno.ImagemUrl;
                    Session["AlunoID"] = aluno.Id;
                    return RedirectToAction("VisaoGeral", "Alunos", new { Id = aluno.Id });
                }
                return Redirect("/");
            }

            return View(alteraSenhaVM);
        }
    }
}