using Enigmatry.Blueprint.Model.Identity;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enigmatry.Blueprint.Infrastructure.Data.Configurations
{
    [UsedImplicitly]
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.CreatedOn).IsRequired();

            builder.HasIndex(x => x.UserName).IsUnique();

#pragma warning disable CS8603 // Possible null reference return. This is configuration class
            builder.HasMany(x => x.CreatedUsers).WithOne(x => x.CreatedBy);
            builder.HasMany(x => x.UpdatedUsers).WithOne(x => x.UpdatedBy);
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
