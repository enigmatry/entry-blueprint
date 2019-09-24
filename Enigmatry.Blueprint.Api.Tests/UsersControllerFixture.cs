using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Enigmatry.Blueprint.Api.Models.Identity;
using Enigmatry.Blueprint.Api.Tests.Common;
using Enigmatry.Blueprint.Api.Tests.Infrastructure.Api;
using Enigmatry.Blueprint.Core.Data;
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
        private DateTimeOffset _updatedOn;
#pragma warning disable CS8618 // Created in Setup
        private User _user;
#pragma warning restore CS8618 //

        [SetUp]
        public void SetUp()
        {
            _createdDate = Resolve<ITimeProvider>().Now;
            _updatedOn = _createdDate;
            _user = new UserBuilder()
                .UserName("john_doe@john.doe")
                .Name("John Doe");

            var userRepository = Resolve<IRepository<User>>();
            userRepository.Add(_user);
            SaveChanges();
        }

        [Test]
        public async Task TestGetAll()
        {
            var users = (await Client.GetAsync<IEnumerable<UserModel>>("users")).ToList();

            users.Count.Should().Be(3, "we have three users in the db, one added, one seeded and one created by current user provider");

            UserModel model = users.Single(u => u.UserName == "john_doe@john.doe");
            model.Name.Should().Be("John Doe");
            model.CreatedOn.Should().Be(_createdDate);
            model.UpdatedOn.Should().Be(_updatedOn);
        }

        [Test]
        public async Task TestCreate()
        {
            var command = new UserCreateOrUpdateCommand {Name = "some user", UserName = "someuser@test.com"};
            UserModel user =
                await Client.PostAsync<UserCreateOrUpdateCommand, UserModel>("users", command);

            user.UserName.Should().Be(command.UserName);
            user.Name.Should().Be(command.Name);
            user.CreatedOn.Date.Should().Be(DateTime.Now.Date);
            user.UpdatedOn.Date.Should().Be(DateTime.Now.Date);
        }

        [Test]
        public async Task TestUpdate()
        {
            var command = new UserCreateOrUpdateCommand { Id = _user.Id, Name = "some user", UserName = "someuser@test.com"};
            UserModel user =
                await Client.PostAsync<UserCreateOrUpdateCommand, UserModel>("users", command);

            user.UserName.Should().Be("john_doe@john.doe", "username is immutable");
            user.Name.Should().Be(command.Name);
            user.CreatedOn.Date.Should().Be(_user.CreatedOn.Date);
            user.UpdatedOn.Date.Should().Be(DateTime.Now.Date);
        }

        [TestCase("some user", "invalid email", "userName", "'Username' is not a valid email address.", TestName =
            "Invalid username")]
        [TestCase("", "someuser@test.com", "name", "'Name' must not be empty.", TestName = "Missing name")]
        [TestCase("some user", "", "userName", "'Username' must not be empty.", TestName = "Missing username")]
        [TestCase("John Doe", "john_doe@john.doe", "userName", "Username already taken", TestName =
            "Duplicate username")]
        public async Task TestCreateReturnsValidationErrors(string name, string userName, string validationField,
            string validationErrorMessage)
        {
            var userToCreate = new UserCreateOrUpdateCommand {Name = name, UserName = userName};
            HttpResponseMessage response = await Client.PostAsJsonAsync("users", userToCreate);

            response.Should().BeBadRequest().And.ContainValidationError(validationField, validationErrorMessage);
        }
    }
}
