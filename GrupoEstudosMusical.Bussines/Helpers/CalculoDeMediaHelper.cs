
using GrupoEstudosMusical.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GrupoEstudosMusical.Bussines.Helpers
{
    public static class CalculoDeMediaHelper
    {

        public static double CalcularMediaDoAluno(List<PalhetaDeNota> palhetasDoAluno) =>
            Convert.ToDouble(palhetasDoAluno.Sum(p => p.Nota) / palhetasDoAluno.Count);
        
    }
}
