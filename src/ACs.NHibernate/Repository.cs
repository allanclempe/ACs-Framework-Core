using System.Linq;
using NHibernate.Linq;
using ACs.NHibernate.Generic;

namespace ACs.NHibernate
{
    public abstract class Repository<T> : IRepository<T> where T : IEntityRoot, IEntityId
    {
        protected readonly IDatabaseFactory _factory;
        protected Repository(IDatabaseFactory factory)
        {
            _factory = factory;
        }

        public virtual T GetById(int id)
        {
            return (from q in _factory.Session.Query<T>()
                where q.Id == id
                select q).SingleOrDefault();
        }

        public virtual void Save(T entity)
        {
            _factory.Session.SaveOrUpdate(entity);
        }

        public virtual void SaveAndFlush(T entity)
        {
            _factory.Session.SaveOrUpdate(entity);
            _factory.Session.Flush();
        }

    }
}
