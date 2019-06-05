using AutoMapper;
using GrupoEstudosMusical.Bussines.Exceptions;
using GrupoEstudosMusical.Bussines.StaticList;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.MVC.Models;
using GrupoEstudosMusical.MVC.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GrupoEstudosMusical.MVC.Controllers
{
    public class MatriculasController : Controller
    {
        private readonly IBussinesTurma _bussinesTurma;
        private readonly IBussinesAluno _bussinesAluno;
        private readonly IBussinesMatricula _bussinesMatricula;
        private readonly IBussinesModulo _bussinesModulo;
        private readonly IBussinesAvaliacaoTurma _bussinesAvaliacaoTurma;
        private readonly IBussinesPalhetaDeNotas _bussinesPalhetaDeNotas;

        public MatriculasController(IBussinesTurma bussinesTurma, IBussinesAluno bussinesAluno,
            IBussinesMatricula bussinesMatricula, IBussinesModulo bussinesModulo, IBussinesAvaliacaoTurma bussinesAvaliacaoTurma, IBussinesPalhetaDeNotas bussinesPalhetaDeNotas)
        {
            _bussinesTurma = bussinesTurma;
            _bussinesAluno = bussinesAluno;
            _bussinesMatricula = bussinesMatricula;
            _bussinesModulo = bussinesModulo;
            _bussinesAvaliacaoTurma = bussinesAvaliacaoTurma;
            _bussinesPalhetaDeNotas = bussinesPalhetaDeNotas;
        }

        public async Task<ActionResult> Index(int idAluno)
        {
            var aluno = await _bussinesAluno.ObterPorIdAsync(idAluno);
            if (aluno == null)
                return HttpNotFound("Aluno não encontrado");

            IList<Matricula> alunosModel = await _bussinesMatricula.ObterMatriculasPorAluno(idAluno);
            if (alunosModel.Count == 0)
            {
                TempData["Mensagem"] = "Aluno não está matrículado em nenhum curso.";
                return RedirectToAction("VisaoGeral", "Alunos", new { id = idAluno });
            }
            var matriculasVM = Mapper.Map<IList<Matricula>, List<MatriculaListaVM>>(alunosModel);            

            return View(matriculasVM);
        }
        
        [HttpGet]
        [Route("Aluno/Matricular/{idAluno}", Name = "Matricular")]
        public async Task<ActionResult> Novo(int idAluno)
        {
            var matriculaVM = new MatriculaVM();
            var aluno = await _bussinesAluno.ObterPorIdAsync(idAluno);
            if (aluno == null)
                return HttpNotFound("Aluno não encontrado");

            matriculaVM.AlunoId = aluno.Id;
            matriculaVM.Aluno = Mapper.Map<Aluno, AlunoVM>(aluno);
            matriculaVM.Turmas = Mapper.Map<IList<Turma>, List<TurmaVM>>(await _bussinesTurma.ObterTodosAsync());
            ViewBag.PossuiRestricao = await _bussinesMatricula.VerificarRestricaoMatricula(idAluno);
            matriculaVM.Modulos = Mapper.Map<IList<Modulo>, List<ModuloVM>>(await TrazerModulosDisponiveisProAlunoSeMatricular(idAluno, ViewBag.PossuiRestricao));
            var matriculas = await _bussinesMatricula.ObterMatriculasPorAluno(idAluno);
            
            matriculaVM.TurmasMatriculadas = Mapper.Map<IList<Turma>, List<TurmaVM>>(matriculas.Select(m => m.Turma).ToList());
            return View(matriculaVM);
        }

        public async Task<IList<Modulo>> TrazerModulosDisponiveisProAlunoSeMatricular(int alunoID, bool semrestricao = false)
        {
            if (!semrestricao)
            {
                return await _bussinesModulo.ObterTodosAsync();
            }
            var modulos = await _bussinesMatricula.ObterModulosEmQueAlunoEstaRetido(alunoID);
            return modulos.Count > 0 ? modulos : null;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Aluno/Matricular/{idAluno}")]
        public async Task<ActionResult> Novo(int idAluno, MatriculaVM matriculaVM)
        {
            try
            {
                var matriculaModel = Mapper.Map<MatriculaVM, Matricula>(matriculaVM);
                var idMatricula = await _bussinesMatricula.IncluirMatricula(matriculaModel);
                await AdicionarAvaliacoesNaMatrículaDoAluno(matriculaModel.TurmaId, idMatricula);

                TempData["Mensagem"] = "Aluno matrículado com sucesso.";
                return RedirectToAction("Index","Alunos");
            }
            catch (TurmaMatriculaExeception ex)
            {
                ViewData["Mensagem"] = ex.Message;
                return View(matriculaVM);
            }
        }

        async Task AdicionarAvaliacoesNaMatrículaDoAluno(int turmaId,int matriculaId )
        {
            var listaDeAvaliacoes = _bussinesAvaliacaoTurma.ObterPorTurma(turmaId);
            if(listaDeAvaliacoes.Count > 0)
                await _bussinesPalhetaDeNotas.AdicionarTodasAvaliacoesDaTurmaAoALuno(listaDeAvaliacoes, matriculaId);
        }

        public async Task<ActionResult> Detalhes(int id)
        {
            var matriculaModel = await _bussinesMatricula.ObterPorIdAsync(id);
            if (matriculaModel == null)
                return HttpNotFound("Matrícula não encontrada");

            var matriculaVM = Mapper.Map<Matricula, MatriculaVM>(matriculaModel);
            return View(matriculaVM);
        }

        [HttpPost]
        public async Task<ActionResult> AlterarDocumentosApresentados(DocumentosApresentadosVM documentosApresentados)
        {
            try
            {
                var matriculaModel = await _bussinesMatricula.ObterPorIdAsync(documentosApresentados.IdMatricula);
                if (matriculaModel == null)
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                matriculaModel.Cpf = documentosApresentados.Cpf;
                matriculaModel.Rg = documentosApresentados.Rg;
                matriculaModel.ComprovanteResidencia = documentosApresentados.ComprovanteResidencia;
                await _bussinesMatricula.AlterarAsync(matriculaModel);
                return new JsonSuccesResult(new { pendente = matriculaModel.Pendente },
                    "Documentos alterados com sucesso.");
            }
            catch (Exception)
            {
                return new JsonErrorResult("Não foi possível alterar documentos.");
            }
        }
    }
}