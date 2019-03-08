namespace GrupoEstudosMusical.MVC.Models
{
    public class MatriculaListaVM
    {
        public int Id { get; set; }
        public TurmaVM Turma { get; set; }
        public bool Pendente { get; set; }
        public AlunoVM Aluno { get; set; }

        public string SituacaoMatricula
        {
            get { return Pendente ? "Pendente" : "Ok"; }
        }
    }
}