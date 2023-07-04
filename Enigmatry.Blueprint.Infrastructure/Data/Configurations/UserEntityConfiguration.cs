using Enigmatry.Blueprint.Domain.Identity;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enigmatry.Blueprint.Infrastructure.Data.Configurations;

[UsedImplicitly]
public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.UserName).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
        builder.Property(x => x.RoleId).IsRequired();
        builder.Property(x => x.CreatedOn).IsRequired();

        builder.HasIndex(x => x.UserName).IsUnique();

        builder.HasOne(u => u.CreatedBy).WithMany().OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(u => u.UpdatedBy).WithMany().OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Role).WithMany(x => x.Users!).OnDelete(DeleteBehavior.NoAction);
    }
}
