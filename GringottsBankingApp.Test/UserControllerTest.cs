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
    public class UserControllerTest
    {
        private readonly Mock<IUserService> _mock;
        private readonly UsersController _controller;
        private static IMapper _mapper;
        private List<User> users;

        public UserControllerTest()
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

            _mock = new Mock<IUserService>();

            _controller = new UsersController(_mock.Object, _mapper);

            users = new List<User>()
            {
                new User{Id=1, Name= "Arisha Barron"},
                new User{Id=2, Name="Branden Gibson"},
                new User{Id=3, Name="Rhonda Church"}
            };
        }

        [Fact]
        public async void GetAll_ActionExecutes_ReturnOkResultWithUsers()
        {
            _mock.Setup(x => x.GetAllAsync()).ReturnsAsync(users);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnUser = Assert.IsAssignableFrom<IEnumerable<UserDto>>(okResult.Value);

            Assert.Equal<int>(3, returnUser.ToList().Count);
        }

        [Theory]
        [InlineData(1)]
        public async void GetById_IdValid_ReturnOkResult(int id)
        {
            var user = users.First(x => x.Id == id);

            _mock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(user);

            var result = await _controller.GetById(id);

            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnUser = Assert.IsType<UserDto>(okResult.Value);

            Assert.Equal(id, returnUser.Id);

            Assert.Equal(user.Name, returnUser.Name);
        }

        [Theory]
        [InlineData(1)]
        public async void GetWithAccountsById_IdValid_ReturnOkResult(int id)
        {
            var user = users.First(x => x.Id == id);

            _mock.Setup(x => x.GetWithAccountsByIdAsync(id)).ReturnsAsync(user);

            var result = await _controller.GetWithAccountsById(id);

            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnUser = Assert.IsType<UserWithAccountsDto>(okResult.Value);

            Assert.Equal(id, returnUser.Id);

            Assert.Equal(user.Name, returnUser.Name);
        }

        [Theory]
        [InlineData(1)]
        public void Update_ActionExecutes_ReturnNoContent(int id)
        {
            var user = users.First(x => x.Id == id);

            _mock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(user);

            _mock.Setup(x => x.Update(user)).Returns(user);

            var result = _controller.Update(_mapper.Map<UserDto>(user));

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void Save_ActionExecutes_ReturnCreated()
        {
            var user = users.First();

            _mock.Setup(x => x.AddAsync(user)).ReturnsAsync(user);

            var result = await _controller.Save(_mapper.Map<UserDto>(user));

            Assert.IsType<CreatedResult>(result);
        }

        [Theory]
        [InlineData(1)]
        public void Remove_ActionExecutes_ReturnNoContent(int id)
        {
            var user = users.First(x => x.Id == id);

            _mock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(user);

            _mock.Setup(x => x.Remove(user));

            var noContentresult = _controller.Remove(id);

            _mock.Verify(x => x.Remove(user), Times.Once);

            Assert.IsType<NoContentResult>(noContentresult);
        }
    }
}
