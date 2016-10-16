
namespace ACs.EntityFramework.Generic
{
    public interface IRepository<T> where T : IEntityRoot, IEntityId
    {
        T GetById(int id);
        void Save();
    }
}
