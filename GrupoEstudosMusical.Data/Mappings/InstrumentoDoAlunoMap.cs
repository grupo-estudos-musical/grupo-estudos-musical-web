using GrupoEstudosMusical.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrupoEstudosMusical.Data.Mappings
{
    public class InstrumentoDoAlunoMap : IEntityTypeConfiguration<InstrumentoDoAluno>
    {
        public void Configure(EntityTypeBuilder<InstrumentoDoAluno> builder)
        {
            builder.ToTable("InstrumentoDoAluno");
            builder.HasKey(i => i.IdInstrumentoDoAluno);
            builder.Property(i => i.Cor).HasColumnType("varchar(10)").IsRequired();
            builder.Property(i => i.DataCadastro).HasColumnType("date");
            builder.Property(i => i.DataEmprestimo).HasColumnType("date").IsRequired();
            builder.HasOne(i => i.Aluno).WithMany(i => i.Instrumentos).HasForeignKey(i => i.AlunoID);
            builder.HasOne(i => i.Fabricante).WithMany(i => i.InstrumentosDoAluno).HasForeignKey(i => i.FabricanteID).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(i => i.Inventario).WithMany(i => i.InstrumentosDoAluno).HasForeignKey(i => i.InventarioID).OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(i => new { i.InventarioID, i.FabricanteID, i.AlunoID }).IsUnique();
            builder.Property(i => i.Status).HasColumnType("varchar(11)").IsRequired();
            builder.Ignore(i => i.Id);
            

        }
    }
}
