using GrupoEstudosMusical.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrupoEstudosMusical.Data.Mappings
{
    public class MatriculaMap : IEntityTypeConfiguration<Matricula>
    {
        public void Configure(EntityTypeBuilder<Matricula> builder)
        {
            builder.ToTable("Matriculas");

            builder.Property(m => m.Id)
                .ValueGeneratedOnAdd();

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Cpf)
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(m => m.Rg)
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(m => m.ComprovanteResidencia)
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(m => m.Pendente)
                .HasColumnType("bit")
                .IsRequired();

            builder.HasOne(m => m.Turma)
                .WithMany(t => t.Matriculas)
                .HasForeignKey(m => m.TurmaId);

            builder.HasOne(m => m.Aluno)
                .WithMany(a => a.Matriculas)
                .HasForeignKey(m => m.AlunoId);

            builder.Ignore(m => m.Aluno);

            builder.Ignore(m => m.Turma);
        }
    }
}