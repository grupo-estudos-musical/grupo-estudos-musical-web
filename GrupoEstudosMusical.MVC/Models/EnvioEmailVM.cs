using System.Collections.Generic;

namespace GrupoEstudosMusical.MVC.Models
{
    public class EnvioEmailVM
    {
        public string Assunto { get; set; }
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
        public List<string> IdsTurma { get; set; }
       
    }
}