using AutoMapper;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GrupoEstudosMusical.MVC.Controllers
{
    public class ModuloController : Controller
    {
        private readonly IBussinesModulo _bussinesModulo;
        public ModuloController(IBussinesModulo bussinesModulo)
        {
            _bussinesModulo = bussinesModulo;
        }
        // GET: Modulo
        public async Task<ActionResult> Index()
        {
            var modulosViewModel = Mapper.Map<IList<Modulo>, IList<ModuloVM>>(await _bussinesModulo.ObterTodosAsync());
            return View(modulosViewModel);
        }
        public ActionResult Cadastrar()
        {
            return View(new ModuloVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Cadastrar(ModuloVM moduloVM)
        {
            var moduloModel = Mapper.Map<ModuloVM, Modulo>(moduloVM);
            await _bussinesModulo.InserirAsync(moduloModel);
            TempData["Mensagem"] = "Modulo cadastrado com sucesso.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult> Editar(int Id)
        {
            var modulo = Mapper.Map<Modulo, ModuloVM>(await _bussinesModulo.ObterPorIdAsync(Id));
            if (modulo == null)
            {
                return HttpNotFound();
            }
            return View(modulo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(ModuloVM moduloVM)
        {
            var moduloModel = Mapper.Map<ModuloVM, Modulo>(moduloVM);
            await _bussinesModulo.AlterarAsync(moduloModel);
            TempData["Mensagem"] = "Módulo alterado com sucesso";
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Deletar(FormCollection formCollection)
        {
            int.TryParse(formCollection["Id"].ToString(), out var id);
            var moduloModel = await _bussinesModulo.ObterPorIdAsync(id);
            await _bussinesModulo.DeletarAsync(moduloModel);
            TempData["Mensagem"] = "Módulo Apagado com Sucesso.";
            return RedirectToAction(nameof(Index));
        }
       
    }
}