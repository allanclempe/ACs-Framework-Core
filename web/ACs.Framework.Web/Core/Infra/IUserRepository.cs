using System.Collections.Generic;
using ACs.Framework.Web.Core.Dtos;
using ACs.NHibernate.Generic;

namespace ACs.Framework.Web.Core.Infra
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByEmail(string address);
        IList<UserDto> GetAll();
        bool EmailExists(string address);
    }
}
