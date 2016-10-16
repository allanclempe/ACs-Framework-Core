using System.Collections;
using NHibernate.Context;
using NHibernate.Engine;

namespace ACs.NHibernate.AspnetCore
{
    public class Web5SessionContext : MapBasedSessionContext
    {
        private const string SessionFactoryMapKey = "NHibernate.Context.WebSessionContext.SessionFactoryMapKey";

        public Web5SessionContext(ISessionFactoryImplementor factory) : base(factory) { }

        protected override IDictionary GetMap()
        {
            return HttpContextHelper.Current.Items[SessionFactoryMapKey] as IDictionary;
        }

        protected override void SetMap(IDictionary value)
        {
            HttpContextHelper.Current.Items.Add(SessionFactoryMapKey, value);
        }

    }
}
