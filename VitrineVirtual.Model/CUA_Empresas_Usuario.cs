using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitrineVirtual.Model
{
    [Table("CUA_Empresas_Usuario")]
    public class CUA_Empresas_Usuario
    {
        [Key]
        public int ID_Aplicacao { get; set; }
        
        public int ID_Empresa { get; set; }

        public int ID_Usuario { get; set; }

        public int Usuario_Pertence_Empresa { get; set; }

        public virtual CUA_Empresas CuaEmpresas { get; set; }

        public virtual CUA_Usuarios CuaUsuarios { get; set; }


    }
}
