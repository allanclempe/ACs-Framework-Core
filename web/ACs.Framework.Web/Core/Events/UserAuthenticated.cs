using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACs.Framework.Web.Core.Events
{
    public struct UserAuthenticated
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
