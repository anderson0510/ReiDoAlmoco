using System;
using System.Collections.Generic;
using System.Text;

namespace ReiDoAlmoco.Persistencia.Repositories
{
    public interface IGenericRepository<T>
    {
        void Inserir(T entity);
        void Alterar(T entity);
        void Deletar(int id);
        ICollection<T> ListarTodos();
        T BuscarPorId(int id);
    }
}
