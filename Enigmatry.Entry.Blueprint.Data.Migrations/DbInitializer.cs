using Enigmatry.Entry.Blueprint.Data.Migrations.Seeding;
using Enigmatry.Entry.Core.EntityFramework.Seeding;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Entry.Blueprint.Data.Migrations;

public static class DbInitializer
{
    private static readonly List<ISeeding> Seeding =
    [
        new UserStatusSeeding(),
        new RolePermissionSeeding(),
        new UserSeeding()
    ];

    // EF Core way of seeding data: https://docs.microsoft.com/en-us/ef/core/modeling/data-seeding
    public static void SeedData(ModelBuilder modelBuilder) => Seeding.ForEach(seeding => seeding.Seed(modelBuilder));
}
