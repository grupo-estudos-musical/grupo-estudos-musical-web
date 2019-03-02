using GrupoEstudosMusical.Models.Entities;
using System;
using System.ComponentModel;


namespace GrupoEstudosMusical.MVC.Models
{
    public class OcorrenciaVM
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        [DisplayName("Data Ocorrido")]
        public DateTime DataOcorrido { get; set; }

        [DisplayName("Tipo de Ocorrência")]
        public string Tipo { get; set; }

        public string Resumo { get; set; }


        public Turma Turma { get; set; }

        [DisplayName("Turma")]
        public int TurmaID { get; set; }

        public Aluno Aluno { get; set; }

        [DisplayName("Aluno")]
        public int AlunoID { get; set; }
    }
}