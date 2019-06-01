﻿using GrupoEstudosMusical.Models.Enums;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.MVC.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GrupoEstudosMusical.MVC.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IBussinesUsuario _bussinesUsuario;
        private readonly IBussinesAluno _bussinesAluno;

        public UsuariosController(IBussinesUsuario bussinesUsuario, IBussinesAluno bussinesAluno)
        {
            _bussinesUsuario = bussinesUsuario;
            _bussinesAluno = bussinesAluno;
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
                    Session["idUsuario"] = usuario.Id;
                    Session["nivelAcesso"] = usuario.NivelAcesso.ToString();
                    Session["nomeUsuario"] = usuario.Nome;
                    if (usuario.NivelAcesso == NivelAcessoEnum.Aluno)
                    {
                        var aluno = await _bussinesAluno.ObterPorUsuario(usuario.Id);
                        Session["imagemUrl"] = aluno.ImagemUrl;
                    }
                    return Redirect("/");
                }

                TempData["mensagem"] = "Usuário ou senha inválidos. Tente novamente.";
                TempData["tipo"] = "error";
            }

            return RedirectToAction("Login");
        }
    }
}