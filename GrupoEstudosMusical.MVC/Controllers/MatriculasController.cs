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
    public class MatriculasController : Controller
    {
        private readonly IBussinesTurma _bussinesTurma;

        public MatriculasController(IBussinesTurma bussinesTurma)
        {
            _bussinesTurma = bussinesTurma;
        }

        public async Task<ActionResult> Novo()
        {
            var matriculaVM = new MatriculaVM();
            matriculaVM.Turmas = Mapper.Map<IList<Turma>, List<TurmaVM>>(await _bussinesTurma.ObterTodosAsync());
            return View(matriculaVM);
        }
    }
}