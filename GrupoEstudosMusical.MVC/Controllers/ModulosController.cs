using AutoMapper;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.MVC.App_Start;
using GrupoEstudosMusical.MVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GrupoEstudosMusical.MVC.Controllers
{
    [AuthorizeGem]
    public class ModulosController : Controller
    {
        private readonly IBussinesModulo _bussinesModulo;
        public ModulosController(IBussinesModulo bussinesModulo)
        {
            _bussinesModulo = bussinesModulo;
        }
        // GET: Modulo
        public async Task<ActionResult> Index()
        {
            var modulosViewModel = Mapper.Map<IList<Modulo>, IList<ModuloVM>>(await _bussinesModulo.ObterTodosAsync());
            return View(modulosViewModel);
        }
        public ActionResult Novo()
        {
            return View(new ModuloVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Novo(ModuloVM moduloVM)
        {
            try
            {
                var moduloModel = Mapper.Map<ModuloVM, Modulo>(moduloVM);
                await _bussinesModulo.InserirAsync(moduloModel);
                TempData["Mensagem"] = "Modulo cadastrado com sucesso.";
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View(moduloVM);
            }
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
            try
            {
                var moduloModel = Mapper.Map<ModuloVM, Modulo>(moduloVM);
                await _bussinesModulo.AlterarAsync(moduloModel);
                TempData["Mensagem"] = "Módulo alterado com sucesso";
                return RedirectToAction(nameof(Index));
            }catch(ArgumentException ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View(moduloVM);
            }
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