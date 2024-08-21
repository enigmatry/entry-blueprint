using Enigmatry.Entry.Blueprint.Domain.Users;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Data.Configurations;

[UsedImplicitly]
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.EmailAddress).IsRequired().HasMaxLength(User.EmailAddressMaxLength);
        builder.Property(u => u.FullName).IsRequired().HasMaxLength(User.NameMaxLength);
        builder.Property(u => u.RoleId).IsRequired();
        builder.Property(u => u.CreatedOn).IsRequired();

        builder.HasIndex(u => u.EmailAddress).IsUnique();
        
        builder.HasLookupTableRelation(x => x.UserStatus, x => x.UserStatusId);

        builder.HasOne(u => u.Role).WithMany(x => x.Users).OnDelete(DeleteBehavior.NoAction);
        builder.HasCreatedByAndUpdatedBy();
    }
}
