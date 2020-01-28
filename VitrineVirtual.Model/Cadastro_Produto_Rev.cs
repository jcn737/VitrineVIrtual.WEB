using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VitrineVirtual.Model
{
    [Table("Cadastro_Produto_Rev")]
    public class Cadastro_Produto_Rev
    {
        [Key]
        public int ID_Cadastro_Prod_Rev { get; set; }
    }
}
