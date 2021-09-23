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
                Amount = 23,
                ContactEmail = "frank.herbert@gmail.com",
                ContactPhone = "+253 56 334 4889",
                InfoLink = "https://en.wikipedia.org/wiki/Dune_(novel)",
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
                Amount = 100,
                ContactEmail = "info@salto.rs",
                ContactPhone = "+381 60 399 8871",
                InfoLink = "https://www.salto.rs/#belgrade-ipa",
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
                Amount = 1,
                ContactEmail = "info@lada.com",
                ContactPhone = "+381 21 661 6432",
                InfoLink = "https://en.wikipedia.org/wiki/Lada_Niva",
                ExpiresOn = null
            });
            product3.SetCreated(new DateTimeOffset(2021, 1, 1, 12, 0, 0, TimeSpan.FromHours(0)), User.TestUserId);
            product3.WithId(new Guid("04C45383-1372-4312-BB38-5EDFA569DB66"));

            var product4 = Product.Create(new ProductCreateOrUpdate.Command
            {
                Name = "Volkswagen Type 2",
                Code = "VWVW12345678",
                Type = ProductType.Car,
                Price = 8799.50,
                Amount = 3,
                ContactEmail = "vw_camper@vw.com",
                ContactPhone = "+381 32 332 7689",
                InfoLink = "https://en.wikipedia.org/wiki/Volkswagen_Type_2",
                ExpiresOn = null
            });
            product4.SetCreated(new DateTimeOffset(2021, 1, 1, 12, 0, 0, TimeSpan.FromHours(0)), User.TestUserId);
            product4.WithId(new Guid("DA8EB2A9-1F8A-4C41-8DF9-10A1FC59305B"));

            var product5 = Product.Create(new ProductCreateOrUpdate.Command
            {
                Name = "Burek",
                Code = "FOOD12345678",
                Type = ProductType.Food,
                Price = 2.50,
                Amount = 89,
                ContactEmail = "burek@burek.com",
                ContactPhone = "+381 11 113 6651",
                InfoLink = "",
                ExpiresOn = null
            });
            product5.SetCreated(new DateTimeOffset(2021, 1, 1, 12, 0, 0, TimeSpan.FromHours(0)), User.TestUserId);
            product5.WithId(new Guid("1DE0818E-04D7-4435-98AF-114B81AFF0D0"));

            var product6 = Product.Create(new ProductCreateOrUpdate.Command
            {
                Name = "Sardines",
                Code = "ZXAB14444678",
                Type = ProductType.Food,
                Price = 7.33,
                Amount = 13,
                ContactEmail = "sardines@ocean.com",
                ContactPhone = "+381 11 451 8709",
                InfoLink = "https://www.youtube.com/watch?v=WPpFjl8qeM4&ab_channel=DiscoveryUK",
                ExpiresOn = new DateTimeOffset(2050, 6, 15, 0, 0, 0, TimeSpan.FromHours(0))
            });
            product6.SetCreated(new DateTimeOffset(2021, 1, 1, 12, 0, 0, TimeSpan.FromHours(0)), User.TestUserId);
            product6.WithId(new Guid("A84132E7-CFC9-4766-8DA0-B1D9E549DE57"));

            modelBuilder.Entity<Product>().HasData(product1, product2, product3, product4, product5, product6);
        }
    }
}
