using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Entry.Blueprint.Data.Migrations.Seeding;

internal interface ISeeding
{
    void Seed(ModelBuilder modelBuilder);
}
