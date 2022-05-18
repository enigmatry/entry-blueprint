﻿using Enigmatry.Blueprint.Api.Tests.Infrastructure.Api;
using Enigmatry.Blueprint.Domain.Identity;
using Enigmatry.BuildingBlocks.Core.Data;
using Enigmatry.Blueprint.Model.Tests.Identity;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SequentialGuidGenerator = Enigmatry.BuildingBlocks.Core.Entities.SequentialGuidGenerator;

namespace Enigmatry.Blueprint.Api.Tests;

[Category("integration")]
public class SequentialGuidValueGeneratorFixture : IntegrationFixtureBase
{
    [SetUp]
    public void SetUp() =>

        // we only want users that we created in the test
        DoNotSeedUsers();

    [Test]
    public void TestGeneratedGuidValuesAreInSequentialOrderAfterReadFromDb()
    {
        const int start = 0;
        const int count = 20;

        //prepare
        var users = Enumerable.Range(start, count).Select(i => new UserBuilder()
            .WithUserName(i.ToString())
            .WithId(SequentialGuidGenerator.Generate())
            .Build()).ToArray();

        AddAndSaveChanges(users);

        var userRepository = Resolve<IRepository<User>>();

        //act
        var usersFromDb = userRepository.QueryAll().AsNoTracking().ToList();

        //assert
        for (var i = start; i < count; i++)
        {
            //order is preserved
            usersFromDb[i].UserName.Should().Be(i.ToString());
        }
    }
}
