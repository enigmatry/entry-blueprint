using System.Net.Mime;
using Enigmatry.Entry.AspNetCore;
using Enigmatry.Entry.Blueprint.Domain.Authorization;
using Enigmatry.Entry.Blueprint.Domain.Products.Commands;
using Enigmatry.Entry.Blueprint.Infrastructure.Authorization;
using Enigmatry.Entry.Core.Paging;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Enigmatry.Entry.Blueprint.Api.Features.Products;

[Produces(MediaTypeNames.Application.Json)]
[Route("api/[controller]")]
public class ProductsController(IMediator mediator) : Controller
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [UserHasPermission(PermissionId.ProductsRead)]
    public async Task<ActionResult<PagedResponse<GetProducts.Response.Item>>> Search([FromQuery] GetProducts.Request query)
    {
        var response = await mediator.Send(query);
        return response.ToActionResult();
    }

    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [UserHasPermission(PermissionId.ProductsRead)]
    public async Task<ActionResult<GetProductDetails.Response>> Get(Guid id)
    {
        var response = await mediator.Send(new GetProductDetails.Request { Id = id });
        return response.ToActionResult();
    }

    [HttpGet]
    [Route("code-unique")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [UserHasPermission(PermissionId.ProductsRead)]
    public async Task<ActionResult<IsProductCodeUnique.Response>> IsCodeUnique([FromQuery] IsProductCodeUnique.Request request)
    {
        var response = await mediator.Send(request);
        return response.ToActionResult();
    }

    [HttpGet]
    [Route("name-unique")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [UserHasPermission(PermissionId.ProductsRead)]
    public async Task<ActionResult<IsProductNameUnique.Response>> IsNameUnique([FromQuery] IsProductNameUnique.Request request)
    {
        var response = await mediator.Send(request);
        return response.ToActionResult();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [UserHasPermission(PermissionId.ProductsWrite)]
    public async Task<ActionResult<ProductCreateOrUpdate.Result>> Post(ProductCreateOrUpdate.Command command)
    {
        var result = await mediator.Send(command);
        return result;
    }

    [HttpDelete]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [UserHasPermission(PermissionId.ProductsDelete)]
    public async Task Remove(Guid id) => await mediator.Send(new RemoveProduct.Command { Id = id });
}
