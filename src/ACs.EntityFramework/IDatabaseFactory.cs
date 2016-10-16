using ACs.EntityFramework.Generic;
using Microsoft.EntityFrameworkCore;

namespace ACs.EntityFramework
{
    public interface IDatabaseFactory 
    {
        DbContext Session { get; }
        IDatabaseRequest BeginRequest(bool beginTransaction = true, TransactionIsolationLevel? isolationLevel = null);
        void End();
    }
}
