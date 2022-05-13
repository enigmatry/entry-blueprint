using Enigmatry.Blueprint.Model.Identity;
using FluentAssertions;

namespace Enigmatry.Blueprint.Model.Tests.Identity
{
    [Category("unit")]
    public class UserQueryableExtensionsFixture
    {
        private IQueryable<User> _query = null!;
        private User _user = null!;
        private User _user2 = null!;

        [SetUp]
        public void Setup()
        {
            _user = new UserBuilder()
                .WithUserName("username1")
                .WithName("name");
            _user2 = new UserBuilder()
                .WithUserName("username2")
                .WithName("name2");

            _query = new List<User> { _user, _user2 }.AsQueryable();
        }

        [Test]
        public void TestQueryEmptyList()
        {
            var result = new List<User>().AsQueryable().QueryByUserName("some").ToList();
            result.Should().BeEmpty();
        }

        [TestCase("username1", TestName = "Case sensitive-should find")]
        [TestCase("username2", TestName = "Case sensitive-should find, v2")]
        [TestCase("userName1", TestName = "Case sensitive-should not find")]
        [TestCase("userName2", TestName = "Case sensitive-should not find, v2")]
        [TestCase("xyz", TestName = "Should not find")]
        public Task TestQueryByUserName(string userName)
        {
            var result = _query.QueryByUserName(userName).ToList();

            return Verify(result).UseParameters(userName).UseMethodName(TestContext.CurrentContext.Test.Name);
        }
    }
}
