﻿using Enigmatry.Blueprint.Data.Migrations.Seeding;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Blueprint.Data.Migrations;

public static class DbInitializer
{
    private static readonly List<ISeeding> Seeding = new()
    {
        new RolePermissionSeeding()
    };

    // EF Core way of seeding data: https://docs.microsoft.com/en-us/ef/core/modeling/data-seeding
    public static void SeedData(ModelBuilder modelBuilder) => Seeding.ForEach(seeding => seeding.Seed(modelBuilder));
}
