using AutoMapper;
using GringottsBankingApp.API.Dtos;
using GringottsBankingApp.API.Filters;
using GringottsBankingApp.Core.Models;
using GringottsBankingApp.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GringottsBankingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
        }

        [ServiceFilter(typeof(UserNotFoundFilter))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);

            return Ok(_mapper.Map<UserDto>(user));
        }

        [ServiceFilter(typeof(UserNotFoundFilter))]
        [HttpGet("{id}/accounts")]
        public async Task<IActionResult> GetWithAccountsById(int id)
        {
            var user = await _userService.GetWithAccountsByIdAsync(id);

            return Ok(_mapper.Map<UserWithAccountsDto>(user));
        }

        [HttpPost]
        public async Task<IActionResult> Save(UserDto userDto)
        {
            var newUser = await _userService.AddAsync(_mapper.Map<User>(userDto));

            return Created(string.Empty, _mapper.Map<UserDto>(newUser));
        }

        [HttpPut]
        public IActionResult Update(UserDto userDto)
        {
            var user = _userService.Update(_mapper.Map<User>(userDto));

            return NoContent();
        }

        [ServiceFilter(typeof(UserNotFoundFilter))]
        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            var user = _userService.GetByIdAsync(id).Result;

            _userService.Remove(user);

            return NoContent();
        }
    }
}
