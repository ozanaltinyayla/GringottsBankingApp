using AutoMapper;
using GringottsBankingApp.API.Dtos;
using GringottsBankingApp.API.Filters;
using GringottsBankingApp.Core.Models;
using GringottsBankingApp.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GringottsBankingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountsController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var accounts = await _accountService.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<AccountDto>>(accounts));
        }

        [ServiceFilter(typeof(AccountNotFoundFilter))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var account = await _accountService.GetByIdAsync(id);

            return Ok(_mapper.Map<AccountDto>(account));
        }

        [ServiceFilter(typeof(AccountNotFoundFilter))]
        [HttpGet("{id}/user")]
        public async Task<IActionResult> GetWithUserById(int id)
        {
            var account = await _accountService.GetWithUserByIdAsync(id);

            return Ok(_mapper.Map<AccountsWithUserDto>(account));
        }

        [HttpPost]
        public async Task<IActionResult> Save(AccountDto accountDto)
        {
            var newAccount = await _accountService.AddAsync(_mapper.Map<Account>(accountDto));

            return Created(string.Empty, _mapper.Map<AccountDto>(newAccount));
        }

        [HttpPut]
        public IActionResult Update(AccountDto accountDto)
        {
            var account = _accountService.Update(_mapper.Map<Account>(accountDto));

            return NoContent();
        }

        [ServiceFilter(typeof(AccountNotFoundFilter))]
        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            var account = _accountService.GetByIdAsync(id).Result;

            _accountService.Remove(account);

            return NoContent();
        }
    }
}
