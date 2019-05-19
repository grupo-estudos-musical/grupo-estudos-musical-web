using GrupoEstudosMusical.Models.Enums;

namespace GrupoEstudosMusical.Models.Entities
{
    public class Usuario : BaseEntity
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public NivelAcessoEnum NivelAcesso { get; set; }
        public Aluno Aluno { get; set; }
        public Professor Professor { get; set; }
    }
}
