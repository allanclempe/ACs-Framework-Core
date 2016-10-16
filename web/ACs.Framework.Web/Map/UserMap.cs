using FluentNHibernate.Mapping;
using ACs.Framework.Web.Core;

namespace ACs.Framework.Web.Map
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id).GeneratedBy.Native();
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.Password);
            Map(x => x.Token);
            Map(x => x.Status).CustomType<UserStatus>();

            HasMany(x => x.Emails).Inverse().Cascade.SaveUpdate();

        }
    }
}
