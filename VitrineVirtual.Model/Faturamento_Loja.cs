using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VitrineVirtual.Model
{
    [Table("Faturamento_Loja")]
    public class Faturamento_Loja
    {
        [Key]
        public int ID_Faturamento_Loja { get; set; }
    }
}
