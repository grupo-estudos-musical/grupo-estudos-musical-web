using System;
using System.ComponentModel.DataAnnotations;

namespace GrupoEstudosMusical.Models.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}
