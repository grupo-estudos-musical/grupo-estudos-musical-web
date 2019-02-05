using AutoMapper;
using GrupoEstudosMusical.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GrupoEstudosMusical.MVC.Models
{
    public class TurmaVM
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [DisplayName("Data Início")]
        public DateTime DataInicio { get; set; } = DateTime.Now;
        [DisplayName("Término da Aula")]
        public DateTime TerminoAula { get; set; }
        [DisplayName("Período")]
        public int Periodo { get; set; }

        [DisplayName("Nível")]
        public string Nivel { get; set; }
    
        //[DisplayName("Professor")]
        public int ProfessorID { get; set; }
        public string Status { get; set; }

        [DisplayName("Módulos")]
        public int ModuloId { get; set; }

        // public Professor Professor { get; set; }
       // public ProfessorVM ProfessorVM { get; set; } = Mapper.Map<ProfessorVM,Professor>(_)
        public string NomeModulo { get; set; }

        public string NomeProfessor { get; set; }

        public int QuantidadeAlunos { get; set; }

        public int Semestre { get; set; }
    }
}