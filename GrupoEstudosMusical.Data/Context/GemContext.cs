﻿using GrupoEstudosMusical.Data.Mappings;
using GrupoEstudosMusical.Models;
using GrupoEstudosMusical.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using static GrupoEstudosMusical.Models.Entities.Instrumento;


namespace GrupoEstudosMusical.Data.Context
{
    public class GemContext : DbContext
    {
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Modulo> Modulos { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Ocorrencia> Ocorrencias { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<AvaliacaoTurma> AvaliacaoTurmas { get; set; }
        public DbSet<Chamada> Chamadas { get; set; }
        public DbSet<Frequencia> Frequencias { get; set; }
        public DbSet<PalhetaDeNota> PalhetaDeNotas { get; set; }
        public DbSet<Instrumento> Instrumentos { get; set; }
        public DbSet<Fabricante> Fabricantes { get; set; }
        public DbSet<InstrumentoDoAluno> InstrumentoDoAlunos { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public GemContext() : base() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            optionsBuilder.UseMySql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OcorrenciaMap());                
            modelBuilder.ApplyConfiguration(new TurmaMap());
            modelBuilder.ApplyConfiguration(new ModuloMap());
            modelBuilder.ApplyConfiguration(new AlunoMap());
            modelBuilder.ApplyConfiguration(new ProfessorMap());
            modelBuilder.ApplyConfiguration(new MatriculaMap());
            modelBuilder.ApplyConfiguration(new AvaliacaoMap());
            modelBuilder.ApplyConfiguration(new AvaliacaoTurmaMap());
            modelBuilder.ApplyConfiguration(new ChamadaMap());
            modelBuilder.ApplyConfiguration(new FrequenciaMap());
            modelBuilder.ApplyConfiguration(new PalhetaDeNotaMap());
            modelBuilder.ApplyConfiguration(new AulaMap());
            modelBuilder.ApplyConfiguration(new InstrumentoMap());
            modelBuilder.ApplyConfiguration(new InstrumentoDoAlunoMap());
            modelBuilder.ApplyConfiguration(new InventarioMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
        }
    }
}
