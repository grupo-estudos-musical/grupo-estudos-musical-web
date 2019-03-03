using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GrupoEstudosMusical.MVC.Models
{
    public class MatriculaVM
    {
        public int Id { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public AlunoVM Aluno { get; set; }
        public int AlunoId { get; set; }
        public int TurmaId { get; set; }

        [DisplayName("CPF")]
        public bool Cpf { get; set; }

        [DisplayName("RG")]
        public bool Rg { get; set; }

        [DisplayName("Comprovante de Residência")]
        public bool ComprovanteResidencia { get; set; }

        public bool Pendente { get; set; }

        public TurmaVM Turma { get; set; }

        public List<TurmaVM> Turmas { get; set; }
    }
}