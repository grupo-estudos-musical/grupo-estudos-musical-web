using GrupoEstudosMusical.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Data.Mappings
{
    public class AvaliacaoMap : IEntityTypeConfiguration<Avaliacao>
    {
        public void Configure(EntityTypeBuilder<Avaliacao> builder)
        {
            builder.ToTable("Avaliacoes");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.DataCadastro).HasColumnType("date");

            builder.Property(a => a.Nome).HasColumnType("varchar(100)").IsRequired();

            builder.Property(a => a.NotaMaxima).HasColumnType("int").IsRequired();

            builder.Property(a => a.Peso).HasColumnType("int").IsRequired();

            builder.HasIndex(a => a.Nome).IsUnique();

        }
    }
}
