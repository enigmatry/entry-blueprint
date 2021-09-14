using Enigmatry.Blueprint.Model.Identity;
using Enigmatry.Blueprint.Model.Products;
using Enigmatry.Blueprint.Model.Products.Commands;
using Enigmatry.BuildingBlocks.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Enigmatry.Blueprint.Data.Migrations.Seeding
{
    public class ProductSedding : ISeeding
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            var product = Product.Create(new ProductCreateOrUpdate.Command
            {
                Name = "Dune",
                Code = "XYZW12345678",
                Type = ProductType.Book,
                Price = 13.7,
                ContactEmail = "frank.herbert@gmail.com",
                ContactPhone = "+1253 3344-889",
                ExpiresOn = null
            });
            product.SetCreated(new DateTimeOffset(2021, 1, 1, 12, 0, 0, TimeSpan.FromHours(0)), User.TestUserId);
            product.WithId(new Guid("7a4ce0d5-471b-4d35-89f2-b6c7a68350c0"));
            modelBuilder.Entity<Product>().HasData(product);
        }
    }
}
