using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace VitrineVirtual.WEB.Repositorio
{
    public class RepoGenericoEntity<TEntidade, TChave> : IRepoGenerico<TEntidade, TChave>
    where TEntidade : class
    {
        protected DbContext _contexto;

        public RepoGenericoEntity(DbContext contexto)
        {
            _contexto = contexto;
        }

        public void Alterar(TEntidade entidade)
        {
            _contexto.Set<TEntidade>().Attach(entidade);
            _contexto.Entry(entidade).State = EntityState.Modified;
            _contexto.SaveChanges();
        }

        public void Excluir(TEntidade entidade)
        {
            _contexto.Set<TEntidade>().Attach(entidade);
            _contexto.Entry(entidade).State = EntityState.Deleted;
            _contexto.SaveChanges();
        }

        public void ExcluirPorId(TChave id)
        {
            TEntidade entidade = selecionaPorId(id);
            Excluir(entidade);
        }

        public void Inserir(TEntidade entidade)
        {
            _contexto.Set<TEntidade>().Add(entidade);
            _contexto.SaveChanges();
        }

        public virtual TEntidade selecionaPorId(TChave id)
        {
            return _contexto.Set<TEntidade>().Find(id);
        }

        public virtual List<TEntidade> Selecionar()
        {
            return _contexto.Set<TEntidade>().ToList();
        }
    }

}