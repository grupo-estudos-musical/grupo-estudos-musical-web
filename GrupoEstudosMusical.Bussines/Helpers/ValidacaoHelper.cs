

using System;

namespace GrupoEstudosMusical.Bussines.Helpers
{
    public static class ValidacaoHelper
    {
		public static void ValidaString(string valor)
        {
            if (string.IsNullOrEmpty(valor))
            {
                throw new System.Exception("String não pode ser nulo ou vazio");
            }
        }
        public static void ValidarSeDataInicialEhMaiorQueAFinal(DateTime dataInicial, DateTime dataFinal)
        {
            if (dataInicial > dataFinal)
                throw new Exception("Data inicial não pode ser superior a data final");
        }

        
		
    }
}
