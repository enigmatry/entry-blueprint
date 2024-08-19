using Argon;
using Enigmatry.Entry.Blueprint.Domain.Products;
using Enigmatry.Entry.Blueprint.Domain.Products.Commands;
using Enigmatry.Entry.Blueprint.Scheduler.Jobs;
using Enigmatry.Entry.Blueprint.Scheduler.Jobs.CleanOldProducts;
using Enigmatry.Entry.Core.Data;
using Enigmatry.Entry.Core.Entities;

namespace Enigmatry.Entry.Blueprint.Scheduler.Tests;

[Category("integration")]
public class CleanOldProductsJobFixture : SchedulerFixtureBase
{
    private IRepository<Product> _repository = null!;
    private readonly TimeSpan _deactivateOlderThan = TimeSpan.FromDays(30);
    private Product _aProductThatWillNotBeDeactivated = null!;
    private Product _aProductThatWillBeDeactivated = null!;
    private readonly DateTimeOffset _now = new(2021, 12, 12, 11, 10, 9, TimeSpan.Zero);

    [SetUp]
    public void Setup()
    {
        _repository = Resolve<IRepository<Product>>();
        _aProductThatWillBeDeactivated = AProductExpectedToDeactivate();
        _aProductThatWillNotBeDeactivated = AProductExpectedNotToDeactivate();
        SetFixedUtcNow(_now);
    }

    [Test]
    public async Task TestCleanOldProductsJob()
    {
        var aRequest = new CleanOldProductsJobRequest { DeactivateOlderThan = _deactivateOlderThan };

        await ExecuteJob<CleanOldProductsJob, CleanOldProductsJobRequest>(aRequest);

        var updatedProducts = _repository.QueryAllSkipCache()
            .QueryByIds([_aProductThatWillBeDeactivated.Id, _aProductThatWillNotBeDeactivated.Id]);

        // we want to explicitly see the values in this test
        await Verify(updatedProducts)
            .DontScrubGuids()
            .DontScrubDateTimes()
            .AddExtraSettings(x => x.DefaultValueHandling = DefaultValueHandling.Include);
    }

    private Product AProductExpectedToDeactivate()
    {
        var thresholdDate = _now.Subtract(TimeSpan.FromSeconds(1)).Subtract(_deactivateOlderThan);
        return AProductCreatedOn(new Guid("E3EA417E-C785-480B-9618-B8D3BF6B86A1"), "Code", "AnInactivatedProduct", thresholdDate);
    }

    private Product AProductExpectedNotToDeactivate()
    {
        var thresholdDate = _now.Subtract(_deactivateOlderThan);
        return AProductCreatedOn(new Guid("F735304A-968C-44DB-9FE2-7A02A5E62AF9"), "Code2", "AnActiveProduct", thresholdDate);
    }

    private Product AProductCreatedOn(Guid id, string code, string name, DateTimeOffset createdDate)
    {
        // Set the time provider to a fixed date so that CreatedOn is set
        SetFixedUtcNow(createdDate);

        var product = Product.Create(new ProductCreateOrUpdate.Command { Code = code, Description = "ADescription", Name = name }).WithId(id);

        // when calling save changes CreatedOn is populated 
        AddAndSaveChanges(product);
        return product;
    }
}
