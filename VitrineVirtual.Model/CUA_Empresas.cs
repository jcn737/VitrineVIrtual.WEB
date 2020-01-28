using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VitrineVirtual.Model
{
    [Table("CUA_Empresas")]
    public class CUA_Empresas
    {
        [Key]
        public int ID_Empresa { get; set; }

        [Required(ErrorMessage = "O Nome da empresa é obrigatório")]
        [DataType(DataType.Text)]
        public string Nome_Fantasia { get; set; }

        public string Status_Empresa { get; set; }

        public DateTime? Data_Criacao { get; set; }

        public string Razao_Social { get; set; }

        [DataType(DataType.PostalCode)]
        [RegularExpression("^[0-9]{1,14}$", ErrorMessage = "Somente Números")]
        public string CEP { get; set; }

        public string Endereco { get; set; }

        [RegularExpression("^[0-9]{1,14}$", ErrorMessage = "Somente Números")]
        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [RegularExpression("^[0-9]{1,14}$", ErrorMessage = "Somente Números")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O CNPJ da empresa é obrigatório")]
        [RegularExpression("^[0-9]{1,14}$", ErrorMessage = "Somente Números")]
        public string CNPJ { get; set; }

        public string Matriz_Filial { get; set; }

        public string Situacao_Cadastral { get; set; }

        public string Natureza_Juridica { get; set; }

        public DateTime? Abertura_Empresa { get; set; }
        
        public virtual ICollection<CUA_Empresas_Usuario> CuaEmpresasUsuarios { get; set; }

        public virtual ICollection<CUA_Plantas> CuaPlantas { get; set; }

        public virtual ICollection<Cadastro_Produto_Loja> CadastroProdutoLoja { get; set; }
    }
}
