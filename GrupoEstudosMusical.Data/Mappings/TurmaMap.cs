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
    public class TurmaMap : IEntityTypeConfiguration<Turma>
    {
        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            builder.ToTable("Turmas");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.Property(t => t.Nome).HasColumnType("varchar(60)")
                .IsRequired();

            builder.Property(t => t.DataInicio).HasColumnType("date");

            builder.Property(t => t.TerminoAula).HasColumnType("date");

            builder.Property(t => t.DataCadastro).HasColumnType("date");

            builder.Property(t => t.Modulo).HasColumnType("int").IsRequired();

            builder.Property(t => t.Nivel).HasColumnType("varchar(15)").IsRequired();

            builder.Property(t => t.Periodo).HasColumnType("int").IsRequired();

            builder.Property(t => t.Professor).HasColumnType("int").IsRequired();

            builder.Property(t => t.Status).HasColumnType("varchar(11)").HasDefaultValue("Ativo").IsRequired();

            builder.Property(t => t.QuantidadeAlunos).HasColumnType("int").HasDefaultValue("0");



        }

    }
}
