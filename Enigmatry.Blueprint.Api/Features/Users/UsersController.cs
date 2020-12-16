using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Enigmatry.Blueprint.Core.Data;
using Enigmatry.Blueprint.Model.Identity;
using Enigmatry.Blueprint.Model.Identity.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Enigmatry.Blueprint.Api.Features.Users
{
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
        public async Task<ActionResult<GetUsers.Response>> Search([FromQuery] GetUsers.Request query)
        {
            var response = await _mediator.Send(query);
            return response.ToActionResult();
        }

        /// <summary>
        ///     Get user for given id
        /// </summary>
        /// <param name="id">Id</param>
        [HttpGet]
        [Route("{id}")]
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
        public async Task<ActionResult<GetUserDetails.Response>> Post(UserCreateOrUpdate.Command command)
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
}
