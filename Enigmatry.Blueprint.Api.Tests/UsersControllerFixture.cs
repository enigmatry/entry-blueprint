using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enigmatry.Blueprint.Api.Models.Identity;
using Enigmatry.Blueprint.Api.Tests.Infrastructure.Api;
using Enigmatry.Blueprint.Model;
using Enigmatry.Blueprint.Model.Identity;
using Enigmatry.Blueprint.Model.Tests.Identity;
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
            User user = new UserBuilder()
                .UserName("userName")
                .Name("John Doe")
                .CreatedOn(_createdDate);
            
            AddToRepository(user);
            SaveChanges();
        }

        [Test]
        public async Task TestGetAll()
        {
            List<UserModel> users = (await Client.GetAsync<IEnumerable<UserModel>>("api/users")).ToList();

            users.Count.Should().Be(1, "we saved one user to the db");

            UserModel user = users.First();
            user.UserName.Should().Be("userName");
            user.Name.Should().Be("John Doe");
            user.CreatedOn.Should().Be(_createdDate);
        }

        [Test]
        public async Task TestPost()
        {
            var userToCreate = new UserCreateDto { Name = "some user", UserName = "some userName"};
            UserModel user = await Client.PostAsJsonAsync<UserCreateDto, UserModel>("api/users", userToCreate);

            user.UserName.Should().Be(userToCreate.UserName);
            user.Name.Should().Be(userToCreate.Name);
        }
    }
}