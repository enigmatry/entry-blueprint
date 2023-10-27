using Enigmatry.Blueprint.Domain;
using Enigmatry.Blueprint.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enigmatry.Blueprint.Infrastructure.Data.Configurations;

public static class EntityTypeBuilderExtensions
{
    public static void HasCreatedByAndUpdatedBy<T>(this EntityTypeBuilder<T> builder) where T : EntityWithCreatedUpdated
    {
        builder.HasOne<User>().WithMany().HasForeignKey(e => e.CreatedById).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne<User>().WithMany().HasForeignKey(e => e.UpdatedById).OnDelete(DeleteBehavior.NoAction);
    }
}
