using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VitrineVirtual.Model
{
    [Table("Catalogo_Produto_Loja")]
    public class Catalogo_Produto_Loja
    {
        [Key]
        public int id_Catalogo_Produto_Loja { get; set; }
    }
}
