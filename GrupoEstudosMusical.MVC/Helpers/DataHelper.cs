using System;
using System.Collections.Generic;
using System.Linq;

namespace GrupoEstudosMusical.MVC.Helpers
{
    public static class DataHelper
    {
        private static readonly IDictionary<string, string> _diasDaSemana = new Dictionary<string, string>
        {
            { "Sunday", "Domingo" },
            { "Monday", "Segunda-Feira" },
            { "Tuesday", "Terça-Feira" },
            { "Wednesday", "Quarta-Feira" },
            { "Thursday", "Quinta-Feira" },
            { "Friday", "Sexta-Feira" },
            { "Saturday", "Sábado" }
        };

        public static string ObterDiaDaSemana(this DateTime data) =>
            _diasDaSemana.FirstOrDefault(k => k.Key == data.DayOfWeek.ToString()).Value;
    }
}