using AutoMapper;
using GringottsBankingApp.API.Dtos;
using GringottsBankingApp.Core.Models;
using GringottsBankingApp.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GringottsBankingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransfersController : ControllerBase
    {
        private readonly ITransferService _transferService;
        private readonly IMapper _mapper;

        public TransfersController(ITransferService transferService, IMapper mapper)
        {
            _transferService = transferService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public IActionResult GetAllById(int id)
        {
            var transferHistory = _transferService.GetAllById(id);

            return Ok(_mapper.Map<List<List<TransferDto>>>(transferHistory));
        }

        [HttpPost]
        public IActionResult TransferMoney(TransferDto transferDto)
        {
            _transferService.TransferMoney(_mapper.Map<Transfer>(transferDto));

            return NoContent();
        }
    }
}
