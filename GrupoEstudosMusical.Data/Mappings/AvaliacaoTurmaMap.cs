

using GrupoEstudosMusical.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrupoEstudosMusical.Data.Mappings
{
    public class AvaliacaoTurmaMap : IEntityTypeConfiguration<AvaliacaoTurma>
    {
        public void Configure(EntityTypeBuilder<AvaliacaoTurma> builder)
        {
            builder.ToTable("AvaliacoesDaTurma");
            builder.HasKey(a => a.IdAvaliacaoTurma);
            builder.HasOne(a => a.Turma).WithMany(t => t.AvaliacoesTurmas)
                .HasForeignKey(a => a.TurmaID);
            builder.HasOne(a => a.Avaliacao).WithMany(a => a.AvaliacoesTurmas)
                .HasForeignKey(a => a.AvaliacaoID);

            builder.HasIndex(a => new { a.AvaliacaoID, a.TurmaID }).IsUnique();
            // builder.HasKey(a => new {a.AvaliacaoID, a.TurmaID});
            builder.Property(a => a.DataPrevista).HasColumnType("date").IsRequired();
            builder.Property(a => a.DataCadastro).HasColumnType("date").IsRequired();
            builder.Ignore(a => a.Id);
            
        }
    }
}
