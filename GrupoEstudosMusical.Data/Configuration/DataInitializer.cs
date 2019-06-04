using GrupoEstudosMusical.Data.Context;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Enums;
using System;
using System.Linq;

namespace GrupoEstudosMusical.Data.Configuration
{
    public class DataInitializer : IDataInitializer
    {
        private readonly GemContext _context;

        public DataInitializer()
        {
            _context = new GemContext();
        }

        public void ExecuteMigrations()
        {
            _context.Database.EnsureCreated();
        }

        public void Seed()
        {
            var usuarios = _context.Usuarios.ToList();

            if (usuarios.Count == 0)
            {
                var usuario = new Usuario
                {
                    DataCadastro = DateTime.Now,
                    Email = "adm@gem.com.br",
                    NivelAcesso = NivelAcessoEnum.Administrador,
                    Nome = "Rafael",
                    Senha = "B26631F4D7E7C4C95CFED395A2BB64B0141ADB1F8A38F2823241FCE9EF81A4D61BD445640A25CD548995C4B1BED08BFD74A5D83CB5EF946B5BEACF727E81C8F6"
                };

                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
            }
        }
    }
}
