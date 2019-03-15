

namespace GrupoEstudosMusical.Bussines.Helpers
{
    public static class ValidacaoHelper
    {
		public static void ValidaString(string valor)
        {
            if (string.IsNullOrEmpty(valor))
            {
                throw new System.Exception("String não pode ser nulo ou vazio")
            }
        }
		
    }
}
