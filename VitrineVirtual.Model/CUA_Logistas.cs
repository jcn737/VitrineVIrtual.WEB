using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VitrineVirtual.Model
{
    [Table("CUA_Logistas")]
    public class CUA_Logistas
    {
        [Key]
        public int ID_Cadastro_Logista { get; set; }
    }
}
