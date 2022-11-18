using System.Net.Mime;
using Enigmatry.Blueprint.Domain.Identity;
using Enigmatry.Blueprint.Domain.Identity.Commands;
using Enigmatry.Entry.AspNetCore;
using Enigmatry.Entry.Core.Data;
using Enigmatry.Entry.Core.Paging;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Enigmatry.Blueprint.Api.Features.Users;

[Produces(MediaTypeNames.Application.Json)]
[Route("[controller]")]
public class UsersController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;

    public UsersController(
        IUnitOfWork unitOfWork,
        IMediator mediator,
        IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
        _configuration = configuration;
    }

    /// <summary>
    ///     Gets listing of all available users
    /// </summary>
    /// <returns>List of users</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResponse<GetUsers.Response.Item>>> Search([FromQuery] GetUsers.Request query)
    {
        var response = await _mediator.Send(query);
        return response.ToActionResult();
    }

    /// <summary>
    ///     Get user for given id
    /// </summary>
    /// <param name="id">Id</param>
    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetUserDetails.Response>> Get(Guid id)
    {
        var response = await _mediator.Send(GetUserDetails.Request.ById(id));
        return response.ToActionResult();
    }

    /// <summary>
    ///  Creates or updates
    /// </summary>
    /// <param name="command">User data</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GetUserDetails.Response>> Post(CreateOrUpdateUser.Command command)
    {
        User user = await _mediator.Send(command);
        await _unitOfWork.SaveChangesAsync();
        return await Get(user.Id);
    }

    /// <summary>
    ///     Gets secret from Azure Key Vault
    /// </summary>
    [HttpGet]
    [Route("secret")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public string GetSecret() => _configuration.GetValue<string>("App:SampleKeyVaultSecret");
}
