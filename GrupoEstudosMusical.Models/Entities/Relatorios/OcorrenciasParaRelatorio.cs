

using System.Collections.Generic;

namespace GrupoEstudosMusical.Models.Entities.Relatorios
{
    public class OcorrenciasParaRelatorio
    {
        public OcorrenciasParaRelatorio()
        {
            Ocorrencias = new List<Ocorrencia>();
        }
        public Aluno Aluno { get; set; }
        public List<Ocorrencia> Ocorrencias { get; set; }
    }
}
