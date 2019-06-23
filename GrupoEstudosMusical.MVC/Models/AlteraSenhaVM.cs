using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GrupoEstudosMusical.MVC.Models
{
    public class AlteraSenhaVM
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Senha")]
        [MinLength(6)]
        [MaxLength(8)]
        [DataType(DataType.Password)]
        public string NovaSenha { get; set; }
    }
}