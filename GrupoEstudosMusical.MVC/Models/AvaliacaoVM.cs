

using System;
using System.ComponentModel;

namespace GrupoEstudosMusical.MVC.Models
{
    public class AvaliacaoVM
    {
        public AvaliacaoVM()
        {
            this.DataCadastro = DateTime.Now;
        }
        public string Nome { get; set; }
        [DisplayName("Nota máxima")]
        public double NotaMaxima { get; set; }
        public int Peso { get; set; }
        public int Id { get; set; }
        public DateTime DataCadastro { get; private set; } 
    }
}