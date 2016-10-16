using System.Collections.Generic;

namespace ACs.Security.Jwt
{
    public interface IJwtTokenConfiguration
    {
        string CertXml { get; set; }
        string Issuer { get; set; }
        string Audience { get; set; }

    }
}
