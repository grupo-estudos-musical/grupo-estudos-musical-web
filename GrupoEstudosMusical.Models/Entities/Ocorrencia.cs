

using System;

namespace GrupoEstudosMusical.Models.Entities
{
    public class Ocorrencia:BaseEntity
    {
        public string Titulo { get; set; }
        public DateTime DataOcorrido { get; set; }
        public TiposOcorrenciaEnum Tipo { get; set; }
        public string Resumo { get; set; }
        public virtual Turma Turma { get; set; }
        public int TurmaID { get; set; }
        public string NomeProfessor { get; set; }
        
    }
}
