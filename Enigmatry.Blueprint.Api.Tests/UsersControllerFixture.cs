using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Enigmatry.Blueprint.Api.Features.Users;
using Enigmatry.Blueprint.Api.Tests.Infrastructure.Api;
using Enigmatry.Blueprint.Model;
using Enigmatry.Blueprint.Model.Identity;
using Enigmatry.Blueprint.Model.Tests.Identity;
using FluentAssertions;
using Enigmatry.Blueprint.Api.Tests.Common;
using Enigmatry.Blueprint.Model.Identity.Commands;
using NUnit.Framework;

namespace Enigmatry.Blueprint.Api.Tests
{
    [Category("integration")]
    public class UsersControllerFixture : IntegrationFixtureBase
    {
        private DateTimeOffset _createdOn;
        private DateTimeOffset _updatedOn;
        private User _user = null!;

        [SetUp]
        public void SetUp()
        {
            _createdOn = Resolve<ITimeProvider>().Now;
            _updatedOn = _createdOn;
            _user = new UserBuilder()
                .WithUserName("john_doe@john.doe")
                .WithName("John Doe");

            AddAndSaveChanges(_user);
        }

        [Test]
        public async Task TestGetAll()
        {
            var users = (await Client.GetAsync<GetUsers.Response>("users")).Items.ToList();

            users.Count.Should().Be(3, "we have three users in the db, one added, one seeded and one created by current user provider");

            GetUsers.Response.Item item = users.Single(u => u.UserName == "john_doe@john.doe");
            item.Name.Should().Be("John Doe");
            item.CreatedOn.Should().Be(_createdOn);
            item.UpdatedOn.Should().Be(_updatedOn);
        }

        [Test]
        public async Task GivenValidUserId_GetById_ReturnsUserDetails()
        {
            var user = await Client.GetAsync<GetUserDetails.Response>($"users/{_user.Id}");

            user.Should().NotBeNull();

            user.Name.Should().Be("John Doe");
            user.UserName.Should().Be("john_doe@john.doe");
            user.CreatedOn.Should().Be(_createdOn);
            user.UpdatedOn.Should().Be(_updatedOn);
        }

        [Test]
        public async Task GivenNonExistingUserId_GetById_ReturnsNotFound()
        {
            var response = await Client.GetAsync($"users/{Guid.NewGuid()}");

            response.Should().BeNotFound();
        }

        [Test]
        public async Task TestCreate()
        {
            var command = new UserCreateOrUpdate.Command {Name = "some user", UserName = "someuser@test.com"};
            GetUserDetails.Response user =
                await Client.PostAsync<UserCreateOrUpdate.Command, GetUserDetails.Response>("users", command);

            user.UserName.Should().Be(command.UserName);
            user.Name.Should().Be(command.Name);
            user.CreatedOn.Date.Should().Be(DateTime.Now.Date);
            user.UpdatedOn.Date.Should().Be(DateTime.Now.Date);
        }

        [Test]
        public async Task TestUpdate()
        {
            var command = new UserCreateOrUpdate.Command {Id = _user.Id, Name = "some user", UserName = "someuser@test.com"};
            GetUserDetails.Response user =
                await Client.PostAsync<UserCreateOrUpdate.Command, GetUserDetails.Response>("users", command);

            user.UserName.Should().Be("john_doe@john.doe", "username is immutable");
            user.Name.Should().Be(command.Name);
            user.CreatedOn.Date.Should().Be(_user.CreatedOn.Date);
            user.UpdatedOn.Date.Should().Be(DateTime.Now.Date);
        }

        [TestCase("some user", "invalid email", "userName", "is not a valid email address.")]
        [TestCase("", "someuser@test.com", "name", "must not be empty.")]
        [TestCase("some user", "", "userName", "must not be empty.")]
        [TestCase("John Doe", "john_doe@john.doe", "userName", "Username already taken")]
        public async Task TestCreateReturnsValidationErrors(string name,
            string userName,
            string validationField,
            string validationErrorMessage)
        {
            var command = new UserCreateOrUpdate.Command {Name = name, UserName = userName};
            HttpResponseMessage response = await Client.PostAsJsonAsync("users", command);

            response.Should().BeBadRequest().And.ContainValidationError(validationField, validationErrorMessage);
        }
    }
}
