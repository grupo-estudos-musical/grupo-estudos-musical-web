using GrupoEstudosMusical.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Data.Mappings
{
    public class InventarioMap : IEntityTypeConfiguration<Inventario>
    {
        public void Configure(EntityTypeBuilder<Inventario> builder)
        {
            builder.ToTable("Inventario");
            builder.HasKey(i => i.InventarioID);
            builder.Property(i => i.DataCadastro).HasColumnType("date");
            builder.Property(i => i.EstoqueMinimo).IsRequired();
            builder.Property(i => i.QuantidadeDisponivel).IsRequired();
            builder.Ignore(i => i.Id);
            builder.HasOne(i => i.Instrumento).WithOne(a => a.Inventario).HasForeignKey<Inventario>(a => a.InventarioID).OnDelete(DeleteBehavior.Cascade);

        }

    }
}
