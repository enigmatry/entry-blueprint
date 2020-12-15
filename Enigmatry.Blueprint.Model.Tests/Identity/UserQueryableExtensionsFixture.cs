using System.Collections.Generic;
using System.Linq;
using Enigmatry.Blueprint.Model.Identity;
using FluentAssertions;
using NUnit.Framework;

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

        [TestCase("username1", true)]
        [TestCase("username2", true)]
        [TestCase("userName1", false)]
        [TestCase("userName2", false)]
        [TestCase("xyz", false)]
        public void TestQueryByUserName(string userName, bool expectedToFind)
        {
            var result = _query.QueryByUserName(userName).ToList();

            result.Count.Should().Be(expectedToFind ? 1 : 0);
        }
    }
}
