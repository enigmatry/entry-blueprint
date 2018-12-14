using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Enigmatry.Blueprint.Api.Models.Identity;
using Enigmatry.Blueprint.Core;
using Enigmatry.Blueprint.Core.Data;
using Enigmatry.Blueprint.Model.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Enigmatry.Blueprint.Api.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public UsersController(IRepository<User> userRepository, IMapper mapper,
            IUnitOfWork unitOfWork, IMediator mediator, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _configuration = configuration;
        }

        /// <summary>
        ///     Gets listing of all available users
        /// </summary>
        /// <returns>List of users</returns>
        /// <response code="200">Returns found users
        /// </response>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IEnumerable<UserModel>> Get()
        {
            List<User> users = await _userRepository.QueryAll().ToListAsync();
            return _mapper.Map<IEnumerable<UserModel>>(users);
        }

        /// <summary>
        ///     Get user for given id
        /// </summary>
        /// <param name="id">User Id</param>
        /// <response code="200">Returns the user</response>
        /// <response code="400">If the user is not found</response>     
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserModel>> Get(Guid id)
        {
            User user = await GetById(id);
            return _mapper.MapToActionResult<UserModel>(user);
        }

        /// <summary>
        ///  Creates or updates
        /// </summary>
        /// <param name="command">User data</param>
        /// <response code="200">Returns the user</response>
        /// <response code="201">Returns created user</response>
        /// <response code="400">If the user is not found</response>     
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        // validation will be done by the MediatR pipeline so we can skip it
        public async Task<ActionResult<UserModel>> Post(UserCreateOrUpdateCommand command)
        {
            User user = await _mediator.Send(command);
            await _unitOfWork.SaveChangesAsync();

            var mappedUser = _mapper.Map<UserModel>(user);
            if (command.IsCreate)
            {
                return CreatedAtAction(nameof(Get), new {id = user.Id}, mappedUser);
            }

            return await Get(user.Id);
        }

        [HttpGet]
        [Route("file")]
        [ProducesResponseType(200)]
        public FileContentResult GetFile()
        {
            byte[] fileBytes = CreateInMemoryFile();
            return File(fileBytes, "application/txt", "download_file.txt");
        }

        private static byte[] CreateInMemoryFile()
        {
            using (var ms = new MemoryStream())
            {
                TextWriter tw = new StreamWriter(ms);
                tw.WriteLine("some line");
                tw.WriteLine("another line");
                tw.Flush();
                ms.Position = 0;
                return ms.ToArray();
            }
        }

        /// <summary>
        ///     Gets secret from Azure Key Vault
        /// </summary>
        [HttpGet]
        [Route("secret")]
        public string GetSecret()
        {
            return _configuration.GetValue<string>("App:SampleKeyVaultSecret");
        }

        private async Task<User> GetById(Guid id)
        {
            User user = await _userRepository.QueryAll()
                .ById(id)
                .SingleOrDefaultAsync();

            return user;
        }
    }
}