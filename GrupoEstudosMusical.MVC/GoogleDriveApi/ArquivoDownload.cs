using System.IO;

namespace GrupoEstudosMusical.MVC.GoogleDriveApi
{
    public class ArquivoDownload
    {
        public string Nome { get; set; }
        public MemoryStream Stream { get; set; }
        public string MimeType { get; set; }
        public string Extensao { get; set; }
    }
}