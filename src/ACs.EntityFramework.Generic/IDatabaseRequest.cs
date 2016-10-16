using System;

namespace ACs.EntityFramework.Generic
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
