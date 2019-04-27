using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GrupoEstudosMusical.MVC.Models
{
    public class AulaVM
    {
        public int Id { get; set; }

        [DisplayName("Data da Aula")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataCadastro { get; private set; } = DateTime.Now;

        [DisplayName("Conteúdo")]
        public string Conteudo { get; set; }

        public int TurmaId { get; set; }

        public TurmaVM TurmaVM { get; set; }

        public List<AvaliacaoTurmaVM> AvaliacoesTurma { get; set; } = new List<AvaliacaoTurmaVM>();
    }
}