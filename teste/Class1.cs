using GrupoEstudosMusical.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace teste
{
    public class Class1 : DbContext
    {
        public DbSet<Aluno> Alunos { get; set; }
        public Class1()
        {
            
        }

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
