using GrupoEstudosMusical.MVC.Models;
using System;
using System.Linq;
using System.Text;

namespace GrupoEstudosMusical.MVC.Helpers
{
    public class TokenHelper
    {
        private string _secretKey = "grupomusical";

        public string GenerateToken(UsuarioVM usuario)
        {
            byte[] key = Encoding.ASCII.GetBytes(_secretKey);
            byte[] guid = Encoding.ASCII.GetBytes(usuario.Guid);
            byte[] id = Encoding.ASCII.GetBytes(usuario.Id.ToString());
            byte[] data = new byte[key.Length + guid.Length + id.Length];

            Buffer.BlockCopy(key, 0, data, 0, key.Length);
            Buffer.BlockCopy(guid, 0, data, key.Length, guid.Length);
            Buffer.BlockCopy(id, 0, data, guid.Length, id.Length);

            return Convert.ToBase64String(data.ToArray());
        }

        public bool ValidateToken(UsuarioVM usuario, string token)
        {
            byte[] data = Convert.FromBase64String(token);
            byte[] key = data.Take(12).ToArray();
            byte[] guid = data.Skip(12).Take(36).ToArray();
            byte[] id = data.Skip(48).ToArray();

            if (Encoding.UTF8.GetString(key) != _secretKey)
                return false;

            if (Encoding.UTF8.GetString(guid) != usuario.Guid)
                return false;

            if (Encoding.UTF8.GetString(id) != usuario.Id.ToString())
                return false;

            return true;
        }
    }
}