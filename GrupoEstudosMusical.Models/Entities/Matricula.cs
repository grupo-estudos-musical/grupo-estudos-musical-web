namespace GrupoEstudosMusical.Models.Entities
{
    public class Matricula : BaseEntity
    {
        public virtual Turma Turma { get; set; }
        public virtual int TurmaId { get; set; }
        public Aluno Aluno { get; set; }
        public int AlunoId { get; set; }
        public bool Cpf { get; set; }
        public bool Rg { get; set; }
        public bool ComprovanteResidencia { get; set; }
        public bool Pendente { get; private set; }

        public bool VerificarMatriculaPendente()
        {
            if (Cpf && Rg && ComprovanteResidencia)
            {
                Pendente = false;
                return false;
            }

            Pendente = true;
            return true;
        }
    }
}
