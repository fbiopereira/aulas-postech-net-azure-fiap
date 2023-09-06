using FiapStore.Entities;

namespace FiapStore.Interfaces
{
    public interface IRepository<T> where T : Entidade
    {
        IList<T> ObterTodos();
        T ObterPorId(int id);
        void Cadastrar(T entidade);
        void Alterar(T entidade);
        void Deletar(int id);

    }
}
