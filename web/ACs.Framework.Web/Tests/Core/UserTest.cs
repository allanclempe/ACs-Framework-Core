using System;
using ACs.Framework.Web.Core;
using ACs.Framework.Web.Core.Infra;
using Xunit;

namespace ACs.Framework.Web.Tests.Core
{
    public class UserTest
    {
        private readonly MockProvider _entityProvider;

        public UserTest()
        {
            _entityProvider = MockProvider.GetInstance();
        }

        [Theory, InlineData("123")]
        public void PasswordIsValid(string password)
        {
            _entityProvider.User.SetPassword(password);

            Assert.True(_entityProvider.User.IsValidPassword(password));
        }

        [Theory, InlineData("other@domain.com")]
        public void AddEmail(string email)
        {
            var userEmail = _entityProvider.User.AddEmail(email, true);

            Assert.NotNull(userEmail);
            Assert.Equal(2, _entityProvider.User.Emails.Count);
            Assert.Equal(email, _entityProvider.User.EmailPrimary.Address);

        }

        [Fact]
        public void Activate()
        {
            var user = _entityProvider.User;
            var token = _entityProvider.User.Token;

            user.Activate(token);
            
            Assert.Equal(UserStatus.Activated, user.Status);
        }

        [Fact]
        public void ActivateWithInvalidStatus()
        {
            var user = _entityProvider.User;
            var token = _entityProvider.User.Token;

            user.Block();

            var ex = Assert.Throws<SystemLogicException>(() => user.Activate(token));
            Assert.Equal(ExceptionMessage.UserInvalidStatus, ex.MessageType);

        }

        [Fact]
        public void ActivateWithUnmatchToken()
        {
            var user = _entityProvider.User;
            var token = Guid.NewGuid();

            var ex = Assert.Throws<SystemLogicException>(() => user.Activate(token));

            Assert.Equal(ExceptionMessage.InvalidToken, ex.MessageType);

        }

        [Fact]
        public void Block()
        {
            var user = _entityProvider.User;

            user.Block();

            Assert.Equal(UserStatus.Blocked, user.Status);

        }
    }
}
