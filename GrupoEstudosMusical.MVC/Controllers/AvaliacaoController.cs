
using AutoMapper;
using GrupoEstudosMusical.Bussines.Exceptions;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.MVC.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GrupoEstudosMusical.MVC.Controllers
{
    public class AvaliacaoController : Controller
    {
        // GET: Avaliacao
        public AvaliacaoController(IBussinesAvaliacao bussinesAvaliacao)
        {
            this._bussinesAvaliacao = bussinesAvaliacao;
        }
        readonly IBussinesAvaliacao _bussinesAvaliacao;
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Novo()
        {
            return View(new AvaliacaoVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Novo(AvaliacaoVM avaliacaoVM)
        {
            try
            {
                var modelAvaliacao = Mapper.Map<AvaliacaoVM, Avaliacao>(avaliacaoVM);
                await _bussinesAvaliacao.InserirAsync(modelAvaliacao);
                return RedirectToAction("Index");

                
            }catch(CrudAvaliacaoException ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View(avaliacaoVM);
            }
        }

    }
}