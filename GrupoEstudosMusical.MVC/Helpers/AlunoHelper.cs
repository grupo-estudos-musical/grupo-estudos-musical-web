using GrupoEstudosMusical.MVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace GrupoEstudosMusical.MVC.Helpers
{
    public static class AlunoHelper
    {
        private static readonly string PathImagens = $@"~/Content/ImagensAlunos";
        private static readonly List<string> formatos = new List<string>() { "jpg", "jpeg", "png" };

        public static void SalvarImagemAluno(HttpPostedFileBase imagem, string imagemUrl) => imagem.SaveAs(imagemUrl);

        public static string ObterCaminhoImagemAluno(AlunoVM alunoVM, HttpServerUtilityBase server)
        {
            var caminhoFisico = server.MapPath(PathImagens);
            if (!Directory.Exists(caminhoFisico))
                Directory.CreateDirectory(caminhoFisico);

            var path = Path.Combine(caminhoFisico, alunoVM.ImagemUrl);

            return path;
        }

        public static byte[] ObterByteImagemAluno(string caminhoImagem, HttpServerUtilityBase server)
        {
            var imagemCaminho = ObterCaminhoImagemAluno(new AlunoVM() { ImagemUrl = caminhoImagem }, server);
            if (!File.Exists(imagemCaminho))
                return (byte[])null;
            var bytesImagem = File.ReadAllBytes(imagemCaminho);
            return bytesImagem;
        }
   
        public static string ObterNomeArquivo(AlunoVM alunoVM)
        {
            if (alunoVM.ImagemUpload == null)
                throw new ArgumentException("Nenhuma foto do aluno foi definida.");

            var formatoImagem = alunoVM.ImagemUpload.ContentType;
            formatoImagem = formatoImagem.Substring(formatoImagem.IndexOf("/") + 1);

            if (!formatos.Contains(formatoImagem))
                throw new ArgumentException("Formato de imagem inválido.");

                var fileName = $"{alunoVM.Id} - {alunoVM.Nome}.{formatoImagem}";
            return fileName;
        }
    }
}