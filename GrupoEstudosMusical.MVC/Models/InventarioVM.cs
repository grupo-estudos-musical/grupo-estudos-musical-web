using GrupoEstudosMusical.Models.Entities;
using System;


namespace GrupoEstudosMusical.MVC.Models
{
    public class InventarioVM
    {
        
        public Guid InventarioID { get; set; }
        public int QuantidadeDisponivel { get; set; } = 0;
        public int EstoqueMinimo { get; set; } = 1;
        public virtual Instrumento Instrumento { get; set; }
        public string NomeInstrumento { get; set; }
    }
}