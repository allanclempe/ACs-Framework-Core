using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Linq;
using Microsoft.IdentityModel.Tokens;

namespace ACs.Security.Jwt
{
    public class JwtTokenProvider : IJwtTokenProvider
    {
        private readonly IJwtTokenConfiguration _config;
        private JwtSecurityTokenHandler _handler;
        private RsaSecurityKey _key;
        private RSACryptoServiceProvider _provider;

        public JwtTokenProvider(IJwtTokenConfiguration config)
        {
            _config = config;
        }

        public string Audience {  get { return _config.Audience; } }
        public string Issuer { get { return _config.Issuer; } }

        public JwtSecurityTokenHandler Handler {
            get
            {
                return _handler ?? (_handler = new JwtSecurityTokenHandler());
            }
        }

        public RSACryptoServiceProvider Provider {
            get
            {
                if (_provider != null)
                    return _provider;

                _provider = new RSACryptoServiceProvider();
				_provider.FromXmlString(_config.CertXml);

                return _provider;

            }
        }

        public RsaSecurityKey Key {
            get
            {
                return _key ?? (_key = new RsaSecurityKey(Provider.ExportParameters(true)));
            }
        }

        public string GetToken(DateTime? notBefore = null, DateTime? expiress = null, params Claim[] claims )
        {
            
            return Handler.WriteToken(new JwtSecurityToken(
               issuer: _config.Issuer,
               audience: _config.Audience,
               signingCredentials: new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature),
               notBefore: notBefore,
               expires: expiress,
               claims: claims               
           ));
        }

        public static string GenerateCert256Xml()
        {
            var myRsa = new RSACryptoServiceProvider(2048);

            return myRsa.ToXmlString(true);
        }

		public IList<Claim> GetClaimsFromToken(string token)
		{
			var securityToken = Handler.ReadToken(token) as JwtSecurityToken;
			if (securityToken == null)
				return new List<Claim>();

			return securityToken.Claims.ToList();
		}

	}
}
