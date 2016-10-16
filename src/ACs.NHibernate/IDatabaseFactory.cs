
using NHibernate;
using ACs.NHibernate.Generic;

namespace ACs.NHibernate
{
    public interface IDatabaseFactory 
    {
        ISession Session { get; }
        IDatabaseRequest BeginRequest(bool beginTransaction = true, TransactionIsolationLevel? isolationLevel = null);
        void End();
    }
}
