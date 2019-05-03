using AutoMapper;
using GrupoEstudosMusical.Models;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.MVC.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GrupoEstudosMusical.MVC.Controllers
{
    public class InventarioController : Controller
    {
        // GET: Inventario
        public ActionResult Index()
        {
            var inventarios = Mapper.Map<IList<Inventario>, IList<InventarioVM>>(_bussinesInventario.ObterTodosItensDoInventario());
            inventarios.ForEach(i => i.NomeInstrumento = i.Instrumento != null ? i.Instrumento.Nome : "");
            return View(inventarios);
        }

        public InventarioController(IBussinesInventario bussinesInventario)
        {
            _bussinesInventario = bussinesInventario;
        }
        readonly IBussinesInventario _bussinesInventario;

        [HttpPost]
        public async Task<JsonResult> AtualizarInventario(Guid idInventario, int quantidadeMinima, int quantidadeDisponivel)
        {
            try
            {
                await _bussinesInventario.ModificarInventario(idInventario, quantidadeMinima, quantidadeDisponivel);
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
                
            }catch(Exception ex)
            {
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}