﻿using AutoMapper;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.MVC.App_Start;
using GrupoEstudosMusical.MVC.GoogleDriveApi;
using GrupoEstudosMusical.MVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GrupoEstudosMusical.MVC.Controllers
{
    [AuthorizeGem]
    public class AulasController : Controller
    {
        private readonly IBussinesAula _bussinesAula;
        private readonly IBussinesTurma _bussinesTurma;
        private readonly IBussinesAvaliacaoTurma _bussinesAvaliacaoTurma;

        public AulasController(IBussinesAula bussinesAula, 
            IBussinesTurma bussinesTurma,
            IBussinesAvaliacaoTurma bussinesAvaliacaoTurma)
        {
            _bussinesAula = bussinesAula;
            _bussinesTurma = bussinesTurma;
            _bussinesAvaliacaoTurma = bussinesAvaliacaoTurma;
        }

        public async Task<ActionResult> Index(int idTurma)
        {
            var turmaVM = Mapper.Map<Turma, TurmaVM>(await _bussinesTurma.ObterPorIdAsync(idTurma));
            if (turmaVM == null)
                return HttpNotFound();

            var aulasModel = await _bussinesAula.ObterPorTurma(idTurma);
            var aulasVM = Mapper.Map<List<Aula>, List<AulaVM>>(aulasModel);

            var listaAulaVM = new ListaAulasVM
            {
                Turma = turmaVM,
                Aulas = aulasVM
            };
            return View(listaAulaVM);
        }

        public async Task<ActionResult> Novo(int idTurma)
        {
            var turmaVM = Mapper.Map<Turma, TurmaVM>(await _bussinesTurma.ObterPorIdAsync(idTurma));
            var avaliacoesTurmaVM =
                Mapper.Map<List<AvaliacaoTurma>, List<AvaliacaoTurmaVM>>(_bussinesAvaliacaoTurma.ObterPorTurma(idTurma));
            var aulaVM = new AulaVM
            {
                TurmaId = turmaVM.Id,
                Turma = turmaVM,
                AvaliacoesTurma = avaliacoesTurmaVM
            };
            return View(aulaVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Novo(AulaVM aulaVM)
        {
            try
            {
                var aulaModel = Mapper.Map<AulaVM, Aula>(aulaVM);
                var avaliacoesSelecionadas = aulaVM.AvaliacoesTurma.Where(a => a.Selecionado).ToList();
                var avaliacoesTurma = Mapper.Map<List<AvaliacaoTurmaVM>, List<AvaliacaoTurma>>(avaliacoesSelecionadas);
                await _bussinesAula.InserirAsync(aulaModel, avaliacoesTurma);

                aulaVM.Id = aulaModel.Id;
                var turmaVM = Mapper.Map<Turma, TurmaVM>(await _bussinesTurma.ObterPorIdAsync(aulaVM.TurmaId));
                using (var driveHelper = new GoogleDriveApiHelper(Server.MapPath("").Replace(@"\Aulas", "")))
                {
                    await driveHelper.UploadArquivoAulaAsync(aulaVM, turmaVM);
                }

                TempData["mensagem"] = "Aula cadastrada com sucesso!";
                TempData["tipo"] = "success";
                return RedirectToAction("Index", "Aulas", new { idTurma = aulaVM.TurmaId });
            }
            catch (ArgumentException ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View(aulaVM);
            }
        }

        public async Task<ActionResult> Detalhes(int id)
        {
            var aulaModel = await _bussinesAula.ObterPorIdAsync(id);
            var aulaVM = Mapper.Map<Aula, AulaVM>(aulaModel);

            using (var driveHelper = new GoogleDriveApiHelper(Server.MapPath("").Replace(@"\Aulas", "").Replace(@"\Detalhes", "")))
            {
                var arquivo = await driveHelper.PesquisarArquivoPorNome(aulaVM);
                aulaVM.NomeArquivo = arquivo.Name;
                aulaVM.ExtensaoArquivo = arquivo.FileExtension;
            }

            return View(aulaVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Deletar(int id)
        {
            var aulaModel = await _bussinesAula.ObterPorIdAsync(id);
            if (aulaModel == null)
                return HttpNotFound();
            await _bussinesAula.DeletarAsync(aulaModel);
            TempData["mensagem"] = "Aula apagada com sucesso!";
            TempData["tipo"] = "success";
            return RedirectToAction(nameof(Index), new { idTurma = aulaModel.TurmaId });
        }

        public async Task<ActionResult> Download(int idAula)
        {
            var aulaVM = Mapper.Map<Aula, AulaVM>(await _bussinesAula.ObterPorIdAsync(idAula));

            using (var driveHelper = new GoogleDriveApiHelper(Server.MapPath("").Replace(@"\Aulas", "")))
            {
                var arquivoDownload = await driveHelper.DownloadArquivoAulaAsync(aulaVM);
                if (arquivoDownload != null)
                {
                    var fileResult = new FileStreamResult(arquivoDownload.Stream, arquivoDownload.MimeType);
                    fileResult.FileDownloadName = $"{arquivoDownload.Nome}.{arquivoDownload.Extensao}";
                    return fileResult;
                }
            }

            return RedirectToAction(nameof(Detalhes), new { id = idAula});
        }
    }
}