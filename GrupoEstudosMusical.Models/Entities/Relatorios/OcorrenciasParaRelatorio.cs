

using System;

namespace GrupoEstudosMusical.Models.Entities.Relatorios
{
    public class OcorrenciasParaRelatorio
    {
        public string NomeDoAluno { get; set; }
        public string Titulo { get; set; }
        public DateTime  DataDoOcorrido { get; set; }
        public string Observacao { get; set; }
        public string NomeProfessor { get; set; }
    }
}
