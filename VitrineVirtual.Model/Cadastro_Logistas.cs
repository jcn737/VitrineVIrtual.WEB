using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VitrineVirtual.Model
{
    [Table("Cadastro_Logistas")]
    public class Cadastro_Logistas
    {
        [Key]
        public int ID_Cadastro_Logista { get; set; }
    }
}
