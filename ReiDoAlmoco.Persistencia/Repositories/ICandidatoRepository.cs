using System;
using System.Collections.Generic;
using System.Text;

namespace ReiDoAlmoco.Persistencia.Repositories
{
    public interface ICandidatoRepository<T> : IGenericRepository<T> where T:class
    {
        T BuscarCandidatoPorEmail(string email);
    }
}
