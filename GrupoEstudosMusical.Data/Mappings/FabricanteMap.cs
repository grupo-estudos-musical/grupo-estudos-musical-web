using GrupoEstudosMusical.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using static GrupoEstudosMusical.Models.Entities.Instrumento;

namespace GrupoEstudosMusical.Data.Mappings
{
    public class FabricanteMap : IEntityTypeConfiguration<Fabricante>
    {
        public void Configure(EntityTypeBuilder<Fabricante> builder)
        {
            builder.ToTable("Fabricantes");
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Nome).HasColumnType("varchar(50)").IsRequired();
            builder.Ignore(f => f.Id);
            builder.Property(f => f.DataCadastro).HasColumnType("date");
        }
    }
}
