
using AutoMapper;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.MVC.App_Start;
using GrupoEstudosMusical.MVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using static GrupoEstudosMusical.Models.Entities.Instrumento;

namespace GrupoEstudosMusical.MVC.Controllers
{
   
    public class FabricantesController : Controller
    {
        // GET: Fabricantes
        public FabricantesController(IBussinesFabricante bussinesFabricante)
        {
            this.bussinesFabricante = bussinesFabricante;
        }
        readonly IBussinesFabricante bussinesFabricante;
        public async Task<ActionResult> Index()
        {
            var fabricantes = Mapper.Map<IList<Fabricante>, IList<FabricanteVM>>(await bussinesFabricante.ObterTodosAsync());
            return View(fabricantes);
        }


        public ActionResult Novo()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Novo(FabricanteVM fabricante)
        {
            try
            {
                var fabricanteModel = Mapper.Map<FabricanteVM, Fabricante>(fabricante);
                await bussinesFabricante.InserirAsync(fabricanteModel);
            
                TempData["Mensagem"] = "Fabricante cadastrado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View(fabricante);
            }
        }

        public ActionResult Editar(Guid ID)
        {
            var fabricanteModel = Mapper.Map<Fabricante,FabricanteVM>(bussinesFabricante.ObterPorIdGuid(ID));

            return View(fabricanteModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult>  Editar(FabricanteVM fabricante)
        {
            try
            {
                var fabricanteModel = Mapper.Map<FabricanteVM, Fabricante>(fabricante);
                await bussinesFabricante.AlterarAsync(fabricanteModel);
                TempData["Mensagem"] = "Fabricante alterado com sucesso!";
                return RedirectToAction("Index");
            }catch(Exception ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View(fabricante);
            }
        }
        public async Task<ActionResult> Deletar(FormCollection formCollection)
        {
            Guid.TryParse(formCollection["Id"].ToString(), out var id);
            var fabricanteModel = bussinesFabricante.ObterPorIdGuid(id);
            await bussinesFabricante.DeletarAsync(fabricanteModel);
            TempData["Mensagem"] = "Fabricante Apagado com Sucesso.";
            return RedirectToAction(nameof(Index));
        }
    }
}