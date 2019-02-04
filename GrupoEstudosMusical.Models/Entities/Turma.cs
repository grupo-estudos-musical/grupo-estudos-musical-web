using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Models.Entities
{
    public class Turma:BaseEntity
    {
        public string Nome { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime TerminoAula { get; set; }
        public int Periodo { get; set; }
        public string Nivel { get; set; }
        public Professor Professor { get; set; }
        public string Status { get; set; }
        public int QuantidadeAlunos { get; set; }
        public Modulo Modulo { get; set; }
       
    }
}
