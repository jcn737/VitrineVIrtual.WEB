using System.ComponentModel.DataAnnotations;

namespace VitrineVirtual.Model
{
    public class FilePath
    {
        [Key]
        public int ID_Caminho { get; set; }
        [StringLength(255)]
        public string Nome_Arquivo { get; set; }
        public string Nome_Arquivo_2 { get; set; }
        public string Nome_Arquivo_3 { get; set; }
        public string Nome_Arquivo_4 { get; set; }
        public string Nome_Arquivo_5 { get; set; }
        public string Tipo_Arquivo { get; set; }
        public string Caminho_Arquivo { get; set; }
        public string Caminho_Arquivo_2 { get; set; }
        public string Caminho_Arquivo_3 { get; set; }
        public string Caminho_Arquivo_4 { get; set; }
        public string Caminho_Arquivo_5 { get; set; }
        public int ID_Cadastro_Prod_Loja { get; set; }

        public virtual Cadastro_Produto_Loja CadastroProdutoLoja { get; set; }

    }
}