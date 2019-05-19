using GrupoEstudosMusical.Models.Enums;
using System;

namespace GrupoEstudosMusical.MVC.Models
{
    public class UsuarioVM
    {
        public int Id { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public NivelAcessoEnum NivelAcesso { get; set; }
    }
}