using Enigmatry.Entry.Core.Data;
using Enigmatry.Entry.EntityFramework.MediatR;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Data;

public class AppSaveChangesBehavior<TRequest, TResponse>(
    AppDbContext dbContext,
    IUnitOfWork unitOfWork,
    ILogger<SaveChangesBehavior<AppDbContext, TRequest, TResponse>> logger)
    : SaveChangesBehavior<AppDbContext, TRequest, TResponse>(dbContext, unitOfWork, logger)
    where TRequest : IBaseRequest;
