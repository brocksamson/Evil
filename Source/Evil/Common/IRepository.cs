using System.Linq;

namespace Evil.Common
{
    public interface IRepository<T> where T : Entity
    {
        T GetById(int id);
        IQueryable<T> Get { get; }
        void Save(T entity);
        void Delete(T entity);
    }
}
