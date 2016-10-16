using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using ACs.Framework.Web.Core;
using ACs.Framework.Web.Core.Events;
using ACs.Framework.Web.Core.Infra;
using ACs.Security.Jwt;
using Xunit;

namespace ACs.Framework.Web.Tests.Core.Events
{
    public class UserEventTest
    {
        private readonly MockProvider _mockProvider;
        private readonly IUserEvent _userEvent;
        public UserEventTest()
        {
            _mockProvider = MockProvider.GetInstance();
            _userEvent = new UserEvent(_mockProvider.UserRepository.Object, new JwtTokenProvider(new JwtTokenConfiguration
            {
                Audience = "test",
                Issuer = "test",
                CertXml = JwtTokenProvider.GenerateCert256Xml()
            }));

        }

        [Theory, InlineData("FirstName", "LastName", "user@domain.com", "123")]
        public void Insert(string firstName, string lastName, string email, string password)
        {
            var model = new UserInserted
            {
                FirstName = firstName,
                LastName = lastName,
                Password = password,
                Email = email
            };

           var user =  _userEvent.DoIt(model);

            _mockProvider.UserRepository.Verify(x=>x.SaveAndFlush(user));

            Assert.NotNull(user);

        }

        [Theory, InlineData("some@domain.com")]
        public void InsertWithExistentEmail(string email)
        {
            var model = new UserInserted {
                Email = email
            };

            var ex = Assert.Throws<SystemLogicException>(() => _userEvent.DoIt(model));
            
            Assert.Equal(ExceptionMessage.UserEmailAlreadyExists, ex.MessageType);

        }

        [Fact]
        public void Activate()
        {
            var model = new UserActivated {Id = 1, Token = _mockProvider.User.Token.ToString()};

            var user = _userEvent.DoIt(model);
            
            _mockProvider.UserRepository.Verify(x=>x.Save(user));

            Assert.Equal(UserStatus.Activated, user.Status);

        }

        [Fact]
        public void ActivateWithInvalidToken()
        {
            var model = new UserActivated { Id = 1, Token = "893283290"};

            var ex = Assert.Throws<SystemLogicException>(() => _userEvent.DoIt(model));

            Assert.Equal(ExceptionMessage.InvalidToken, ex.MessageType);

        }

        [Fact]
        public void Autenticate()
        {
            var model = new UserAuthenticated {Email = _mockProvider.User.EmailPrimary.Address, Password = "123"};
            var bearerToken = _userEvent.DoIt(model);
       

            var token = new JwtSecurityToken(bearerToken);
            var claims = token.Claims;
            var idclaim = claims.SingleOrDefault(x => x.Type == ClaimTypes.Sid);
                
            Assert.NotNull(idclaim);
            Assert.Equal(_mockProvider.User.Id, int.Parse(idclaim.Value));
        }

    }
}
