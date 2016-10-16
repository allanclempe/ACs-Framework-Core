using ACs.Framework.Web.Core;
using ACs.Framework.Web.Core.Infra;
using Moq;

namespace ACs.Framework.Web.Tests
{
    public class MockProvider
    {
        protected MockProvider()
        {
            SetupUser();
        }

        private void SetupUser()
        {
            User = new User("FirstName", "LastName", "some@domain.com", "123");
            User.GetType().GetProperty("Id").SetValue(User, 1);

            UserRepository = new Mock<IUserRepository>();
            UserRepository.Setup(x => x.GetById(User.Id)).Returns(User);
            UserRepository.Setup(x => x.GetByEmail(User.EmailPrimary.Address)).Returns(User);
            UserRepository.Setup(x => x.EmailExists(User.EmailPrimary.Address)).Returns(true);
        }

        public User User { get; set; }
        public Mock<IUserRepository> UserRepository { get; private set; }

        public static MockProvider GetInstance()
        {         
            return new MockProvider();
        }

    }
}
