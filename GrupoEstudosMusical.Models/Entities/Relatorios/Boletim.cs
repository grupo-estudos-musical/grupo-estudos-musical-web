﻿
using System.Collections.Generic;

namespace GrupoEstudosMusical.Models.Entities.Relatorios
{
    public class Boletim
    {
        public Boletim(){}
        public Boletim(Matricula matricula, List<PalhetaDeNota> palhetaDeNotas)
        {
            this.PalhetasDeNotasDoAluno = palhetaDeNotas;
            this.MatriculaAluno = MatriculaAluno;
        }
        public List<PalhetaDeNota> PalhetasDeNotasDoAluno { get; set; }
        public Matricula MatriculaAluno { get; set; }
        public byte[] ImagemAluno { get; set; }
    }
}
