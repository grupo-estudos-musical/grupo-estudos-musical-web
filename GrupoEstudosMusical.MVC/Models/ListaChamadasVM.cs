using System.Collections.Generic;

namespace GrupoEstudosMusical.MVC.Models
{
    public class ListaChamadasVM
    {
        public TurmaVM Turma { get; set; }
        public List<ChamadaVM> Chamadas { get; set; }
    }
}