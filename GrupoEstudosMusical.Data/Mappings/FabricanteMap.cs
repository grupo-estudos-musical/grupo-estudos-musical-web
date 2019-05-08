
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static GrupoEstudosMusical.Models.Entities.Instrumento;

namespace GrupoEstudosMusical.Data.Mappings
{
    public class FabricanteMap : IEntityTypeConfiguration<Fabricante>
    {
        public void Configure(EntityTypeBuilder<Fabricante> builder)
        {
            builder.ToTable("Fabricantes");
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Nome).HasColumnType("varchar(50)").IsRequired();
            builder.Property(f => f.DataCadastro).HasColumnType("date");
        }
    }
}
