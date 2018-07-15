using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Enigmatry.Blueprint.Api.Filters;
using Enigmatry.Blueprint.Api.Models.Identity;
using Enigmatry.Blueprint.Core.Data;
using Enigmatry.Blueprint.Model.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Enigmatry.Blueprint.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _log;
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IRepository<User> userRepository, IMapper mapper, ILogger<UsersController> log)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _log = log;
        }

        [HttpGet]
        [TransactionFilter]
        public async Task<IEnumerable<UserModel>> Get()
        {
            _log.LogError("Error example.");
            _log.LogWarning("Warning example.");
            _log.LogDebug("Debug example.");

            var position = new {Latitude = 25, Longitude = 134};
            const int elapsedMs = 34;

            // see https://github.com/serilog/serilog/wiki/Writing-Log-Events on best practice of writing log events.
            _log.LogInformation("Processed {@Position} in {Elapsed:000} ms.", position, elapsedMs);

            List<User> users = await _userRepository.QueryAll().ToListAsync();
            return _mapper.Map<IEnumerable<UserModel>>(users);
        }

        [HttpPost]
        public async Task<UserModel> Post([FromBody] UserCreateDto model)
        {
            User user = Model.Identity.User.Create(model);
            _userRepository.Add(user);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<UserModel>(user);
        }
    }
}