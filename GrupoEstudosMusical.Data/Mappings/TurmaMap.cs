using GrupoEstudosMusical.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace GrupoEstudosMusical.Data.Mappings
{
    public class TurmaMap : IEntityTypeConfiguration<Turma>
    {
        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            builder.ToTable("Turmas");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.Property(t => t.Nome).HasColumnType("varchar(60)");

            builder.Property(t => t.DataInicio).HasColumnType("date");

            builder.Property(t => t.TerminoAula).HasColumnType("date");

            builder.Property(t => t.DataCadastro).HasColumnType("date");

            builder.Property(t => t.Nivel).HasColumnType("varchar(15)").IsRequired();

            builder.Property(t => t.Periodo).HasColumnType("int").IsRequired();

            builder.Property(t => t.Status).HasColumnType("varchar(11)").HasDefaultValue("Ativo").IsRequired();

            builder.Property(t => t.QuantidadeAlunos).HasColumnType("int").HasDefaultValue("0");

            builder.Property(t => t.Semestre).HasColumnType("int").IsRequired();
            
            builder.HasOne(t => t.Modulo)
                .WithMany(m => m.Turmas);

            builder.HasOne(t => t.Professor)
                .WithMany(p => p.Turmas);

            builder.HasIndex(t => new { t.Nome, t.Periodo, t.Semestre }).IsUnique();
            builder.Ignore(m => m.Matriculas);
        }
    }
}
