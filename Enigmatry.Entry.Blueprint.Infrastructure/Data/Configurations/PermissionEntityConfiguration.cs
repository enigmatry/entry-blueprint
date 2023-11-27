using Enigmatry.Entry.Blueprint.Domain.Authorization;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Data.Configurations;

[UsedImplicitly]
public class PermissionEntityConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.Property(permission => permission.Name).IsRequired().HasMaxLength(Permission.NameMaxLength);
        builder.HasIndex(permission => permission.Name).IsUnique();
    }
}
