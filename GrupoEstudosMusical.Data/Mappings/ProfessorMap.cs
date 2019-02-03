using GrupoEstudosMusical.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrupoEstudosMusical.Data.Mappings
{
    public class ProfessorMap : IEntityTypeConfiguration<Professor>
    {
        public void Configure(EntityTypeBuilder<Professor> builder)
        {
            builder.ToTable("Professores");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Nome)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(p => p.Telefone)
                .HasColumnType("varchar(14)")
                .IsRequired();

            builder.Property(p => p.Celular)
                .HasColumnType("varchar(15)");

            builder.Property(p => p.Email)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(p => p.Cep)
                .HasColumnType("varchar(9)");

            builder.Property(p => p.Endereco)
                .HasColumnType("varchar(180)")
                .IsRequired();

            builder.Property(p => p.Complemento)
                .HasColumnType("varchar(50)");

            builder.Property(p => p.Bairro)
                .HasColumnType("varchar(60)")
                .IsRequired();

            builder.Property(p => p.Cidade)
                .HasColumnType("varchar(60)")
                .IsRequired();

            builder.Property(p => p.Uf)
                .HasColumnType("varchar(2)")
                .IsRequired();            
        }
    }
}
