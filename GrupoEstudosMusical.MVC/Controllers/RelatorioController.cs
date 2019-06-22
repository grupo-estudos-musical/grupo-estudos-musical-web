using GrupoEstudosMusical.Bussines;
using GrupoEstudosMusical.Email;
using GrupoEstudosMusical.Email.Services.Generic;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Entities.Relatorios;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.MVC.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using static GrupoEstudosMusical.Email.EmailMessage;

namespace GrupoEstudosMusical.MVC.Controllers
{
    public class RelatorioController : Controller
    {
        // GET: Relatorio
        public RelatorioController(IBussinesOcorrencia bussinesOcorrencia, IEmailService bussinesEmail
            , IBussinesTurma bussinesTurma, IBussinesInstrumentoDoAluno bussinesInstrumentoDoAluno,
            IBussinesAluno bussinesAluno, IBussinesMatricula bussinesMatricula, IBussinesRelatorio bussinesRelatorio)
        {
            _bussinesOcorrencia = bussinesOcorrencia;
            emailService = bussinesEmail;
            this.bussinesTurma = bussinesTurma;
            _bussinesInstrumentoDoAluno = bussinesInstrumentoDoAluno;
            this.bussinesAluno = bussinesAluno;
            this.bussinesMatricula = bussinesMatricula;
            this.bussinesRelatorio = bussinesRelatorio;
        }


        private readonly IBussinesOcorrencia _bussinesOcorrencia;
        private readonly IEmailService emailService;
        private readonly IBussinesTurma bussinesTurma;
        private readonly IBussinesInstrumentoDoAluno _bussinesInstrumentoDoAluno;
        private readonly IBussinesAluno bussinesAluno;
        private readonly IBussinesMatricula bussinesMatricula;
        private readonly IBussinesRelatorio bussinesRelatorio;

