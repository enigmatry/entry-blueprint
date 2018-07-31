using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Enigmatry.Blueprint.Api.Models.Identity;
using Enigmatry.Blueprint.Core;
using Enigmatry.Blueprint.Core.Data;
using Enigmatry.Blueprint.Model.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Blueprint.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly IMediator _mediator;

        public UsersController(IRepository<User> userRepository, IMapper mapper,
            IUnitOfWork unitOfWork, IMediator mediator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
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
            User user = await _userRepository.QueryAll().ById(id).SingleAsync();
            return _mapper.MapToActionResult<UserModel>(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> Post(UserCreateOrUpdateCommand command)
        {
            User user = await _mediator.Send(command);
            await _unitOfWork.SaveChangesAsync();
            return await Get(user.Id);
        }
    }
}