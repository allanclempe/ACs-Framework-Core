using System;

namespace ACs.NHibernate.Generic
{
    public interface IDatabaseRequest : IDisposable
    {
        void BeginTransaction();
        void BeginTransaction(TransactionIsolationLevel isolationLevel);
        void CommitTransaction();
        void RollbackTransaction();
        void Finish(bool forceRollback = false);
    }
}
