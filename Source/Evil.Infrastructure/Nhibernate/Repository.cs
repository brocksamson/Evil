using System.Linq;
using Evil.Common;
using NHibernate;
using NHibernate.Linq;

namespace Evil.Infrastructure.Nhibernate
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly ISession _session;

        public Repository(ISession session)
        {
            _session = session;
        }

        public T GetById(int id)
        {
            return _session.Load<T>(id);
        }

        public IQueryable<T> Get
        {
            get { return _session.Query<T>(); }
        }

        public void Save(T entity)
        {
            _session.SaveOrUpdate(entity);
        }

        public void Delete(T entity)
        {
            _session.Delete(entity);
        }
    }
}