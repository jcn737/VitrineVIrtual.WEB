using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VitrineVirtual.Model
{
    [Table("CUA_Usuarios")]
    public class CUA_Usuarios
    {
        [Key]
        public int ID_Usuario { get; set; }
        
        public string id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [DataType(DataType.Text)]
        //[MaxLength(30, ErrorMessage = "O email não pode ter mais que 30 caracteres")]]
        public string Nome_Completo { get; set; }

        [Required(ErrorMessage = "A login é obrigatória")]
        public string Login_Usuario { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        [RegularExpression("^[0-9]{1,14}$", ErrorMessage = "Somente Números")]
        public string Departamento { get; set; }

        public string Cargo { get; set; }

        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}"] 
        public DateTime? Data_Nascimento { get; set; }

        [RegularExpression("^[0-9]{1,14}$", ErrorMessage = "Somente Números")]
        public string Cpf { get; set; }

        [RegularExpression("^[0-9]{1,14}$", ErrorMessage = "Somente Números")]
        public string Rg { get; set; }

        [RegularExpression(@"^[1-9]\d+$", ErrorMessage = "Somente Números")]
        public string Cep { get; set; }

        public string Endereco { get; set; }

        public string Numero_Endereco { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public string UF { get; set; }

        public string Municipio { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        public char Status_Usuario { get; set; }

        public DateTime? Data_Criacao { get; set; }

        public DateTime? Data_Ultimo_Logon { get; set; }

        [Required(ErrorMessage = "O CNPJ é obrigatório")]
        [RegularExpression("^[0-9]{1,14}$", ErrorMessage = "Somente Números")]
        public string CNPJ { get; set; }

        public string Tipo_Usuario { get; set; }

        public string Situacao_Cadastral { get; set; }

        public virtual ICollection<CUA_Plantas_Usuarios> CuaPlantasUsuarios { get; set; }
        public virtual ICollection<CUA_Empresas_Usuario> CuaEmpresasUsuarios { get; set; }
    }

}
