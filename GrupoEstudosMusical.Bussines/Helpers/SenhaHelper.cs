using System;
using System.Linq;

namespace GrupoEstudosMusical.Bussines.Helpers
{
    public static class SenhaHelper
    {
        public static string GerarSenhaNumericaAleatoria()
        {
            int tamanho = 8;
            var chars = "0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, tamanho)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());
            return result;
        }
    }
}