        public async Task<ActionResult> VerificarExistenciaDeDadosParaRelatorio(int indiceEmitirRelatorio, int tipoDeRelatorio)
        {
            try
            {

                var dados = await bussinesRelatorio.VerificarExistenciaDeDadosParaRelatorio(indiceEmitirRelatorio, tipoDeRelatorio);
                return Json(new {  result = dados }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

        }

        
        public async Task<ActionResult> GerarRelatorioOcorrenciasPorAluno(int Id)
        {

            var listaOcorrenciasAluno = new List<OcorrenciasParaRelatorio>()
                {
                    await _bussinesOcorrencia.ObterOcorrenciasParaRelatorio(Id)
                };


            listaOcorrenciasAluno.ForEach(l => l.ImagemAluno = AlunoHelper.ObterByteImagemAluno(l.Aluno.ImagemUrl, Server));
            var relatorioGerado = Relatorios.GerarRelatorio<OcorrenciasParaRelatorio>(System.Web.HttpContext.Current.Server.MapPath("~/Relatorios/ocorrencias.frx"), listaOcorrenciasAluno, "Dados", TiposDeRelatorios.PDF, null);
            try
            {
                EnviarEmailAoALunoComRelatorio(listaOcorrenciasAluno.FirstOrDefault().Aluno, new List<byte[]>() { relatorioGerado });
            }
            catch
            { }

            return File(relatorioGerado, "application/pdf", $"RelatorioOcorrencias{DateTime.Now}.pdf");
        }

        private void EnviarEmailAoALunoComRelatorio(Aluno aluno, List<byte[]> Arquivos)
        {
            var emailMessage = new EmailMessage()
            {
                Body = $"Segue em anexo o relatório de ocorrências do {aluno.Nome}",
                Subject = "Relatório",
                Title = "Relatório teste",
                ToEmails = new List<string> { aluno.Email }

            };
            foreach (var bytes in Arquivos)
            {
                emailMessage.AdicionarAnexos(new Anexo() { Arquivo = bytes, NomeDoArquivo = "Relatório de ocorrência.pdf" });
            }
            emailService.SendEmailMessage(emailMessage);
        }


        public async Task<ActionResult> GerarRelatoriosDeOcorrenciasPorTurma(int turmaID)
        {
            if(TempData["Error"] != null && TempData["Error"].ToString() != "")
            {
                ViewBag.Error = TempData["Error"].ToString();
            }

            var turma = await bussinesTurma.ObterPorIdAsync(turmaID);
            if (turma == null)
                return HttpNotFound("Turma não encontrada!");
            return View(turma);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GerarRelatoriosDeOcorrenciasPorTurma(int turmaID, DateTime dataInicial, DateTime dataFinal)
        {
            try
            {
                var listaDeOcorrenciasPorTurma = this._bussinesOcorrencia.ObterOcorrenciasPorTurma(dataInicial, dataFinal, turmaID);
                listaDeOcorrenciasPorTurma.ForEach(l => l.ImagemAluno = AlunoHelper.ObterByteImagemAluno(l.Aluno.ImagemUrl, Server));
                var relatorioGerado = Relatorios.GerarRelatorio<OcorrenciasParaRelatorio>(System.Web.HttpContext.Current.Server.MapPath("~/Relatorios/ocorrencias.frx"), listaDeOcorrenciasPorTurma, "Dados", TiposDeRelatorios.PDF, null);
                return File(relatorioGerado, "application/pdf", $"RelatorioDeOcorrenciasPorTurma{DateTime.Now}.pdf");
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
                return RedirectToAction("GerarRelatoriosDeOcorrenciasPorTurma", new { turmaID });
            }

        }


        public async Task<ActionResult> GerarDetalhamentoDosEmprestimos(int alunoID)
        {
            var detalhesDoEmprestimo = new List<DetalhesDoEmprestimo>()
                {
                    await _bussinesInstrumentoDoAluno.ObterDadosDeEmprestimos(alunoID)
                 };
            var relatorioGerado = Relatorios.GerarRelatorio<DetalhesDoEmprestimo>(System.Web.HttpContext.Current.Server.MapPath("~/Relatorios/DetalhesDeEmpréstimos.frx"), detalhesDoEmprestimo, "Dados", TiposDeRelatorios.PDF, null);
            return File(relatorioGerado, "application/pdf", $"RelatorioDeDetalhamentoDeEmprestimo_{DateTime.Now}.pdf");
        }


        public async Task<ActionResult> GerarAtestadoDeMatricula(int alunoID)
        {

            var aluno = await bussinesAluno.ObterPorIdAsync(alunoID);
            if (aluno == null)
                return HttpNotFound("Aluno não encontrado");
            var matriculas = await bussinesMatricula.ObterMatriculasPorAluno(alunoID);
            if (matriculas.Count == 0)
                return HttpNotFound("Aluno não possui matrículas!");
            ViewBag.Matriculas = matriculas.Select(x => new { Id = x.Id, NomeTurma = x.Turma.Nome });
            return View(aluno);
        }

        [HttpPost]
        public async Task<ActionResult> GerarAtestadoDeMatricula(int MatriculaID, int alunoID)
        {
            try
            {
                var listaDeAtestadoDeMatricula = new List<AtestadoDeMatricula>()
                {
                    await bussinesMatricula.ObterAtestadoDeMatricula(MatriculaID)
                };
                var relatorioGerado = Relatorios.GerarRelatorio<AtestadoDeMatricula>(System.Web.HttpContext.Current.Server.MapPath("~/Relatorios/AtestadoDeMatricula.frx"), listaDeAtestadoDeMatricula, "Dados", TiposDeRelatorios.PDF, null);
                return File(relatorioGerado, "application/pdf", $"AtestadoDeMatricula_{MatriculaID}.pdf");
            }catch(Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("GerarAtestadoDeMatricula", new { alunoID });
            }

            
        }


        void SetarImagemDosAlunosNoBoletim(List<Boletim> boletimDosAlunos)
        {
            foreach (var item in boletimDosAlunos)
            {
                item.ImagemAluno = AlunoHelper.ObterByteImagemAluno(item.MatriculaAluno.Aluno.ImagemUrl, Server);
            }

        }


        public async Task<ActionResult> GerarBoletim(int matriculaID)
        {
            var listaBoletim = new List<Boletim>()
            {
                await bussinesMatricula.ObterBoletimAluno(matriculaID)
            };

            SetarImagemDosAlunosNoBoletim(listaBoletim);

            var relatorioGerador = Relatorios.GerarRelatorio<Boletim>(System.Web.HttpContext.Current.Server.MapPath("~/Relatorios/boletim.frx"), listaBoletim, "Dados", TiposDeRelatorios.PDF, null);
            return File(relatorioGerador, "application/pdf", "boletimAluno.pdf");
        }


        public async Task<ActionResult> GerarBoletimdaTurma(int ID)
        {
            var listaBoletimDaTurma = await bussinesMatricula.ObterBoletimDaTurma(ID);

            SetarImagemDosAlunosNoBoletim(listaBoletimDaTurma);

            var relatorioGerador = Relatorios.GerarRelatorio<Boletim>(System.Web.HttpContext.Current.Server.MapPath("~/Relatorios/boletim.frx"), listaBoletimDaTurma, "Dados", TiposDeRelatorios.PDF, null);
            return File(relatorioGerador, "application/pdf", $"boletimDaTurma{DateTime.Now}.pdf");
        }

    }
}