using AutoMapper;
using GringottsBankingApp.API.Controllers;
using GringottsBankingApp.API.Dtos;
using GringottsBankingApp.API.Mappings;
using GringottsBankingApp.Core.Models;
using GringottsBankingApp.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GringottsBankingApp.Test
{
    public class AccountControllerTest
    {
        private readonly Mock<IAccountService> _mock;
        private readonly AccountsController _controller;
        private static IMapper _mapper;
        private List<Account> accounts;

        public AccountControllerTest()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MapProfile());
                });

                IMapper mapper = mappingConfig.CreateMapper();

                _mapper = mapper;
            }

            _mock = new Mock<IAccountService>();

            _controller = new AccountsController(_mock.Object, _mapper);

            accounts = new List<Account>()
            {
                new Account{Id=1, Deposit=100, UserId=1},
                new Account{Id=2, Deposit=150, UserId=1},
                new Account{Id=3, Deposit=150, UserId=1}
            };
        }

        [Fact]
        public async void GetAll_ActionExecutes_ReturnOkResultWithAccounts()
        {
            _mock.Setup(x => x.GetAllAsync()).ReturnsAsync(accounts);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnAccount = Assert.IsAssignableFrom<IEnumerable<AccountDto>>(okResult.Value);

            Assert.Equal<int>(3, returnAccount.ToList().Count);
        }

        [Theory]
        [InlineData(1)]
        public async void GetById_IdValid_ReturnOkResult(int id)
        {
            var account = accounts.First(x => x.Id == id);

            _mock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(account);

            var result = await _controller.GetById(id);

            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnAccount = Assert.IsType<AccountDto>(okResult.Value);

            Assert.Equal(id, returnAccount.Id);

            Assert.Equal(account.Deposit, returnAccount.Deposit);
        }

        [Theory]
        [InlineData(1)]
        public async void GetWithUserById_IdValid_ReturnOkResult(int id)
        {
            var account = accounts.First(x => x.Id == id);

            _mock.Setup(x => x.GetWithUserByIdAsync(id)).ReturnsAsync(account);

            var result = await _controller.GetWithUserById(id);

            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnAccount = Assert.IsType<AccountsWithUserDto>(okResult.Value);

            Assert.Equal(id, returnAccount.Id);

            Assert.Equal(account.Deposit, returnAccount.Deposit);
        }

        [Theory]
        [InlineData(1)]
        public void Update_ActionExecutes_ReturnNoContent(int id)
        {
            var account = accounts.First(x => x.Id == id);

            _mock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(account);

            _mock.Setup(x => x.Update(account)).Returns(account);

            var result = _controller.Update(_mapper.Map<AccountDto>(account));

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void Save_ActionExecutes_ReturnCreated()
        {
            var account = accounts.First();

            _mock.Setup(x => x.AddAsync(account)).ReturnsAsync(account);

            var result = await _controller.Save(_mapper.Map<AccountDto>(account));

            Assert.IsType<CreatedResult>(result);
        }

        [Theory]
        [InlineData(1)]
        public void Remove_ActionExecutes_ReturnNoContent(int id)
        {
            var account = accounts.First(x => x.Id == id);

            _mock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(account);

            _mock.Setup(x => x.Remove(account));

            var noContentresult = _controller.Remove(id);

            _mock.Verify(x => x.Remove(account), Times.Once);

            Assert.IsType<NoContentResult>(noContentresult);
        }
    }
}
