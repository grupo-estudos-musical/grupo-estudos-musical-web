namespace GrupoEstudosMusical.MVC.Models
{
    public class TurmasMatriculaVM
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int QuantidadeAlunos { get; set; }
        public int QuantidadeMatriculas { get; set; }
        public bool AlunoMatriculado { get; set; }
    }
}