using Enigmatry.Entry.Blueprint.Domain.Products;
using Enigmatry.Entry.Blueprint.Domain.Products.Commands;
using Enigmatry.Entry.Blueprint.Infrastructure.Data;
using Enigmatry.Entry.Blueprint.Scheduler.Jobs;
using Enigmatry.Entry.Blueprint.Scheduler.Jobs.Requests;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Entry.Blueprint.Scheduler.Tests;

public class UpdateProductAmountJobFixtureBase : SchedulerFixtureBase
{
    private AppDbContext _dbContext = null!;

    [SetUp]
    public void SetUp() => _dbContext = Resolve<AppDbContext>();

    [Test]
    public async Task TestUpdateProductAmountJob()
    {
        var product = Product.Create(new ProductCreateOrUpdate.Command
        {
            Amount = 100,
            Code = "Test code",
            Description = "Description",
            Name = "Some name",
            Price = 100,
            Type = ProductType.Book
        });
        _dbContext.Set<Product>().Add(product);
        await _dbContext.SaveChangesAsync();
        _dbContext.ChangeTracker.Clear();

        await ExecuteJob<UpdateProductAmountJob, EmptyJobRequest>(new EmptyJobRequest());

        var updatedProducts = await _dbContext.Set<Product>().ToListAsync();

        await Verify(updatedProducts);
    }
}
