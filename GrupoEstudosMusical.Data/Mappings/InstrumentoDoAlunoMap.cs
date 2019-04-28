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
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Cor).HasColumnType("varchar(10)").IsRequired();
            builder.Property(i => i.DataCadastro).HasColumnType("date");
            builder.Property(i => i.DataEmprestimo).HasColumnType("date").IsRequired();
            builder.HasOne(i => i.Aluno).WithMany(i => i.Instrumentos).HasForeignKey(i => i.AlunoID);
            builder.HasOne(i => i.Fabricante).WithMany(i => i.Instrumentos).HasForeignKey(i => i.FabricanteID);
            builder.HasOne(i => i.Instrumento).WithMany(i => i.Instrumentos).HasForeignKey(i => i.InstrumentoID);
            builder.HasIndex(i => new { i.InstrumentoID, i.FabricanteID, i.AlunoID }).IsUnique();

        }
    }
}
