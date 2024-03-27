using Enigmatry.Entry.Blueprint.Domain.Products;
using Enigmatry.Entry.Blueprint.Domain.Products.Commands;
using Enigmatry.Entry.Blueprint.Infrastructure.Data;
using Enigmatry.Entry.Blueprint.Scheduler.Jobs;
using Enigmatry.Entry.Blueprint.Scheduler.Jobs.Requests;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Entry.Blueprint.Scheduler.Tests;

public class UpdateProductAmountJobFixtureBase : SchedulerFixtureBase
{

    [Test]
    public async Task TestUpdateProductAmountJob()
    {
        var dbContext = Resolve<AppDbContext>();
        var product = Product.Create(new ProductCreateOrUpdate.Command
        {
            Amount = 100,
            Code = "Test code",
            Description = "Description",
            Name = "Some name",
            Price = 100,
            Type = ProductType.Book
        });
        dbContext.Set<Product>().Add(product);
        await dbContext.SaveChangesAsync();
        dbContext.ChangeTracker.Clear();

        await ExecuteJob<UpdateProductAmountJob, EmptyJobRequest>(new EmptyJobRequest());

        var updatedProducts = await dbContext.Set<Product>().ToListAsync();

        await Verify(updatedProducts);
    }
}
