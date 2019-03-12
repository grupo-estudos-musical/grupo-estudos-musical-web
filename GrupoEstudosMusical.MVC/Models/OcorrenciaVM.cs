using GrupoEstudosMusical.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GrupoEstudosMusical.MVC.Models
{
    public class OcorrenciaVM
    {
        public OcorrenciaVM()
        {
            Turmas = new List<Turma>();
        }
        public int Id { get; set; }

        public string Titulo { get; set; }

        [DisplayName("Data Ocorrido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataOcorrido { get; set; } = DateTime.Now;

        [DisplayName("Tipo de Ocorrência")]
        public string Tipo { get; set; }

        public string Resumo { get; set; }


        public Turma Turma { get; set; }

        [DisplayName("Turma")]
        public int TurmaID { get; set; }

        public Aluno Aluno { get; set; }

        [DisplayName("Aluno")]
        public int AlunoID { get; set; }

        public List<Turma> Turmas { get; set; }
    }
}