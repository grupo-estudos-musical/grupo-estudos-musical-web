using Google.Apis.Drive.v3;
using GrupoEstudosMusical.MVC.Models;
using MimeTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GrupoEstudosMusical.MVC.GoogleDriveApi
{
    public class GoogleDriveApiHelper : IDisposable
    {
        private DriveService _driveService;
        private GoogleDriveApiService _googleDriveApi;

        public GoogleDriveApiHelper(string diretorioAtualAplicacao)
        {
            AbrirServico(diretorioAtualAplicacao);
        }

        private void AbrirServico(string diretorioAtualAplicacao)
        {
            _googleDriveApi = new GoogleDriveApiService();
            var credenciais = _googleDriveApi.AutenticarAsync(diretorioAtualAplicacao).Result;
            _driveService = _googleDriveApi.AbrirServico(credenciais);
        }

        private async Task<string> CriarDiretorioAsync(string nomeDiretorio, List<string> parents)
        {
            var existenciaDiretorio = await _googleDriveApi.VerificarDiretorioExisteAsync(_driveService, nomeDiretorio);
            if (!existenciaDiretorio)
                return await _googleDriveApi.CriarDiretorioAsync(_driveService, nomeDiretorio, parents);

            var arquivos = await _googleDriveApi.PesquisarArquivoPorNomeAsync(_driveService, nomeDiretorio);
            return arquivos[0].Id;
        }

        private async Task UploadArquivoAsync(int id, HttpPostedFileBase arquivoUpload, List<string> parents)
        {
            await _googleDriveApi.UploadAsync(_driveService, arquivoUpload, $"{id}-{DateTime.Now.ToString("dd-MM-yyyy")}-{arquivoUpload.FileName}", parents);
        }

        public async Task<ArquivoDownload> DownloadArquivoAulaAsync(AulaVM aulaVM, TurmaVM turmaVM)
        {
            var nomeArquivo = $"{aulaVM.Id}-{aulaVM.DataCadastro.ToString("dd-MM-yyyy")}";
            var arquivoAula = (await _googleDriveApi
                .PesquisarArquivoPorNomeAsync(_driveService, nomeArquivo, "contains"))
                .FirstOrDefault();

            if (arquivoAula != null)
            {
                var stream = _googleDriveApi.Download(_driveService, arquivoAula.Id);
                var arquivoDownload = new ArquivoDownload
                {
                    Nome = arquivoAula.Name,
                    Stream = new MemoryStream(stream.ToArray()),
                    MimeType = MimeTypeMap.GetMimeType(arquivoAula.FileExtension),
                    Extensao = arquivoAula.FileExtension
                };
                return arquivoDownload;
            }

            return null;
        }

        public async Task UploadArquivoAulaAsync(AulaVM aulaVM, TurmaVM turmaVM)
        {
            var idDirAulas = await CriarDiretorioAsync("Aulas", null);

            var idDirPeriodo = await CriarDiretorioAsync(turmaVM.PeriodoSemestre, new List<string> { idDirAulas });

            var idDirTurma = await CriarDiretorioAsync(turmaVM.Nome, new List<string> { idDirPeriodo});

            var parentsArquivo = new List<string> { idDirTurma };
            await UploadArquivoAsync(aulaVM.Id, aulaVM.Arquivo, parentsArquivo);
        }

        public void Dispose()
        {
            _driveService.Dispose();
        }
    }
}