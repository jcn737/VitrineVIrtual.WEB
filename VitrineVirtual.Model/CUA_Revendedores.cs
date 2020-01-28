using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VitrineVirtual.Model
{
    [Table("CUA_Revendedores")]
    public class CUA_Revendedores
    {
        [Key]
        public int ID_Cadastro_Rev { get; set; }
    }
}
