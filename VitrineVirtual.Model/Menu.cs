using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VitrineVirtual.Model
{
    [Table("Moda_Feminina")]
    public class Moda_Feminina
    {
        [Key]
        public int ID_Moda_Feminina { get; set; }
    }

    [Table("Moda_Masculina")]
    public class Moda_Masculina
    {
        [Key]
        public int ID_Moda_Masculina { get; set; }
    }

    [Table("Moda_Baby")]
    public class Moda_Baby
    {
        [Key]
        public int ID_Moda_Baby { get; set; }
    }

    [Table("Moda_Infanto_Juvenil")]
    public class Moda_Infanto_Juvenil
    {
        [Key]
        public int ID_Moda_Infanto_Juv { get; set; }
    }

    [Table("Moda_Praia")]
    public class Moda_Praia
    {
        [Key]
        public int ID_Moda_Praia { get; set; }
    }

    [Table("Moda_Fitness")]
    public class Moda_Fitness
    {
        [Key]
        public int ID_Moda_Fitness { get; set; }
    }

    [Table("Moda_Intima")]
    public class Moda_Intima
    {
        [Key]
        public int ID_Moda_Intima { get; set; }
    }

    [Table("Sapatos_Femininos")]
    public class Sapatos_Femininos
    {
        [Key]
        public int ID_Sapatos_Femininos { get; set; }
    }

    [Table("Sapatos_Masculinos")]
    public class Sapatos_Masculinos
    {
        [Key]
        public int ID_Sapatos_Masculinos { get; set; }
    }

    [Table("Sapatos_Infantis")]
    public class Sapatos_Infantis
    {
        [Key]
        public int ID_Sapatos_Infantis { get; set; }
    }
}
