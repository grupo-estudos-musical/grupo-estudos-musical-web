using GrupoEstudosMusical.Data.Mappings;
using GrupoEstudosMusical.Models.Entities;
using MySql.Data.Entity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Data.Context
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class GemContext : DbContext
    {
        public DbSet<Aluno> Alunos { get; set; }

        public GemContext() : base("name=DefaultConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<DateTime>()
                .Configure(p => p.HasColumnType("date"));

            modelBuilder.Configurations.Add(new AlunoMapping());
        } 

        public override int SaveChanges()
        {
            SetarValorPadraoDataCadastro();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync()
        {
            SetarValorPadraoDataCadastro();
            return await base.SaveChangesAsync();
        }

        private void SetarValorPadraoDataCadastro()
        {
            //antes de salvar procura por uma propriedade chamada DataCadastro
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                //se o objeto estiver no estado Added, seta a propriedade DataAtual com a data atual
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                //se o objeto estiver no estado Modified, não modifica a propriedade DataCadastro
                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }
        }
    }
}
