using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VitrineVirtual.Model
{
    [Table("Faturamento_Rev")]
    public class Faturamento_Rev
    {
        [Key]
        public int ID_Faturamento_Rev { get; set; }
    }
}
