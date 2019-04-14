using GrupoEstudosMusical.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrupoEstudosMusical.Data.Mappings
{
    public class FrequenciaMap : IEntityTypeConfiguration<Frequencia>
    {
        public void Configure(EntityTypeBuilder<Frequencia> builder)
        {
            builder.ToTable("Frequencias");

            builder.HasKey(f => new { f.IdChamada, f.IdAluno });

            builder.Property(f => f.Presenca)
                .HasColumnType("bit")
                .IsRequired();

            builder.HasOne(f => f.Chamada)
                .WithMany(c => c.Frequencias)
                .HasForeignKey(f => f.IdChamada);

            builder.HasOne(f => f.Aluno)
                .WithMany(a => a.Frequencias)
                .HasForeignKey(f => f.IdAluno);

            builder.Ignore(f => f.Id);
            builder.Ignore(f => f.DataCadastro);
        }
    }
}
