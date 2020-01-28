using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VitrineVirtual.Model
{
    [Table("Cadastro_Empresas")]
    public class Cadastro_Empresas
    {
        [Key]
        public int ID_Cadastro_Empresa { get; set; }
    }
}
