using System.Linq;
using ACs.EntityFramework.Generic;
using Microsoft.EntityFrameworkCore;

namespace ACs.EntityFramework
{
    public abstract class Repository<T> : IRepository<T> where T : class, IEntityRoot, IEntityId
	{	
        protected readonly DbContext Session;
        protected Repository(DbContext context)
        {
			Session = context;
        }

        public virtual T GetById(int id)
        {
            return (from q in Session.Set<T>().AsQueryable()
                where q.Id == id
                select q).SingleOrDefault();
        }

        public virtual void Save()
        {
			Session.SaveChanges();
        }

    }
}
