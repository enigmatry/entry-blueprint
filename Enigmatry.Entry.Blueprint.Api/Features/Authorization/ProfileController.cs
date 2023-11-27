using System.Net.Mime;
using Enigmatry.Entry.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Enigmatry.Entry.Blueprint.Api.Features.Authorization;

[Produces(MediaTypeNames.Application.Json)]
[Route("[controller]")]
public class ProfileController : Controller
{
    private readonly IMediator _mediator;

    public ProfileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetUserProfile.Response>> GetProfile()
    {
        var response = await _mediator.Send(new GetUserProfile.Request());
        return response.ToActionResult();
    }
}
