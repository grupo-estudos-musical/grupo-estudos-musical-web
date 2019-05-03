using GrupoEstudosMusical.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GrupoEstudosMusical.MVC.Models
{
    public class AvaliacaoTurmaVM
    {
        public AvaliacaoTurmaVM()
        {
            
        }

        public AvaliacaoTurmaVM(List<AvaliacaoVM> avaliacaos)
        {
            AvaliacoesDisponiveis = avaliacaos;
        }

        public Guid IdAvaliacaoTurma { get; set; }
        public int AvaliacaoID { get; set; }
        public virtual TurmaVM Turma { get; set; }
        public int TurmaID { get; set; }
        public virtual AvaliacaoVM Avaliacao { get; set; }

        [DisplayName("Data Prevista Para Realização da Prova")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataPrevista { get; set; } = DateTime.Now;


        [DisplayName("Data da Realização da Prova")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataRealizacao { get; set; } = DateTime.Now;

        public DateTime DataCadastro { get; private set; } = DateTime.Now;
        public List<AvaliacaoVM> AvaliacoesDisponiveis { get; set; }
        public string NomeTurma { get; set; }
        public int? AulaId { get; set; }
        public bool Selecionado { get; set; }
    }
}