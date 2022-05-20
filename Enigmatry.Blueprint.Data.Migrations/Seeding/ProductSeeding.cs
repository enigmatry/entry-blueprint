using Enigmatry.Blueprint.Domain.Identity;
using Enigmatry.Blueprint.Domain.Products;
using Enigmatry.Blueprint.Domain.Products.Commands;
using Enigmatry.BuildingBlocks.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Blueprint.Data.Migrations.Seeding;

public class ProductSeeding : ISeeding
{
    public void Seed(ModelBuilder modelBuilder)
    {
        var dune = Product.Create(new ProductCreateOrUpdate.Command
        {
            Name = "Dune I",
            Code = "XYZW12345678",
            Type = ProductType.Book,
            Price = 13.7,
            Amount = 23,
            ContactEmail = "frank.herbert@gmail.com",
            ContactPhone = "+253 (056) 334 4889",
            InfoLink = "https://en.wikipedia.org/wiki/Dune_(novel)",
            ExpiresOn = null,
            FreeShipping = true,
            HasDiscount = true,
            Discount = 25.0F
        });
        dune.SetCreated(new DateTimeOffset(2021, 1, 1, 12, 0, 0, TimeSpan.FromHours(0)), User.TestUserId);
        dune.WithId(new Guid("7a4ce0d5-471b-4d35-89f2-b6c7a68350c0"));

        var salto = Product.Create(new ProductCreateOrUpdate.Command
        {
            Name = "Salto IPA",
            Code = "ABCD12345678",
            Type = ProductType.Drink,
            Price = 2.6,
            Amount = 100,
            ContactEmail = "info@salto.rs",
            ContactPhone = "+381 (060) 399 8871",
            InfoLink = "https://www.salto.rs/#belgrade-ipa",
            ExpiresOn = new DateTimeOffset(2025, 6, 15, 0, 0, 0, TimeSpan.FromHours(0)),
            FreeShipping = false
        });
        salto.SetCreated(new DateTimeOffset(2021, 1, 1, 12, 0, 0, TimeSpan.FromHours(0)), User.TestUserId);
        salto.WithId(new Guid("DC7047B0-0D35-4CBC-9424-D907AE5A25F4"));

        var lada = Product.Create(new ProductCreateOrUpdate.Command
        {
            Name = "Lada Niva",
            Code = "XXXX12345678",
            Type = ProductType.Car,
            Price = 15335.0,
            Amount = 1,
            ContactEmail = "info@lada.com",
            ContactPhone = "+381 (021) 661 6432",
            InfoLink = "https://en.wikipedia.org/wiki/Lada_Niva",
            ExpiresOn = null,
            FreeShipping = true
        });
        lada.SetCreated(new DateTimeOffset(2021, 1, 1, 12, 0, 0, TimeSpan.FromHours(0)), User.TestUserId);
        lada.WithId(new Guid("04C45383-1372-4312-BB38-5EDFA569DB66"));

        var camper = Product.Create(new ProductCreateOrUpdate.Command
        {
            Name = "Volkswagen Type 2",
            Code = "VWVW12345678",
            Type = ProductType.Car,
            Price = 8799.50,
            Amount = 3,
            ContactEmail = "vw_camper@vw.com",
            ContactPhone = "+381 (032) 332 7689",
            InfoLink = "https://en.wikipedia.org/wiki/Volkswagen_Type_2",
            ExpiresOn = null,
            FreeShipping = true,
            HasDiscount = true,
            Discount = 10.0F
        });
        camper.SetCreated(new DateTimeOffset(2021, 1, 1, 12, 0, 0, TimeSpan.FromHours(0)), User.TestUserId);
        camper.WithId(new Guid("DA8EB2A9-1F8A-4C41-8DF9-10A1FC59305B"));

        var burek = Product.Create(new ProductCreateOrUpdate.Command
        {
            Name = "Burek",
            Code = "FOOD12345678",
            Type = ProductType.Food,
            Price = 2.50,
            Amount = 89,
            ContactEmail = "burek@burek.com",
            ContactPhone = "+381 (011) 113 6651",
            InfoLink = "",
            ExpiresOn = null,
            FreeShipping = false
        });
        burek.SetCreated(new DateTimeOffset(2021, 1, 1, 12, 0, 0, TimeSpan.FromHours(0)), User.TestUserId);
        burek.WithId(new Guid("1DE0818E-04D7-4435-98AF-114B81AFF0D0"));

        var sardines = Product.Create(new ProductCreateOrUpdate.Command
        {
            Name = "Sardines",
            Code = "ZXAB14444678",
            Type = ProductType.Food,
            Price = 7.33,
            Amount = 13,
            ContactEmail = "sardines@ocean.com",
            ContactPhone = "+381 (011) 451-8709",
            InfoLink = "https://www.youtube.com/watch?v=WPpFjl8qeM4&ab_channel=DiscoveryUK",
            ExpiresOn = new DateTimeOffset(2050, 6, 15, 0, 0, 0, TimeSpan.FromHours(0)),
            FreeShipping = false
        });
        sardines.SetCreated(new DateTimeOffset(2021, 1, 1, 12, 0, 0, TimeSpan.FromHours(0)), User.TestUserId);
        sardines.WithId(new Guid("A84132E7-CFC9-4766-8DA0-B1D9E549DE57"));

        var mockingbird = Product.Create(new ProductCreateOrUpdate.Command
        {
            Name = "To Kill a Mockingbird",
            Code = "TKMB33774422",
            Type = ProductType.Book,
            Price = 9.09,
            Amount = 22,
            ContactEmail = "harper.lee@book.com",
            ContactPhone = "+253 (056) 331-1178",
            InfoLink = "https://en.wikipedia.org/wiki/To_Kill_a_Mockingbird",
            ExpiresOn = null,
            FreeShipping = false
        });
        mockingbird.SetCreated(new DateTimeOffset(2021, 1, 1, 12, 0, 0, TimeSpan.FromHours(0)), User.TestUserId);
        mockingbird.WithId(new Guid("65FB31FA-FF40-4BD5-8FE6-755122B6428B"));

        var kibbeling = Product.Create(new ProductCreateOrUpdate.Command
        {
            Name = "Kibbeling",
            Code = "KIBL12344321",
            Type = ProductType.Food,
            Price = 4.50,
            Amount = 68,
            ContactEmail = "kibbeling@nl.fast.food.com",
            ContactPhone = "+31 (098) 777 3379",
            InfoLink = "https://en.wikipedia.org/wiki/Kibbeling",
            ExpiresOn = null,
            FreeShipping = false
        });
        kibbeling.SetCreated(new DateTimeOffset(2021, 1, 1, 12, 0, 0, TimeSpan.FromHours(0)), User.TestUserId);
        kibbeling.WithId(new Guid("767C974C-8C07-43F5-8C6F-0CC1EEF3C65A"));

        var kapsalon = Product.Create(new ProductCreateOrUpdate.Command
        {
            Name = "Kapsalon",
            Code = "KAPS11223344",
            Type = ProductType.Food,
            Price = 5.50,
            Amount = 54,
            ContactEmail = "kapsalon@nl.fast.food.com",
            ContactPhone = "+31 (098) 221 3489",
            InfoLink = "https://en.wikipedia.org/wiki/Kapsalon",
            ExpiresOn = null,
            FreeShipping = false
        });
        kapsalon.SetCreated(new DateTimeOffset(2021, 1, 1, 12, 0, 0, TimeSpan.FromHours(0)), User.TestUserId);
        kapsalon.WithId(new Guid("67A75734-D04A-4661-95D9-24879122C901"));

        modelBuilder.Entity<Product>().HasData(dune, salto, lada, camper, burek, sardines, mockingbird, kibbeling, kapsalon);
    }
}
