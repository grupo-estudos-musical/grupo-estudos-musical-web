using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GrupoEstudosMusical.Data.Mappings
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(u => u.Senha)
                .HasColumnType("varchar(130)")
                .IsRequired();

            builder.Property(u => u.NivelAcesso)
                .HasColumnType("varchar(50)")
                .HasConversion(
                    u => u.ToString(),
                    u => (NivelAcessoEnum)Enum.Parse(typeof(NivelAcessoEnum), u));

            builder.Property(u => u.DataCadastro)
                .HasColumnType("date");

            builder.Property(u => u.Guid)
                .HasColumnType("varchar(36)");
        }
    }
}
