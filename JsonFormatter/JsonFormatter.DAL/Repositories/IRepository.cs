using System.Threading.Tasks;

namespace JsonFormatter.DAL.Repositories
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(int id);
    }
}
