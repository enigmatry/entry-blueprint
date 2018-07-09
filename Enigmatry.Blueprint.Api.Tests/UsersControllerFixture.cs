using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enigmatry.Blueprint.Api.Models.Identity;
using Enigmatry.Blueprint.Api.Tests.Infrastructure.Api;
using Enigmatry.Blueprint.Model;
using Enigmatry.Blueprint.Model.Identity;
using FluentAssertions;
using NUnit.Framework;

namespace Enigmatry.Blueprint.Api.Tests
{
    [Category("integration")]
    public class UsersControllerFixture : IntegrationFixtureBase
    {
        private DateTimeOffset _createdDate;

        [SetUp]
        public void SetUp()
        {
            _createdDate = Resolve<ITimeProvider>().Now;
            User user = User.Create("userName", "someName", _createdDate);

            AddToRepository(user);
            SaveChanges();
        }

        [Test]
        public async Task Users_GetAll()
        {
            List<UserModel> users = (await Client.GetAsync<IEnumerable<UserModel>>("api/users")).ToList();
            
            users.Count.Should().Be(1, "we saved one user to the db");
            
            UserModel user = users.First();
            user.UserName.Should().Be("userName");
            user.Name.Should().Be("someName");
            user.CreatedOn.Should().Be(_createdDate);
        }
    }
}