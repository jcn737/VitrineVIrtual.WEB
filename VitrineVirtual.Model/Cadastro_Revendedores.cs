using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VitrineVirtual.Model
{
    [Table("Cadastro_Revendedores")]
    public class Cadastro_Revendedores
    {
        [Key]
        public int ID_Cadastro_Rev { get; set; }
    }
}
