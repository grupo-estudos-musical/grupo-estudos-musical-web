using GrupoEstudosMusical.Bussines;
using GrupoEstudosMusical.Email;
using GrupoEstudosMusical.Email.Services.Generic;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Entities.Relatorios;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.MVC.App_Start;
using GrupoEstudosMusical.MVC.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using static GrupoEstudosMusical.Email.EmailMessage;

namespace GrupoEstudosMusical.MVC.Controllers
{
    [AuthorizeGem]
    public class RelatoriosController : Controller
    {
        // GET: Relatorios
        public RelatoriosController(IBussinesOcorrencia bussinesOcorrencia, IBussinesMatricula bussinesMatricula
            , IBussinesTurma bussinesTurma, IEmailService email, IBussinesAluno bussinesAluno, IBussinesInstrumentoDoAluno bussinesInstrumentoDoAluno)
        {
            _bussinesEmail = email;
            this.bussinesOcorrencia = bussinesOcorrencia;
            this.bussinesMatricula = bussinesMatricula;
            this.bussinesTurma = bussinesTurma;
            this.bussinesAluno = bussinesAluno;
            this._bussinesInstrumentoDoAluno = bussinesInstrumentoDoAluno;
        }
        readonly IBussinesOcorrencia bussinesOcorrencia;
        readonly IBussinesMatricula bussinesMatricula;
        readonly IBussinesTurma bussinesTurma;
        readonly IEmailService _bussinesEmail;
        readonly IBussinesAluno bussinesAluno;
        readonly IBussinesInstrumentoDoAluno _bussinesInstrumentoDoAluno;


        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GerarRelatoriosDeOcorrenciasPorTurma(int turmaID)
        {
            var turma = await bussinesTurma.ObterPorIdAsync(turmaID);
            if (turma == null)
                return HttpNotFound("Turma não encontrada!");
            return View(turma);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GerarRelatoriosDeOcorrenciasPorTurma(DateTime dataInicial, DateTime dataFinal, int turmaID)
        {
            try
            {
                var listaDeOcorrenciasPorTurma = bussinesOcorrencia.ObterOcorrenciasPorTurma(dataInicial, dataFinal, turmaID);
                listaDeOcorrenciasPorTurma.ForEach(l => l.ImagemAluno = AlunoHelper.ObterByteImagemAluno(l.Aluno.ImagemUrl, Server));
                var relatorioGerado = Relatorios.GerarRelatorio<OcorrenciasParaRelatorio>(System.Web.HttpContext.Current.Server.MapPath("~/Relatorios/ocorrencias.frx"), listaDeOcorrenciasPorTurma, "Dados", TiposDeRelatorios.PDF, null);
                return File(relatorioGerado, "application/pdf", $"RelatorioDeOcorrenciasPorTurma{DateTime.Now}.pdf");
            }
            catch (Exception e)
            {
                TempData["_MensagemErro"] = e.Message;
                return View();
            }

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
            _bussinesEmail.SendEmailMessage(emailMessage);
        }



        public async Task<ActionResult> GerarRelatorioOcorrenciasPorAluno(int AlunoId)
        {
            try
            {
                var listaOcorrenciasAluno = new List<OcorrenciasParaRelatorio>()
                {
                    await bussinesOcorrencia.ObterOcorrenciasParaRelatorio(AlunoId)
                };
                listaOcorrenciasAluno.ForEach(l => l.ImagemAluno = AlunoHelper.ObterByteImagemAluno(l.Aluno.ImagemUrl, Server));
                var relatorioGerado = Relatorios.GerarRelatorio<OcorrenciasParaRelatorio>(System.Web.HttpContext.Current.Server.MapPath("~/Relatorios/ocorrencias.frx"), listaOcorrenciasAluno, "Dados", TiposDeRelatorios.PDF, null);
                EnviarEmailAoALunoComRelatorio(listaOcorrenciasAluno.FirstOrDefault().Aluno, new List<byte[]>() { relatorioGerado });
                return File(relatorioGerado, "application/pdf", $"RelatorioOcorrencias{DateTime.Now}.pdf");

            }
            catch(Exception ex) {
                TempData["Error"] = ex.Message + ex.StackTrace;
                return RedirectToAction("Index", "Home");
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
        public async Task<ActionResult> GerarAtestadoDeMatricula(int MatriculaID, string opcao = "")
        {

            var listaDeAtestadoDeMatricula = new List<AtestadoDeMatricula>()
            {
                await bussinesMatricula.ObterAtestadoDeMatricula(MatriculaID)
            };
            var relatorioGerado = Relatorios.GerarRelatorio<AtestadoDeMatricula>(System.Web.HttpContext.Current.Server.MapPath("~/Relatorios/AtestadoDeMatricula.frx"), listaDeAtestadoDeMatricula, "Dados", TiposDeRelatorios.PDF, null);
            return File(relatorioGerado, "application/pdf", $"AtestadoDeMatricula_{MatriculaID}.pdf");
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

        public void SetarImagemDosAlunosNoBoletim(List<Boletim> boletimDosAlunos)
        {
            foreach (var item in boletimDosAlunos)
            {
                item.ImagemAluno = AlunoHelper.ObterByteImagemAluno(item.MatriculaAluno.Aluno.ImagemUrl, Server);
            }

        }



    }
}