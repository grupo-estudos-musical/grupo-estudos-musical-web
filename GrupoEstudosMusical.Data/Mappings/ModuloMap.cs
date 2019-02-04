using GrupoEstudosMusical.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrupoEstudosMusical.Data.Mappings
{
    public class ModuloMap : IEntityTypeConfiguration<Modulo>
    {
        public void Configure(EntityTypeBuilder<Modulo> builder)
        {
            builder.ToTable("Modulo");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id).ValueGeneratedOnAdd();

            builder.Property(m => m.Nome).HasColumnType("varchar(60)")
                .IsRequired();

            builder.Property(m => m.Observacoes).HasColumnType("varchar(300)").IsRequired();

            builder.Property(m => m.DataCadastro).HasColumnType("date");

        }
    }
}
