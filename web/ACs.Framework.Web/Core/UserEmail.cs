using System;
using ACs.NHibernate.Generic;

namespace ACs.Framework.Web.Core
{
    public class UserEmail : IEntityId
    {
        protected UserEmail()
        {
        }

        public UserEmail(User user, string address)
            :this()
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (address == null) throw new ArgumentNullException(nameof(address));

            User = user;
            Address = ThreatmentAddress(address);

        }

        public virtual int Id { get; protected set; }
        public virtual User User { get; protected set; }
        public virtual string Address { get; protected set; }
        public virtual bool IsPrimary { get; protected internal set; }

        public virtual void SetAsPrimary()
        {
            foreach (var email in User.Emails)
                email.IsPrimary = false;

            IsPrimary = true;
        }

        public virtual bool Compare(string address)
        {
            return ThreatmentAddress(Address).Equals(ThreatmentAddress(address), StringComparison.CurrentCultureIgnoreCase);
        }

        public static string ThreatmentAddress(string address)
        {
            return string.IsNullOrEmpty(address) ? string.Empty : address.ToLower().Trim();
        }
    }
}
