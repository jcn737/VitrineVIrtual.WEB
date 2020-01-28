using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitrineVirtual.Model
{
    [Table("CUA_Plantas_Usuarios")]
    public class CUA_Plantas_Usuarios
    {
        [Key]
        public int ID_Planta_Usuarios { get; set; }

        public int ID_Planta { get; set; }
        
        public int ID_Usuario { get; set; }

        public virtual CUA_Plantas CuaPlantas { get; set; }

        public virtual CUA_Usuarios CuaUsuarios { get; set; }


    }
}
