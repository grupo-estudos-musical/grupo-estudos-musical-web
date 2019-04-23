namespace GrupoEstudosMusical.Models.Entities
{
    public class Frequencia : BaseEntity
    {
        public int IdChamada { get; set; }        
        public int IdAluno { get; set; }
        public bool Presenca { get; set; }
        public Chamada Chamada { get; set; }
        public Aluno Aluno { get; set; }
    }
}
