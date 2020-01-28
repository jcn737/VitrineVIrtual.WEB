using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VitrineVirtual.Model
{
    [Table("Catalogo_Produtos_Rev")]
    public class Catalogo_Produtos_Rev
    {
        [Key]
        public int ID_Catalogo_Prod_Rev { get; set; }
    }
}
