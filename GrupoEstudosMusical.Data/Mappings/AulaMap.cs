using GrupoEstudosMusical.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrupoEstudosMusical.Data.Mappings
{
    public class AulaMap : IEntityTypeConfiguration<Aula>
    {
        public void Configure(EntityTypeBuilder<Aula> builder)
        {
            builder.ToTable("Aulas");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.DataCadastro)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(a => a.Conteudo)
                .HasColumnType("varchar(300)")
                .IsRequired();

            builder.HasOne(a => a.Turma)
                .WithMany(t => t.Aulas)
                .HasForeignKey(a => a.TurmaId);
        }
    }
}
