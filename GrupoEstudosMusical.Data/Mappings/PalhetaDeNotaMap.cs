
using GrupoEstudosMusical.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrupoEstudosMusical.Data.Mappings
{
    public class PalhetaDeNotaMap : IEntityTypeConfiguration<PalhetaDeNota>
    {
        public void Configure(EntityTypeBuilder<PalhetaDeNota> builder)
        {
            builder.ToTable("PalhetaDeNotas");
            builder.HasKey(p => p.IdPalheta);
            builder.Property(p => p.DataCadastro).HasColumnType("date");
            builder.Property(p => p.Nota).HasColumnType("double");
            builder.HasOne(p => p.Matricula).WithMany(p => p.PalhetasDeNotas).HasForeignKey(p => p.MatriculaID);
            builder.HasOne(p => p.AvaliacaoTurma).WithMany(a => a.PalhetaDeNotas).HasForeignKey(p => p.AvaliacaoID);
            builder.Ignore(p => p.Id);
        }
    }
}
