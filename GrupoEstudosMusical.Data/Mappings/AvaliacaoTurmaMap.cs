

using GrupoEstudosMusical.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrupoEstudosMusical.Data.Mappings
{
    public class AvaliacaoTurmaMap : IEntityTypeConfiguration<AvaliacaoTurma>
    {
        public void Configure(EntityTypeBuilder<AvaliacaoTurma> builder)
        {
            builder.ToTable("AvaliacoesTurmas");
            builder.HasKey(a => new {a.AvaliacaoID, a.TurmaID});
            builder.Property(a => a.DataPrevista).HasColumnType("date").IsRequired();
            builder.Property(a => a.DataCadastro).HasColumnType("date").IsRequired();
            builder.Ignore(a => a.Id);
            
        }
    }
}
