using System.Collections.Generic;

namespace GrupoEstudosMusical.Email
{
    public class EmailMessage
    {
        public EmailMessage()
        {
            Anexos = new List<Anexo>();
        }
        public List<string> ToEmails { get; set; }
        public string Subject { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public List<Anexo> Anexos { get; set; }

        public class Anexo
        {
            public string NomeDoArquivo { get; set; }
            public byte[] Arquivo { get; set; }
        }

        public void AdicionarAnexos(Anexo anexo)
        {
            Anexos.Add(anexo);
        }
    }
}
