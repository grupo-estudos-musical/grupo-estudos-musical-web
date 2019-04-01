using GrupoEstudosMusical.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GrupoEstudosMusical.MVC.Models
{
    public class AvaliacaoTurmaVM
    {
        public AvaliacaoTurmaVM()
        {
            this.DataCadastro = DateTime.Now;
        }

        public AvaliacaoTurmaVM(List<AvaliacaoVM> avaliacaos)
        {
            AvaliacoesDisponiveis = avaliacaos;

        }
        public int AvaliacaoID { get; set; }
        public virtual TurmaVM Turma { get; set; }
        public int TurmaID { get; set; }
        public virtual AvaliacaoVM Avaliacao { get; set; }
        [DisplayName("Data Prevista Para Realização da Prova")]
        public DateTime DataPrevista { get; set; }

        [DisplayName("Data da Realização da Prova")]
        public DateTime DataRealizacao { get; set; }
        public DateTime DataCadastro { get; private set; }
        public List<AvaliacaoVM> AvaliacoesDisponiveis { get; set; }
        public string NomeTurma { get; set; }

    }
}