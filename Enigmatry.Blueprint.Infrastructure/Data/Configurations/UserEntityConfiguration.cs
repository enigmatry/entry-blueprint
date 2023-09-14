using Enigmatry.Blueprint.Domain.Users;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enigmatry.Blueprint.Infrastructure.Data.Configurations;

[UsedImplicitly]
public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.EmailAddress).IsRequired().HasMaxLength(User.EmailAddressMaxLength);
        builder.Property(u => u.FullName).IsRequired().HasMaxLength(User.NameMaxLength);
        builder.Property(u => u.RoleId).IsRequired();
        builder.Property(u => u.CreatedOn).IsRequired();

        builder.HasIndex(u => u.EmailAddress).IsUnique();

        builder.HasOne(u => u.CreatedBy).WithMany().OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(u => u.UpdatedBy).WithMany().OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(u => u.Role).WithMany(x => x.Users).OnDelete(DeleteBehavior.NoAction);
    }
}
