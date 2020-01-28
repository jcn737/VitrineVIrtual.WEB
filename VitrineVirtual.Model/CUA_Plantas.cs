using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VitrineVirtual.Model
{
    [Table("CUA_Plantas")]
    public class CUA_Plantas
    {
        [Key]
        public int ID_Planta { get; set; }

        public int ID_Empresa { get; set; }
        
        public string Descricao { get; set; }

        public char UF { get; set; }

        public char Status_Planta { get; set; }

        public char Franquia { get; set; }

        public char Centro { get; set; }

        public string Razao_Social { get; set; }

        [Required(ErrorMessage = "O CNPJ é obrigatório")]
        public char CNPJ { get; set; }

        public char Inscricao_Estadual { get; set; }

        public string Endereco { get; set; }

        public string Cidade { get; set; }

        public string Bairro { get; set; }

        public char CEP { get; set; }

        public char Pais { get; set; }

        public virtual CUA_Empresas CuaEmpresas { get; set; }
        public virtual ICollection<CUA_Plantas_Usuarios> CuaPlantasUsuarios { get; set; }


    }
}
