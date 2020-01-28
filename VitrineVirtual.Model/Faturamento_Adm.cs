using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VitrineVirtual.Model
{
    [Table("Faturamento_Adm")]
    public class Faturamento_Adm
    {
        [Key]
        public int ID_Faturamento_Adm { get; set; }
    }
}
