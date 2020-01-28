using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VitrineVirtual.Model
{
    [Table("Carrinho_Compras")]
    public class Carrinho_Compras
    {
        [Key]
        public int ID_Carrinho_Compras { get; set; }

        public int ID_Cadastro_Prod_Loja { get; set; }

        public string ID_Usuario { get; set; }

        public string Nome_Produto { get; set; }

        public string Modelo_Produto { get; set; }

        public string Descricao_Produto { get; set; }

        public Nullable<decimal> Preco_Produto { get; set; }

        public Nullable<decimal> Total { get; set; }

        public Nullable<decimal> Valor_Frete { get; set; }

        public int Quantidade_Produto { get; set; }

        public string Caminho_Arquivo { get; set; }

        public string Tipo_Frete { get; set; }

        public string Peso_Itens { get; set; }

    }
}
 