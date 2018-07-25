using System;
using System.Collections.Generic;
using System.Text;

namespace ReiDoAlmoco.Persistencia.Repositories
{
    public interface IVotoRepository<T> : IGenericRepository<T> where T:class
    {
        ICollection<T> VotosHoje(DateTime hoje);
    }
}
