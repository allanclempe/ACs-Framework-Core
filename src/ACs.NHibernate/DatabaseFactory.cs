using System;
using System.Collections.Generic;
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using ACs.NHibernate.Generic;
using System.Linq;


namespace ACs.NHibernate
{
    public class DatabaseFactory : IDatabaseFactory, IDisposable
    {
        private static ISessionFactory _sessionFactory;

        public DatabaseFactory(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }
	    
		public virtual IDatabaseRequest BeginRequest(bool beginTransaction = true, TransactionIsolationLevel? isolationLevel = null)
        {
            ISession session = Session;

            if (session == null)
            {
                session = _sessionFactory.OpenSession();
                CurrentSessionContext.Bind(session);
            }

            return new DatabaseRequest(session)
                .Open(beginTransaction, isolationLevel);
        }

        public virtual void End()
        {
            if (Session == null)
                return;

            var session = Session;

            CurrentSessionContext.Unbind(_sessionFactory);

            if (session.IsConnected)
                session.Disconnect();

            if (session.IsOpen)
                session.Close();

            session.Dispose();
        }

        public ISession Session => GetSession();
        public static ISession GetSession()
        {
            return !CurrentSessionContext.HasBind(_sessionFactory) ? null : _sessionFactory.GetCurrentSession();
        }

        #region | Configuration Helper
        private static Assembly GetAssembly(string name)
        {
            var type = Type.GetType(name);

            if (type == null)
                throw new Exception(
                    $"Cannot find assembly name {name}. Please, configure correctly mappingfluent onto config file.");

            return type.Assembly;

        }

        public static ISessionFactory BuildSessionFactory(IDictionary<string, string> configuration)
        {
            return Fluently.Configure(new Configuration().AddProperties(configuration))
                .Mappings(m => m.FluentMappings.AddFromAssembly(GetAssembly(configuration["mappingfluent"]))
                    .Conventions.Add(DefaultLazy.Always()))
                .BuildSessionFactory();
        }
        #endregion

        public void Dispose()
        {
            End();
        }
    }
}
