using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VitrineVirtual.Model
{
    [Table("Despesas_Lucros_Rev")]
    public class Despesas_Lucros_Rev
    {
        [Key]
        public int ID_Despesas_Lucros_Rev { get; set; }
    }
}
