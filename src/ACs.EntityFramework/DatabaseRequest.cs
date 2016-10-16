using System;
using System.Data;
using ACs.EntityFramework.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;


namespace ACs.EntityFramework
{
    public class DatabaseRequest : IDatabaseRequest
    {
        private readonly DbContext _session;
		
        public DatabaseRequest(DbContext session)
        {
            _session = session;
        }

        internal virtual IDatabaseRequest Open(bool beginTransaction = true, TransactionIsolationLevel? isolationLevel = null)
        {
	        if (!beginTransaction) return this;


	        if (!isolationLevel.HasValue)
	        {
		        BeginTransaction();
		        return this;
	        }

	        BeginTransaction(isolationLevel.Value);


	        return this;

        }

	    public void BeginTransaction()
	    {
			_session.Database.BeginTransaction();
		}

        public void BeginTransaction(TransactionIsolationLevel isolationLevel)
        {
			_session.Database.BeginTransaction(ParseIsolationLevel(isolationLevel));
        }

	    private IsolationLevel ParseIsolationLevel(TransactionIsolationLevel isolationLevel)
	    {
	        IsolationLevel iso;
			if (Enum.TryParse(isolationLevel.ToString(), out iso))
			{
				return iso;
			}

			throw new Exception("IsolationLevel not found.");

		}

		public virtual void CommitTransaction()
        {
			_session.Database.CommitTransaction();
        }

        public virtual void RollbackTransaction()
        {
            _session.Database.RollbackTransaction();
        }

        public virtual void Finish(bool forceRollback = false)
        {
            if (_session == null) return;

            try
            {
                if (forceRollback)
                {
                    _session.Database.RollbackTransaction();
                    return;
                }

                _session.Database.CommitTransaction();

            }
            catch (Exception)
            {
                _session.Database.RollbackTransaction();
                throw;
            }
        }


        public void Dispose()
        {
            Finish();
        }

    }
}
