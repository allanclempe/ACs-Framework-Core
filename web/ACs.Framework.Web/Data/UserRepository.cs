using System.Collections.Generic;
using System.Linq;
using ACs.Framework.Web.Core;
using ACs.Framework.Web.Core.Dtos;
using ACs.Framework.Web.Core.Infra;
using ACs.NHibernate;
using NHibernate.Linq;

namespace ACs.Framework.Web.Data
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IDatabaseFactory factory) : base(factory)
        {
        }

        public IList<UserDto> GetAll()
        {
            return (from user in _factory.Session.Query<User>()
                from userEmail in user.Emails.Where(x => x.IsPrimary).DefaultIfEmpty()
                select new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = userEmail.Address
                })
                .ToList();
        }
        public User GetByEmail(string address)
        {
            address = UserEmail.ThreatmentAddress(address);

            return (from userEmail in _factory.Session.Query<UserEmail>()
                    let user = userEmail.User
                where userEmail.Address == address 
                select user)
                .SingleOrDefault();
        }

        public bool EmailExists(string address)
        {
            return (from userEmail in _factory.Session.Query<UserEmail>()
                where userEmail.Address == address
                select userEmail).Any();
        }
    }
}
