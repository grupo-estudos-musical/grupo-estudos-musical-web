using GrupoEstudosMusical.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace GrupoEstudosMusical.Data.Mappings
{
    public class AlunoMapping : EntityTypeConfiguration<Aluno>
    {
        public AlunoMapping()
        {
            ToTable("Alunos");

            HasKey(a => a.Id);

            Property(a => a.Nome)
                .IsRequired()
                .HasMaxLength(100);

            Property(a => a.DataNascimento)
                .IsRequired();

            Property(a => a.Telefone)
                .HasMaxLength(10);

            Property(a => a.Celular)
                .HasMaxLength(11);

            Property(a => a.Rg)
                .HasMaxLength(9);

            HasIndex(a => a.Rg)
                .IsUnique();

            Property(a => a.Email)
                .IsRequired()
                .HasMaxLength(50);

            Property(a => a.Cpf)
                .HasMaxLength(11);

            HasIndex(a => a.Cpf)
                .IsUnique();

            Property(a => a.Cep)
                .HasMaxLength(8);

            Property(a => a.Endereco)
                .IsRequired()
                .HasMaxLength(180);

            Property(a => a.Complemento)
                .HasMaxLength(50);

            Property(a => a.Bairro)
                .IsRequired()
                .HasMaxLength(60);

            Property(a => a.Cidade)
                .IsRequired()
                .HasMaxLength(60);

            Property(a => a.Uf)
                .IsRequired()
                .HasMaxLength(2);
        }
    }
}
