
using System.Collections.Generic;

namespace GrupoEstudosMusical.Models.Entities.Relatorios
{
    public class Boletim
    {
        public List<PalhetaDeNota> PalhetasDeNotasDoAluno { get; set; }
        public Matricula MatriculaAluno { get; set; }
        public byte[] ImagemAluno { get; set; }
    }
}
