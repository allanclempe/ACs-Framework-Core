using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACs.Framework.Web.Core.Events
{
    public class UserActivated
    {
        public string Token { get; set; }
        public int Id { get; set; }
    }
}
