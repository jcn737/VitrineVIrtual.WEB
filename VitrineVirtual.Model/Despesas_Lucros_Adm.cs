using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VitrineVirtual.Model
{
    [Table("Despesas_Lucros_Adm")]
    public class Despesas_Lucros_Adm
    {
        [Key]
        public int ID_Despesas_Lucros_Adm { get; set; }
    }
}
