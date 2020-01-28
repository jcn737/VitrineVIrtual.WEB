using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VitrineVirtual.Model
{
    [Table("Menu_Itens")]
    public class Menu_Itens
    {
        [Key]
        public int ID_Menu_Itens { get; set; }
    }
}
