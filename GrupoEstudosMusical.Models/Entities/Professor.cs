﻿using System.Collections.Generic;

namespace GrupoEstudosMusical.Models.Entities
{
    public class Professor : BaseEntity
    {        
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public List<Turma> Turmas { get; set; } = new List<Turma>();
    }
}
