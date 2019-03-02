
using GrupoEstudosMusical.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrupoEstudosMusical.Data.Mappings
{
    public class OcorrenciaMap : IEntityTypeConfiguration<Ocorrencia>
    {
        public void Configure(EntityTypeBuilder<Ocorrencia> builder)
        {
            builder.ToTable("Ocorrencias");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id).ValueGeneratedOnAdd();

            builder.Property(o => o.Titulo).HasColumnType("varchar(70)").IsRequired();

            builder.Property(o => o.Resumo).HasColumnType("varchar(300)").IsRequired();

            builder.Property(o => o.DataCadastro).HasColumnType("date");

            builder.Property(o => o.DataOcorrido).HasColumnType("date").IsRequired();

            builder.Property(o => o.Tipo).HasColumnType("varchar(30)").IsRequired();

            builder.HasOne(o => o.Aluno).WithMany(a => a.Ocorrencias);

            builder.HasOne(o => o.Turma).WithMany(t => t.Ocorrencias);

        }
    }
}
