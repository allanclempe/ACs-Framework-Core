using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using ACs.EntityFramework.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ACs.EntityFramework
{
    public class DatabaseFactory : IDatabaseFactory, IDisposable
    {
	    public DbContext Session { get; }

	    public DatabaseFactory(DbContext sessionFactory)
        {
            Session = sessionFactory;
        }
	    
		public virtual IDatabaseRequest BeginRequest(bool beginTransaction = true, TransactionIsolationLevel? isolationLevel = null)
        {
            if (Session.Database.GetDbConnection().State == ConnectionState.Closed)
            {
				Session.Database.OpenConnection();
			}

            return new DatabaseRequest(Session)
                .Open(beginTransaction, isolationLevel);
        }

        public virtual void End()
        {
			Session.Dispose();
        }

        public void Dispose()
        {
            End();
        }
    }
}
