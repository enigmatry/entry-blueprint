using Enigmatry.Entry.Blueprint.Domain.Products;
using Enigmatry.Entry.Blueprint.Scheduler.Jobs.Requests;
using Enigmatry.Entry.Core;
using Enigmatry.Entry.Core.Data;
using Enigmatry.Entry.Scheduler;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Entry.Blueprint.Scheduler.Jobs;

[UsedImplicitly]
public class CleanOldProductsJob : EntryJob<CleanOldProductsJobRequest>
{
    private readonly IRepository<Product> _repository;
    private readonly ITimeProvider _timeProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CleanOldProductsJob> _logger;

    public CleanOldProductsJob(ILogger<CleanOldProductsJob> logger,
        IConfiguration configuration,
        IUnitOfWork unitOfWork,
        IRepository<Product> repository,
        ITimeProvider timeProvider) : base(logger, configuration)
    {
        _logger = logger;
        _repository = repository;
        _timeProvider = timeProvider;
        _unitOfWork = unitOfWork;
    }

    public override async Task Execute(CleanOldProductsJobRequest request)
    {
        var createdBeforeDateTime = _timeProvider.FixedUtcNow - request.DeactivateOlderThan;
        _logger.LogInformation("Will deactivate products created before: {CreatedBeforeDate}", createdBeforeDateTime);
        var products = await _repository.QueryAll().QueryInStatus(ProductStatus.Active)
            .QueryCreatedBefore(createdBeforeDateTime)
            .ToListAsync();
        _logger.LogInformation("Found {ProductCount} products to deactivate", products.Count);
        foreach (var product in products)
        {
            _logger.LogInformation("Deactivating product {ProductId} with name {ProductName}", product.Id, product.Name);
            product.UpdateStatus(ProductStatus.Inactive);
        }

        await _unitOfWork.SaveChangesAsync();
    }
}
