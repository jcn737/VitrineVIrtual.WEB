using System.Collections.Generic;

namespace VitrineVirtual.WEB.Repositorio
{
    public interface IRepoGenerico<TEntidade, TChave>
    where TEntidade : class
    {
        List<TEntidade> Selecionar();
        TEntidade selecionaPorId(TChave id);
        void Inserir(TEntidade entidade);
        void Alterar(TEntidade entidade);
        void Excluir(TEntidade entidade);
        void ExcluirPorId(TChave id);
    }

}
