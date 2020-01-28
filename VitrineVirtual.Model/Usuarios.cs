using System.ComponentModel.DataAnnotations;

namespace VitrineVirtual.Model
{
    public class Usuarios
    {
        [Key]
        public long UsuarioId { get; set; }

        [Required(ErrorMessage = "O email é obrigatório")]
        [DataType(DataType.EmailAddress)]
        //[MaxLength(30, ErrorMessage = "O email não pode ter mais que 30 caracteres")]]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }

}
