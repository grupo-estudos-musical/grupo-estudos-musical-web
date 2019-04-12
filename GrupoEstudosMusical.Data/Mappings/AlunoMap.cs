using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GrupoEstudosMusical.Data.Mappings
{
    public class AlunoMap : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.ToTable("Alunos");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .ValueGeneratedOnAdd();

            builder.Property(a => a.Nome)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(a => a.DataNascimento)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(a => a.Telefone)
                .HasColumnType("varchar(14)");

            builder.Property(a => a.Celular)
                .HasColumnType("varchar(15)");

            builder.Property(a => a.Rg)
                .HasColumnType("varchar(12)");

            builder.Property(a => a.Cpf)
                .HasColumnType("varchar(14)");

            builder.HasIndex(a => a.Cpf)
                .IsUnique();

            builder.Property(a => a.Email)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(a => a.TipoResponsavel)
                .HasColumnType("varchar(50)")
                .HasConversion(
                    v => v.ToString(),
                    v => (TipoResponsavelEnum)Enum.Parse(typeof(TipoResponsavelEnum), v));

            builder.Property(a => a.NomeResponsavel)
                .HasColumnType("varchar(50)");

            builder.Property(a => a.Cep)
                .HasColumnType("varchar(9)");

            builder.Property(a => a.Logradouro)
                .HasColumnType("varchar(180)")
                .IsRequired();

            builder.Property(a => a.Numero)
                .HasColumnType("varchar(10)")
                .IsRequired();

            builder.Property(a => a.Complemento)
                .HasColumnType("varchar(50)");

            builder.Property(a => a.Bairro)
                .HasColumnType("varchar(60)")
                .IsRequired();

            builder.Property(a => a.Cidade)
                .HasColumnType("varchar(60)")
                .IsRequired();

            builder.Property(a => a.Uf)
                .HasColumnType("varchar(2)")
                .IsRequired();

            builder.Property(a => a.DataCadastro)
                .HasColumnType("date");

            builder.Property(a => a.ImagemUrl)
                .HasColumnType("varchar(300)")
                .IsRequired();
        }
    }
}
