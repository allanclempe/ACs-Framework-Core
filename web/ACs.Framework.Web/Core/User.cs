using System;
using System.Collections.Generic;
using System.Linq;
using ACs.Framework.Web.Core.Infra;
using ACs.NHibernate.Generic;
using ACs.Security;

namespace ACs.Framework.Web.Core
{
    public class User : IEntityRoot, IEntityId
    {
        protected User()
        {
            CreatedOn = DateTime.UtcNow;
            Status = UserStatus.Created;
            Token = Guid.NewGuid();
        }
 
        public User(string firstName, string lastName, string emailAddress, string password)
            :this()
        {
            if (firstName == null) throw new ArgumentNullException(nameof(firstName));
            if (lastName == null) throw new ArgumentNullException(nameof(lastName));
            if (password == null) throw new ArgumentNullException(nameof(password));

            FirstName = firstName;
            LastName = lastName;
            Emails = new List<UserEmail>();

            SetPassword(password);
            AddEmail(emailAddress, true);
        }

        public virtual int Id { get; protected set; }
        public virtual Guid Token { get; protected set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Password { get; protected set; }
        public virtual IList<UserEmail> Emails { get; protected set; }
        public virtual UserEmail EmailPrimary => Emails.FirstOrDefault(x => x.IsPrimary);
        public virtual DateTime CreatedOn { get; protected set; }
        public virtual UserStatus Status { get; protected set; }

        public virtual void SetPassword(string password)
        {
            Password = PasswordHash.CreateHash(password);
        }

        public virtual bool IsValidPassword(string password)
        {
            return PasswordHash.ValidatePassword(password, Password);
        }

        public virtual UserEmail AddEmail(string address, bool primary = false)
        {
            var email = new UserEmail(this, address);

            if (primary)
                email.SetAsPrimary();

            Emails.Add(email);

            return email;
        }

        public virtual void Activate(Guid token)
        {
            if (Status != UserStatus.Created)
                throw new SystemLogicException(ExceptionMessage.UserInvalidStatus);

            if (!token.Equals(Token))
                throw new SystemLogicException(ExceptionMessage.InvalidToken);
            

            Status = UserStatus.Activated;
        }

        public virtual void Block()
        {
            Status = UserStatus.Blocked;
        }

    }
}
