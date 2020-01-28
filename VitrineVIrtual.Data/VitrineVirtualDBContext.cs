using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using VitrineVirtual.Model;

namespace VitrineVirtual.Data
{
    public class VitrineVirtualDBContext : IdentityDbContext
    {
        public VitrineVirtualDBContext()
            : base("name=VitrineVirtualDBContext")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<CUA_Usuarios> Usuarios { get; set; }
        public DbSet<Faturamento_Rev> FaturamentoRev { get; set; }
        public DbSet<Faturamento_Loja> FaturamentoLoja { get; set; }
        public DbSet<Faturamento_Adm> FaturamentoAdm { get; set; }
        public DbSet<Despesas_Lucros_Rev> FaturamentoLucrosRev { get; set; }
        public DbSet<Despesas_Lucros_Loja> DespesasLucrosLoja { get; set; }
        public DbSet<Despesas_Lucros_Adm> DespesasLucrosAdm { get; set; }
        public DbSet<Compras_Pag_Seguro> ComparaPagSeguro { get; set; }
        public DbSet<Catalogo_Produtos_Rev> CatalogoProdutoRev { get; set; }
        public DbSet<Catalogo_Produto_Loja> CatalogoProdutoLoja { get; set; }
        public DbSet<Carrinho_Compras> CarrinhoCompras { get; set; }
        public DbSet<CUA_Revendedores> CadastroRev { get; set; }
        public DbSet<Cadastro_Produto_Rev> CadastroProdutoRev { get; set; }
        public DbSet<Cadastro_Produto_Loja> CadastroProdutoLoja { get; set; }
        public DbSet<CUA_Logistas> Lojista { get; set; }
        public DbSet<CUA_Empresas> Empresas { get; set; }
        public DbSet<CUA_Administradores> CadastroAdm { get; set; }
        public DbSet<Menu_Itens> MenuItens { get; set; }
        public DbSet<FilePath> FilePath { get; set; }
        public DbSet<CUA_Plantas> Plantas { get; set; }
        public DbSet<CUA_Empresas_Usuario> CuaEMpresasUsuarios { get; set; }
        public DbSet<CUA_Plantas_Usuarios> CuaPlantasUsuarios { get; set; }
        public DbSet<CEP> Cep { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            Database.SetInitializer<VitrineVirtualDBContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }

}
