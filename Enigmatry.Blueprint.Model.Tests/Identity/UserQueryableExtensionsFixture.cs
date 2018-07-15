using System;
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
        private IQueryable<User> _query;
        private User _user;
        private User _user2;

        [SetUp]
        public void Setup()
        {
            _user = new UserBuilder()
                .UserName("username1")
                .Name("name");
            _user2 = new UserBuilder()
                .UserName("username2")
                .Name("name2");

            _query = new List<User> {_user, _user2}.AsQueryable();
        }

        [Test]
        public void TestQueryEmptyList()
        {
            List<User> result = new List<User>().AsQueryable().ByUserName("some").ToList();
            result.Should().BeEmpty();
        }

        [TestCase("username1", true)]
        [TestCase("username2", true)]
        [TestCase("userName1", false)]
        [TestCase("userName2", false)]
        [TestCase("xyz", false)]
        public void TestQueryByUserName(string userName, bool expectedToFind)
        {
            List<User> result = _query.ByUserName(userName).ToList();

            result.Count.Should().Be(expectedToFind ? 1 : 0);
        }
    }
}