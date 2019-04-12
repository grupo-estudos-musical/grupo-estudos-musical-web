using GrupoEstudosMusical.MVC.Models;
using System;
using System.IO;

namespace GrupoEstudosMusical.MVC.Helpers
{
    public static class AlunoHelper
    {
        private static readonly string PathImagens = $@"{AppDomain.CurrentDomain.BaseDirectory}\Content\ImagensAlunos";

        public static void SalvarImagemAluno(AlunoVM alunoVM) => alunoVM.ImagemUpload.SaveAs(alunoVM.ImagemUrl);

        public static string ObterCaminhoImagemAluno(AlunoVM alunoVM)
        {
            if (alunoVM.ImagemUpload == null)
                throw new ArgumentException("Nenhuma foto do aluno foi definida.");

            var formatoImagem = alunoVM.ImagemUpload.ContentType;
            formatoImagem = formatoImagem.Substring(formatoImagem.IndexOf("/") + 1);

            var fileName = $"{alunoVM.Id} - {alunoVM.Nome}.{formatoImagem}";

            if (!Directory.Exists(PathImagens))
                Directory.CreateDirectory(PathImagens);

            var path = Path.Combine(PathImagens, fileName);

            return path;
        }
    }
}