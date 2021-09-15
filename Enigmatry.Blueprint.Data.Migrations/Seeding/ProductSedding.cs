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
            var product1 = Product.Create(new ProductCreateOrUpdate.Command
            {
                Name = "Dune I",
                Code = "XYZW12345678",
                Type = ProductType.Book,
                Price = 13.7,
                ContactEmail = "frank.herbert@gmail.com",
                ContactPhone = "+253 56 334 4889",
                ExpiresOn = null
            });
            product1.SetCreated(new DateTimeOffset(2021, 1, 1, 12, 0, 0, TimeSpan.FromHours(0)), User.TestUserId);
            product1.WithId(new Guid("7a4ce0d5-471b-4d35-89f2-b6c7a68350c0"));

            var product2 = Product.Create(new ProductCreateOrUpdate.Command
            {
                Name = "Salto IPA",
                Code = "ABCD12345678",
                Type = ProductType.Drink,
                Price = 2.6,
                ContactEmail = "info@salto.rs",
                ContactPhone = "+381 60 399 8871",
                ExpiresOn = new DateTimeOffset(2025, 6, 15, 0, 0, 0, TimeSpan.FromHours(0))
            });
            product2.SetCreated(new DateTimeOffset(2021, 1, 1, 12, 0, 0, TimeSpan.FromHours(0)), User.TestUserId);
            product2.WithId(new Guid("DC7047B0-0D35-4CBC-9424-D907AE5A25F4"));

            var product3 = Product.Create(new ProductCreateOrUpdate.Command
            {
                Name = "Lada Niva",
                Code = "XXXX12345678",
                Type = ProductType.Car,
                Price = 15335.0,
                ContactEmail = "info@lada.com",
                ContactPhone = "+381 21 661 6432",
                ExpiresOn = null
            });
            product3.SetCreated(new DateTimeOffset(2021, 1, 1, 12, 0, 0, TimeSpan.FromHours(0)), User.TestUserId);
            product3.WithId(new Guid("04C45383-1372-4312-BB38-5EDFA569DB66"));

            modelBuilder.Entity<Product>().HasData(product1, product2, product3);
        }
    }
}
