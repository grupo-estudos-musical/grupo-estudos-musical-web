using GrupoEstudosMusical.Models.Entities;
using System;

namespace GrupoEstudosMusical.MVC.Models
{
    public class AvaliacaoTurmaVM
    {
        public AvaliacaoTurmaVM()
        {
            this.DataCadastro = DateTime.Now;
        }
        public int AvaliacaoID { get;set; }
        public virtual Turma Turma { get; set; }
        public int TurmaID { get; set; }
        public virtual Avaliacao Avaliacao { get; set; }
        public DateTime DataPrevista { get; set; }
        public DateTime DataRealizacao { get; set; }
        public DateTime DataCadastro { get; private set; }
    }
}