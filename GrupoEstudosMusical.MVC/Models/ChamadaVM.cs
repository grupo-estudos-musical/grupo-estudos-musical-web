using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GrupoEstudosMusical.MVC.Models
{
    public class ChamadaVM
    {
        public int Id { get; set; }

        [DisplayName("Data Cadastro")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        [DisplayName("Observação")]
        public string Observacao { get; set; }

        [DisplayName("Presenças")]
        public int QuantidadePresenca => Frequencias.Where(f => f.Presenca).Count();

        [DisplayName("Faltas")]
        public int QuantidadeFaltas => Frequencias.Where(f => !f.Presenca).Count();

        [DisplayName("Frequência")]
        public double PorcentagemFrequencia
        {
            get
            {
                if (Frequencias.Count > 0)
                    return ((double)QuantidadePresenca / (double)Frequencias.Count) * 100;
                return 0;
            }
        }

        public int IdTurma { get; set; }

        public TurmaVM Turma { get; set; }

        public List<FrequenciaVM> Frequencias { get; set; } = new List<FrequenciaVM>();
    }
}