
using GrupoEstudosMusical.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrupoEstudosMusical.Data.Mappings
{
    public class InstrumentoMap : IEntityTypeConfiguration<Instrumento>
    {
        public void Configure(EntityTypeBuilder<Instrumento> builder)
        {
            builder.ToTable("Instrumentos");
            builder.HasKey(i => i.IntrumentoID);
            builder.Property(i => i.Nome).HasColumnType("varchar(20)").IsRequired();
            builder.Property(i => i.DataCadastro).HasColumnType("date");
            builder.Ignore(i => i.Id);
        }

        
    }
}
