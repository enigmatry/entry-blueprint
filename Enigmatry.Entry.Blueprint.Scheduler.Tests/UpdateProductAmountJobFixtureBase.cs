using Enigmatry.Entry.Blueprint.Domain.Products;
using Enigmatry.Entry.Blueprint.Domain.Products.Commands;
using Enigmatry.Entry.Blueprint.Scheduler.Jobs;
using Enigmatry.Entry.Blueprint.Scheduler.Jobs.Requests;
using Enigmatry.Entry.Core.Data;
using Enigmatry.Entry.Core.Entities;

namespace Enigmatry.Entry.Blueprint.Scheduler.Tests;

public class UpdateProductAmountJobFixtureBase : SchedulerFixtureBase
{
    private Product _aProduct = null!;
    private IRepository<Product> _repository = null!;

    [SetUp]
    public void Setup()
    {
        _repository = Resolve<IRepository<Product>>();
        _aProduct = Product.Create(new ProductCreateOrUpdate.Command
        {
            Amount = 100,
            Code = "Test code",
            Description = "Description",
            Name = "Some name",
            Price = 100,
            Type = ProductType.Book
        });
        AddAndSaveChanges(_aProduct);
    }
    
    [Test]
    public async Task TestUpdateProductAmountJob()
    {
        var aRequest = new UpdateProductAmountJobRequest { ProductId = _aProduct.Id, Amount = 10};
        
        await ExecuteJob<UpdateProductAmountJob, UpdateProductAmountJobRequest>(aRequest);
        
        var anUpdatedProduct = _repository.QueryAllSkipCache().QueryById(_aProduct.Id).First();
        
        await Verify(anUpdatedProduct);
    }
}
