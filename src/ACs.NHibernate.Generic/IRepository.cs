
namespace ACs.NHibernate.Generic
{
    public interface IRepository<T> where T : IEntityRoot, IEntityId
    {
        T GetById(int id);
        void Save(T entity);
        void SaveAndFlush(T entity);
    }
}
