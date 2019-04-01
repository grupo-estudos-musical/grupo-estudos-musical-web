using GrupoEstudosMusical.MVC.Models;
using System;
using System.IO;
using System.Web;

namespace GrupoEstudosMusical.MVC.Helpers
{
    public static class AlunoHelper
    {
        public static string SalvarImagemAluno(AlunoVM alunoVM, HttpServerUtilityBase server)
        {
            if (alunoVM.ImagemUpload == null)
                throw new ArgumentException("Nenhuma foto do aluno foi definida.");

            var formatoImagem = alunoVM.ImagemUpload.ContentType;
            formatoImagem = formatoImagem.Substring(formatoImagem.IndexOf("/") + 1);
            var fileName = $"{alunoVM.Nome.Replace(" ", "_")}.{formatoImagem}";
            var path = Path.Combine(server.MapPath("~/Content/Alunos"), fileName);
            alunoVM.ImagemUpload.SaveAs(path);
            return fileName;
        }
    }
}