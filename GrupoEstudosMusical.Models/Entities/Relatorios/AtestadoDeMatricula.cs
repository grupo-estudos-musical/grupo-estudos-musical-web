using System;

namespace GrupoEstudosMusical.Models.Entities.Relatorios
{
    public class AtestadoDeMatricula
    {
        public Matricula Matricula { get; set; }
        public DateTime DataMatricula { get; set; }
        public string MatriculadoPor { get; set; }
        public Modulo Modulo { get; set; }
    }
}
