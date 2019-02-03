using GrupoEstudosMusical.Data.Mappings;
using GrupoEstudosMusical.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace GrupoEstudosMusical.Data.Context
{
    public class GemContext : DbContext
    {
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Modulo> Modulos { get; set; }

        public GemContext() : base() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            optionsBuilder.UseMySql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ModuloMap());
            modelBuilder.ApplyConfiguration(new AlunoMap());
            modelBuilder.ApplyConfiguration(new ProfessorMap());
        }
    }
}
