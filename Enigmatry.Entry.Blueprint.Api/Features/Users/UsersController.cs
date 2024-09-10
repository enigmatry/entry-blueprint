using System.Net.Mime;
using Enigmatry.Entry.AspNetCore;
using Enigmatry.Entry.Blueprint.Domain.Authorization;
using Enigmatry.Entry.Blueprint.Domain.Users;
using Enigmatry.Entry.Blueprint.Domain.Users.Commands;
using Enigmatry.Entry.Blueprint.Infrastructure.Authorization;
using Enigmatry.Entry.Core.Data;
using Enigmatry.Entry.Core.Paging;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Enigmatry.Entry.Blueprint.Api.Features.Users;

[Produces(MediaTypeNames.Application.Json)]
[Route("api/[controller]")]
public class UsersController(
    IUnitOfWork unitOfWork,
    IMediator mediator) : Controller
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [UserHasPermission(PermissionId.UsersRead)]
    public async Task<ActionResult<PagedResponse<GetUsers.Response.Item>>> Search([FromQuery] GetUsers.Request query)
    {
        var response = await mediator.Send(query);
        return response.ToActionResult();
    }

    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [UserHasPermission(PermissionId.UsersRead)]
    public async Task<ActionResult<GetUserDetails.Response>> Get(Guid id)
    {
        var response = await mediator.Send(GetUserDetails.Request.ById(id));
        return response.ToActionResult();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [UserHasPermission(PermissionId.UsersWrite)]
    public async Task<ActionResult<GetUserDetails.Response>> Post(CreateOrUpdateUser.Command command)
    {
        var user = await mediator.Send(command);
        await unitOfWork.SaveChangesAsync();
        return await Get(user.Id);
    }

    [HttpGet]
    [Route("roles")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [UserHasPermission(PermissionId.UsersRead)]
    public async Task<ActionResult<IEnumerable<LookupResponse<Guid>>>> GetRolesLookup()
    {
        var response = await mediator.Send(new GetRoleLookup.Request());
        return response.ToActionResult();
    }

    [HttpGet]
    [Route("statuses")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [UserHasPermission(PermissionId.UsersRead)]
    public async Task<ActionResult<IEnumerable<LookupResponse<UserStatusId>>>> GetStatusesLookup()
    {
        var response = await mediator.Send(new GetStatusesLookup.Request());
        return response.ToActionResult();
    }
}
