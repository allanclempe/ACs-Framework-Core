using ACs.Framework.Web.Core;
using FluentNHibernate.Mapping;

namespace ACs.Framework.Web.Map
{
    public class UserEmailMap: ClassMap<UserEmail>
    {
        public UserEmailMap()
        {
            Id(x => x.Id).GeneratedBy.Native();
            References(x => x.User);
            Map(x => x.Address);
            Map(x => x.IsPrimary);
        }
    }
}
