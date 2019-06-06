using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Util.Store;
using MimeTypes;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using FileGoogle = Google.Apis.Drive.v3.Data.File;

namespace GrupoEstudosMusical.MVC.GoogleDriveApi
{
    public class GoogleDriveApiService
    {
        public async Task<UserCredential> AutenticarAsync(string diretorioAtual)
        {
            UserCredential credenciais;

            string diretorioDrive = $@"{diretorioAtual}\GoogleDriveApi";
            using (var stream = new FileStream(Path.Combine(diretorioDrive, "client_id.json"), FileMode.Open, FileAccess.Read))
            {
                var diretorioCredenciais = Path.Combine(diretorioDrive, "credential");

                credenciais = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { DriveService.Scope.Drive },
                    "user",
                    System.Threading.CancellationToken.None,
                    new FileDataStore(diretorioCredenciais, true));
            }

            return credenciais;
        }

        public DriveService AbrirServico(UserCredential credenciais)
        {
            return new DriveService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credenciais
            });
        }

        public async Task<string> CriarDiretorioAsync(DriveService servico, string nomeDiretorio, List<string> parents)
        {
            var diretorio = new FileGoogle();

            if (parents != null && parents.Count > 0)
                diretorio.Parents = parents;

            diretorio.Name = nomeDiretorio;
            diretorio.MimeType = "application/vnd.google-apps.folder";
            var request = servico.Files.Create(diretorio);
            var file = await request.ExecuteAsync();
            return file.Id;
        }

        public async Task UploadAsync(DriveService servico, HttpPostedFileBase arquivoUpload, string nomeArquivo, List<string> parents)
        {
            var arquivoGoogle = new FileGoogle
            {
                Name = nomeArquivo,
                MimeType = MimeTypeMap.GetMimeType(arquivoUpload.ContentType)
            };

            if (parents != null && parents.Count > 0)
                arquivoGoogle.Parents = parents;

            using (var stream = arquivoUpload.InputStream)
            {
                var request = servico.Files.Create(arquivoGoogle, stream, arquivoGoogle.MimeType);
                await request.UploadAsync();
            }
        }

        public async Task<bool> VerificarDiretorioExisteAsync(DriveService servico, string nomeTurma)
        {
            IList<FileGoogle> arquivos = await PesquisarArquivoPorNomeAsync(servico, nomeTurma);
            return arquivos != null && arquivos.Any();
        }

        public async Task<IList<FileGoogle>> PesquisarArquivoPorNomeAsync(DriveService servico, string nomeArquivo, string operador = "=")
        {
            var request = servico.Files.List();
            request.Q = $"name {operador} '{nomeArquivo}' and trashed=false";
            request.Fields = "files(id,name,mimeType,parents,fileExtension)";
            var resultado = await request.ExecuteAsync();
            return resultado.Files;
        }

        public async Task<IList<FileGoogle>> PesquisarArquivoPorIdAsyn(DriveService servico, string id)
        {
            var request = servico.Files.List();
            request.Q = $"id='{id}'";
            request.Fields = "files(id, name, mimeType)";
            var resultado = await request.ExecuteAsync();
            return resultado.Files;
        }

        public MemoryStream Download(DriveService servico, string idArquivo)
        {
            var request = servico.Files.Get(idArquivo);

            if (request != null)
            {
                var stream = new MemoryStream();
                request.Download(stream);
                return stream;
            }

            return null;
        }
    }
}