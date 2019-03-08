using GrupoEstudosMusical.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GrupoEstudosMusical.MVC.Models
{
    public class TurmaVM
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        [DisplayName("Data Início")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataInicio { get; set; } = DateTime.Now;

        [DisplayName("Término da Aula")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime TerminoAula { get; set; } = DateTime.Now;

        [DisplayName("Período")]
        public int Periodo { get; set; }

        [DisplayName("Nível")]
        public string Nivel { get; set; }

        [DisplayName("Professor")]
        public int ProfessorID { get; set; }

        public string Status { get; set; }
        
        [DisplayName("Módulo")]
        public int ModuloId { get; set; }
        
        public Professor Professor { get; set; }

        public ModuloVM Modulo { get; set; }

        [DisplayName("Quantidade Máximo de Alunos")]
        public int QuantidadeAlunos { get; set; }

        public int Semestre { get; set; }

        public string PeriodoSemestre
        {
            get { return $"{Periodo}/{Semestre}"; }
        }

        public List<Matricula> Matriculas { get; set; }
    }
}