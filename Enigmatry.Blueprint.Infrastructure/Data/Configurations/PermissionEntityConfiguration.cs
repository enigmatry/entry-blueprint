using Enigmatry.Blueprint.Domain.Authorization;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enigmatry.Blueprint.Infrastructure.Data.Configurations;

[UsedImplicitly]
public class PermissionEntityConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(Permission.NameMaxLength);

        builder.HasIndex(x => x.Name).IsUnique();
    }
}
