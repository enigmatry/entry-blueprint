using Enigmatry.Blueprint.Infrastructure.Data.EntityFramework;

namespace Enigmatry.Blueprint.Data.Migrations.Seeding
{
    internal interface ISeeding
    {
        void Seed(BlueprintContext context);
    }
}