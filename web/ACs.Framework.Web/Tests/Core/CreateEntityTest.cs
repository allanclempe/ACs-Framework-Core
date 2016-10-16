using System;
using ACs.Framework.Web.Core;
using Moq;
using Xunit;

namespace ACs.Framework.Web.Tests.Core
{
    public class CreateEntityTest
    {

        [Theory, InlineData("FirstName", "LastName", "user@domain.com", "123")]
        public void CreateUser(string firstName, string lastName, string email, string password)
        {
            var user = new User(firstName, lastName, email, password);

            Assert.Equal(firstName, user.FirstName);
            Assert.Equal(lastName, user.LastName);
            Assert.NotNull(user.EmailPrimary);
            Assert.NotNull(user.Password);
            Assert.Equal(UserStatus.Created, user.Status);
            Assert.NotEqual(DateTime.MinValue, user.CreatedOn);
            Assert.NotEqual(Guid.Empty, user.Token);
        }

        [Theory, InlineData("user@domain.com")]
        public void CreateEmail(string address)
        {
            var user = new Mock<User>();
            var userEmail = new UserEmail(user.Object, address);

            Assert.Equal(address, userEmail.Address);
            Assert.False(userEmail.IsPrimary);
        }

    }
}
