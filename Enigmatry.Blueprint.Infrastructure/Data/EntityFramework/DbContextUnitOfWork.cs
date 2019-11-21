using System.Threading;
using System.Threading.Tasks;
using Enigmatry.Blueprint.Core.Data;
using Enigmatry.Blueprint.Model.Identity;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Enigmatry.Blueprint.Infrastructure.Data.EntityFramework
{
    [UsedImplicitly]
    public class DbContextUnitOfWork : IUnitOfWork
    {
        private readonly BlueprintContext _context;
        private readonly ILogger<DbContextUnitOfWork> _logger;
        private bool _cancelSaving;

        public DbContextUnitOfWork(BlueprintContext context, ILogger<DbContextUnitOfWork> logger, ICurrentUserProvider currentUserProvider)
        {
            _context = context;
            _logger = logger;
        }

        public int SaveChanges()
        {
            Task<int> task = Task.Run(async () => await SaveChangesAsync());
            return task.Result;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (_cancelSaving)
            {
                _logger.LogWarning("Not saving database changes since saving was cancelled.");
                return 0;
            }

            int numberOfChanges = await _context.SaveChangesAsync(cancellationToken);
            _logger.LogDebug(
                $"{numberOfChanges} of changed were saved to database {_context.Database.GetDbConnection().Database}");
            return numberOfChanges;
        }

        public void CancelSaving()
        {
            _cancelSaving = true;
        }
    }
}
