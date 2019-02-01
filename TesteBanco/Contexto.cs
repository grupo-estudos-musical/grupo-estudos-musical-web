using GrupoEstudosMusical.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TesteBanco
{
    public class Contexto : DbContext
    {
        public DbSet<Aluno> Alunos { get; set; }

        public List<Aluno> GetAll()
        {
            return Alunos.ToList();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var con = "server=localhost;port=3306;database=GrupoEstudosMusical;uid=root;password=root";
            optionsBuilder.UseMySql(con);
        }
    }
}
