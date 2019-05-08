using System.Collections.Generic;

namespace GrupoEstudosMusical.MVC.Models
{
    public class ListaAulasVM
    {
        public TurmaVM Turma { get; set; }
        public List<AulaVM> Aulas { get; set; }
    }
}