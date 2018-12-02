using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Enigmatry.Blueprint.Api.Models.Identity;
using Enigmatry.Blueprint.Core;
using Enigmatry.Blueprint.Core.Data;
using Enigmatry.Blueprint.Model.Identity;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Enigmatry.Blueprint.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        [HttpGet]
        public async Task<IEnumerable<UserModel>> Get()
        {
            List<User> users = await _userRepository.QueryAll().ToListAsync();
            return _mapper.Map<IEnumerable<UserModel>>(users);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<UserModel>> Get(Guid id)
        {
            User user = await _userRepository.QueryAll().ById(id).SingleOrDefaultAsync();
            return _mapper.MapToActionResult<UserModel>(user);
        }

        [HttpPost]
        // validation will be done by the mediatr pipeline so we can skip it
        public async Task<ActionResult<UserModel>> Post([CustomizeValidator(Skip=true)] UserCreateOrUpdateCommand command)
        {
            User user = await _mediator.Send(command);
            await _unitOfWork.SaveChangesAsync();
            return await Get(user.Id);
        }

        [HttpGet]
        [Route("secret")]
        public string GetSecret()
        {
            return _configuration.GetValue<string>("App:SampleKeyVaultSecret");
        }
    }
}