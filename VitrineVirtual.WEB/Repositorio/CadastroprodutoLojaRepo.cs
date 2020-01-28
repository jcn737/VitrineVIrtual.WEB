using System.Collections.Generic;
using VitrineVirtual.Data;
using VitrineVirtual.Model;
using System.Data.Entity;
using System.Linq;

namespace VitrineVirtual.WEB.Repositorio
{
    public class CadastroprodutoLojaRepo : RepoGenericoEntity<Cadastro_Produto_Loja, int>
    {
        public CadastroprodutoLojaRepo(VitrineVirtualDBContext contexto)
            : base(contexto)
        {

        }

        public override List<Cadastro_Produto_Loja> Selecionar()
        {
            return _contexto.Set<Cadastro_Produto_Loja>().Include(p => p.ID_Cadastro_Prod_Loja).ToList();
        }

        public override Cadastro_Produto_Loja selecionaPorId(int id)
        {
            return _contexto.Set<Cadastro_Produto_Loja>().Include(p => p.ID_Cadastro_Prod_Loja).SingleOrDefault(a => a.ID_Cadastro_Prod_Loja == id);
        }
    }
}