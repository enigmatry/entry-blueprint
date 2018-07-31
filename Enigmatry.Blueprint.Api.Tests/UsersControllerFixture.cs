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

        [SetUp]
        public void SetUp()
        {
            _createdDate = Resolve<ITimeProvider>().Now;
            User user = new UserBuilder()
                .UserName("john_doe@john.doe")
                .Name("John Doe")
                .CreatedOn(_createdDate)
                .UpdatedOn(_createdDate.AddYears(1));


            var userRepository = Resolve<IRepository<User>>();
            var unitOfWork = Resolve<IUnitOfWork>();
            userRepository.Add(user);
            unitOfWork.SaveChanges();
        }

        [Test]
        public async Task TestGetAll()
        {
            List<UserModel> users = (await JsonClient.GetAsync<IEnumerable<UserModel>>("api/users")).ToList();

            users.Count.Should().Be(2, "we have two users in the db");

            UserModel user = users.First();
            user.UserName.Should().Be("john_doe@john.doe");
            user.Name.Should().Be("John Doe");
            user.CreatedOn.Should().Be(_createdDate);
        }

        [TestCase("some user", "someuser@test.com", TestName = "Create valid user")]
        public async Task TestCreate(string name, string userName)
        {
            var userToCreate = new UserCreateOrUpdateCommand {Name = name, UserName = userName};
            UserModel user =
                await JsonClient.PostAsJsonAsync<UserCreateOrUpdateCommand, UserModel>("api/users", userToCreate);

            user.UserName.Should().Be(userToCreate.UserName);
            user.Name.Should().Be(userToCreate.Name);
            user.CreatedOn.Date.Should().Be(DateTime.Now.Date);
            user.CreatedOn.Date.Should().Be(DateTime.Now.Date);
            user.UpdatedOn.Date.Should().Be(DateTime.Now.Date);
        }

        [TestCase("some user", "invalid email", "userName", "'User Name' is not a valid email address.", TestName =
            "Invalid username")]
        [TestCase("", "someuser@test.com", "name", "'Name' should not be empty.", TestName = "Missing name")]
        [TestCase("some user", "", "userName", "'User Name' should not be empty.", TestName = "Missing username")]
        [TestCase("John Doe", "john_doe@john.doe", "userName", "unique", TestName =
            "Duplicate username")]
        public async Task TestCreateReturnsValidationErrors(string name, string userName, string validationField,
            string validationErrorMessage)
        {
            var userToCreate = new UserCreateOrUpdateCommand {Name = name, UserName = userName};
            HttpResponseMessage response = await Client.PostAsJsonAsync("api/users", userToCreate);

            response.Should().BeBadRequest().And.ContainValidationError(validationField, validationErrorMessage);
        }
    }
}