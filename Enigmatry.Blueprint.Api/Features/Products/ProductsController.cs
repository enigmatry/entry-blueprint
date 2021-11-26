using Enigmatry.Blueprint.Model.Products.Commands;
using Enigmatry.BuildingBlocks.AspNetCore;
using Enigmatry.BuildingBlocks.Core.Data;
using Enigmatry.BuildingBlocks.Core.Paging;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Enigmatry.Blueprint.Api.Features.Products
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public ProductsController(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        /// <summary>
        ///     Gets listing of all available users
        /// </summary>
        /// <returns>List of products</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponse<GetProducts.Response.Item>>> Search([FromQuery] GetProducts.Request query)
        {
            var response = await _mediator.Send(query);
            return response.ToActionResult();
        }

        /// <summary>
        ///     Get product for given id
        /// </summary>
        /// <param name="id">Id</param>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetProductDetails.Response>> Get(Guid id)
        {
            var response = await _mediator.Send(new GetProductDetails.Request { Id = id });
            return response.ToActionResult();
        }

        /// <summary>
        ///     Return true if product code is unique
        /// </summary>
        /// <param name="request">Request</param>
        [HttpGet]
        [Route("code-unique")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IsProductCodeUnique.Response>> IsCodeUnique([FromQuery] IsProductCodeUnique.Request request)
        {
            var response = await _mediator.Send(request);
            return response.ToActionResult();
        }

        /// <summary>
        ///     Return true if product name is unique
        /// </summary>
        /// <param name="request">Request</param>
        [HttpGet]
        [Route("name-unique")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IsProductNameUnique.Response>> IsNameUnique([FromQuery] IsProductNameUnique.Request request)
        {
            var response = await _mediator.Send(request);
            return response.ToActionResult();
        }

        /// <summary>
        ///  Creates or updates
        /// </summary>
        /// <param name="command">Product data</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductCreateOrUpdate.Result>> Post(ProductCreateOrUpdate.Command command)
        {
            var result = await _mediator.Send(command);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }

        /// <summary>
        ///     Removes product for given id
        /// </summary>
        /// <param name="id">Id</param>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task Remove(Guid id)
        {
            await _mediator.Send(new RemoveProduct.Command { Id = id });
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
