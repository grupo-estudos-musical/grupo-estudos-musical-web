using System.Collections.Generic;

namespace GrupoEstudosMusical.Email
{
    public class EmailMessage
    {
        public List<string> ToEmails { get; set; }
        public string Subject { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
