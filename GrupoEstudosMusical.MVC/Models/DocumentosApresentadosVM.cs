using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrupoEstudosMusical.MVC.Models
{
    public class DocumentosApresentadosVM
    {
        public int IdMatricula { get; set; }
        public bool Cpf { get; set; }
        public bool Rg { get; set; }
        public bool ComprovanteResidencia { get; set; }
    }
}