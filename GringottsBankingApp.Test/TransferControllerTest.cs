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
    public class TransferControllerTest
    {
        private readonly Mock<ITransferService> _mock;
        private readonly TransfersController _controller;
        private static IMapper _mapper;
        private List<Transfer> transfers;

        public TransferControllerTest()
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

            _mock = new Mock<ITransferService>();

            _controller = new TransfersController(_mock.Object, _mapper);

            transfers = new List<Transfer>()
            {
                new Transfer{Id=1, SenderAccountId=14, ReceiverAccountId=9, TransferAmount=200},
                new Transfer{Id=2, SenderAccountId=7, ReceiverAccountId=12, TransferAmount=20},
                new Transfer{Id=3, SenderAccountId=9, ReceiverAccountId=7, TransferAmount=30},
            };
        }

        [Theory]
        [InlineData(1)]
        public void GetAllById_IdValid_ReturnOkResult(int id)
        {
            var transfer = transfers.First(x => x.Id == id);

            _mock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(transfer);

            var result = _controller.GetAllById(id);

            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.IsType<List<List<TransferDto>>>(okResult.Value);
        }

        [Theory]
        [InlineData(1)]
        public void TransferMoney_ActionExecutes_ReturnNoContent(int id)
        {
            var transfer = transfers.First(x => x.Id == id);

            _mock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(transfer);

            _mock.Setup(x => x.TransferMoney(transfer));

            var result = _controller.TransferMoney(_mapper.Map<TransferDto>(transfer));

            Assert.IsType<NoContentResult>(result);
        }
    }
}
