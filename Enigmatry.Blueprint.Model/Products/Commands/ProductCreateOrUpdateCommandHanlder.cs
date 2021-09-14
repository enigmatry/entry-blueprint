using System;
using System.Threading;
using System.Threading.Tasks;
using Enigmatry.BuildingBlocks.Core.Data;
using JetBrains.Annotations;
using MediatR;

namespace Enigmatry.Blueprint.Model.Products.Commands
{
    [UsedImplicitly]
    public class ProductCreateOrUpdateCommandHanlder : IRequestHandler<ProductCreateOrUpdate.Command, ProductCreateOrUpdate.Result>
    {
        private readonly IRepository<Product, Guid> _productRepository;

        public ProductCreateOrUpdateCommandHanlder(IRepository<Product, Guid> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductCreateOrUpdate.Result> Handle(ProductCreateOrUpdate.Command request, CancellationToken cancellationToken)
        {
            Product result;
            if (request.Id.HasValue)
            {
                result = await _productRepository.FindByIdAsync(request.Id.Value)
                    ?? throw new InvalidOperationException("Could not find product by Id");
                result.Update(request);
            }
            else
            {
                result = Product.Create(request);
                _productRepository.Add(result);
            }

            return new ProductCreateOrUpdate.Result { Id = result.Id };
        }
    }
}
