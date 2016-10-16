using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACs.Security.Jwt;

namespace ACs.Framework.Web.Core.Infra
{
    public class JwtTokenConfiguration : IJwtTokenConfiguration
	{
		public string CertXml { get; set; }
		public string Issuer { get; set; }
		public string Audience { get; set; }
	}
}
