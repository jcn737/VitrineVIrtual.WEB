using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VitrineVirtual.Model
{
    [Table("Compras_PagSeguro")]
    public class Compras_Pag_Seguro
    {
        [Key]
        public int ID_Compras_PagSeguro { get; set; }
    }
}
