using System.Net.Mime;
using Enigmatry.Entry.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Enigmatry.Entry.Blueprint.Api.Features.Authorization;

[Produces(MediaTypeNames.Application.Json)]
[Route("api/[controller]")]
public class ProfileController(IMediator mediator) : Controller
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetUserProfile.Response>> GetProfile()
    {
        var response = await mediator.Send(new GetUserProfile.Request());
        return response.ToActionResult();
    }
}
