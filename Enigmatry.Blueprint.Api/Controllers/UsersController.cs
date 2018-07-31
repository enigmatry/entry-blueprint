using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Enigmatry.Blueprint.Api.Models.Identity;
using Enigmatry.Blueprint.Core.Data;
using Enigmatry.Blueprint.Model.Identity;
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

        public UsersController(IRepository<User> userRepository, IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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
            User user = await _userRepository.FindByIdAsync(id);
            return _mapper.MapToActionResult<UserModel>(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> Post(UserCreateOrUpdateCommand model)
        {
            User user = Model.Identity.User.Create(model);
            _userRepository.Add(user);
            await _unitOfWork.SaveChangesAsync();

            return await Get(user.Id);
        }

        [HttpPut]
        public async Task<ActionResult<UserModel>> Put(UserCreateOrUpdateCommand model)
        {
            if (!model.Id.HasValue)
            {
                return BadRequest();
            }

            User user = _userRepository.FindById(model.Id.Value);
            if (user == null)
            {
                return NotFound();
            }

            user.Update(model);
            await _unitOfWork.SaveChangesAsync();
            return await Get(model.Id.Value);
        }
    }
}