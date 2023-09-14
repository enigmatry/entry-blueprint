using System.Net.Mime;
using Enigmatry.Blueprint.Domain.Authorization;
using Enigmatry.Blueprint.Domain.Users;
using Enigmatry.Blueprint.Domain.Users.Commands;
using Enigmatry.Blueprint.Infrastructure.Authorization;
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

    public UsersController(
        IUnitOfWork unitOfWork,
        IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [UserHasPermission(PermissionId.UsersRead)]
    public async Task<ActionResult<PagedResponse<GetUsers.Response.Item>>> Search([FromQuery] GetUsers.Request query)
    {
        var response = await _mediator.Send(query);
        return response.ToActionResult();
    }

    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [UserHasPermission(PermissionId.UsersRead)]
    public async Task<ActionResult<GetUserDetails.Response>> Get(Guid id)
    {
        var response = await _mediator.Send(GetUserDetails.Request.ById(id));
        return response.ToActionResult();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [UserHasPermission(PermissionId.UsersWrite)]
    public async Task<ActionResult<GetUserDetails.Response>> Post(CreateOrUpdateUser.Command command)
    {
        User user = await _mediator.Send(command);
        await _unitOfWork.SaveChangesAsync();
        return await Get(user.Id);
    }

    [HttpGet]
    [Route("roles")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [UserHasPermission(PermissionId.UsersRead)]
    public async Task<ActionResult<IEnumerable<LookupResponse<Guid>>>> GetRolesLookup()
    {
        var response = await _mediator.Send(new GetRoleLookup.Request());
        return response.ToActionResult();
    }
}
