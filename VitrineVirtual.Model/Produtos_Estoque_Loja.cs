using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VitrineVirtual.Model
{
    [Table("Produtos_Estoque_Loja")]
    public class Produtos_Estoque_Loja
    {
        [Key]
        public int ID_Prod_Estoque_Loja { get; set; }
    }
}
