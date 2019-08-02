using System.Collections.Generic;

namespace RateLimit.DAL.Repositoriy
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
    }
}
