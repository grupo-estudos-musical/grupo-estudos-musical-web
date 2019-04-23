using GrupoEstudosMusical.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrupoEstudosMusical.Data.Mappings
{
    public class ChamadaMap : IEntityTypeConfiguration<Chamada>
    {
        public void Configure(EntityTypeBuilder<Chamada> builder)
        {
            builder.ToTable("Chamadas");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd();

            builder.Property(c => c.DataCadastro)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(c => c.Observacao)
                .HasColumnType("varchar(200)");

            builder.HasOne(c => c.Turma)
                .WithMany(t => t.Chamadas)
                .HasForeignKey(c => c.IdTurma);
        }
    }
}
