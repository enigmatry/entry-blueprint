﻿using Enigmatry.Entry.Blueprint.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(Product.NameMaxLength);
        builder.Property(x => x.Code).IsRequired().HasMaxLength(Product.CodeMaxLength);
        builder.Property(x => x.Price).IsRequired();
        builder.Property(x => x.ContactEmail).IsRequired().HasMaxLength(Product.ContactEmailMaxLength);
        builder.Property(x => x.ContactPhone).IsRequired().HasMaxLength(Product.ContactPhoneMaxLength);
        builder.Property(x => x.Description).HasMaxLength(Product.DescriptionMaxLength);
        builder.Property(x => x.Status).HasSentinel(ProductStatus.Active).HasDefaultValue(ProductStatus.Active);

        builder.HasCreatedByAndUpdatedBy();

        builder.HasIndex(x => x.Code).IsUnique();
    }
}
