using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Blueprint.Data.Migrations.Seeding;

internal interface ISeeding
{
    void Seed(ModelBuilder modelBuilder);
}
