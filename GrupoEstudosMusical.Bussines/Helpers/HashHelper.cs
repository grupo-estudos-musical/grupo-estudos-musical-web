using System.Security.Cryptography;
using System.Text;

namespace GrupoEstudosMusical.Bussines.Helpers
{
    public class HashHelper
    {
        private readonly HashAlgorithm _algoritmo;

        public HashHelper(HashAlgorithm algoritmo)
        {
            _algoritmo = algoritmo;
        }

        public string CriptografarSenha(string senha)
        {
            var encodedValue = Encoding.UTF8.GetBytes(senha);
            var encryptedPassword = _algoritmo.ComputeHash(encodedValue);

            var sb = new StringBuilder();
            foreach (var caracter in encryptedPassword)
            {
                sb.Append(caracter.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
