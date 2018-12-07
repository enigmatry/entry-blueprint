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
        private User _user;

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
            List<UserModel> users = (await Client.GetAsync<IEnumerable<UserModel>>("users")).ToList();

            users.Count.Should().Be(3, "we have three users in the db, one added, one seeded and one created by current user provider");

            UserModel model = users.FirstOrDefault(u => u.UserName == "john_doe@john.doe");
            model.Should().NotBeNull();
            model.Name.Should().Be("John Doe");
            model.CreatedOn.Should().Be(_createdDate);
            model.UpdatedOn.Should().Be(_updatedOn);
        }

        [TestCase("some user", "someuser@test.com", TestName = "Create valid user")]
        public async Task TestCreate(string name, string userName)
        {
            var command = new UserCreateOrUpdateCommand {Name = name, UserName = userName};
            UserModel user =
                await Client.PostAsync<UserCreateOrUpdateCommand, UserModel>("users", command);

            user.UserName.Should().Be(command.UserName);
            user.Name.Should().Be(command.Name);
            user.CreatedOn.Date.Should().Be(DateTime.Now.Date);
            user.UpdatedOn.Date.Should().Be(DateTime.Now.Date);
        }

        [TestCase("some user", "someuser@test.com", TestName = "Create valid user")]
        public async Task TestUpdate(string name, string userName)
        {
            var command = new UserCreateOrUpdateCommand { Id = _user.Id, Name = name, UserName = userName};
            UserModel user =
                await Client.PostAsync<UserCreateOrUpdateCommand, UserModel>("users", command);

            user.UserName.Should().Be("john_doe@john.doe", "username is immutable");
            user.Name.Should().Be(command.Name);
            user.CreatedOn.Date.Should().Be(_user.CreatedOn.Date);
            user.UpdatedOn.Date.Should().Be(DateTime.Now.Date);
        }

        [TestCase("some user", "invalid email", "userName", "'User Name' is not a valid email address.", TestName =
            "Invalid username")]
        [TestCase("", "someuser@test.com", "name", "'Name' must not be empty.", TestName = "Missing name")]
        [TestCase("some user", "", "userName", "'User Name' must not be empty.", TestName = "Missing username")]
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