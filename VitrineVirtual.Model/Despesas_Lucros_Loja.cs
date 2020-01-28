using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VitrineVirtual.Model
{
    [Table("Despesas_Lucros_Loja")]
    public class Despesas_Lucros_Loja
    {
        [Key]
        public int ID_Despesas_Lucros_Loja { get; set; }
    }
}
