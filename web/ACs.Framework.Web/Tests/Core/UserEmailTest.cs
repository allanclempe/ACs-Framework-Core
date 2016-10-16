using Xunit;

namespace ACs.Framework.Web.Tests.Core
{
    public class UserEmailTest
    {
        private readonly MockProvider _entityProvider;

        public UserEmailTest()
        {
            _entityProvider = MockProvider.GetInstance();
        }

        [Theory, InlineData("sOme@domaIn.com")]
        public void Compare(string password)
        {
            Assert.True(_entityProvider.User.EmailPrimary.Compare(password));
        }

    }
}
