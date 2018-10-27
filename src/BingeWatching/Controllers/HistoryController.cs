using AutoMapper;
using bingeWatching.Domain.Abstractions;
using BingeWatching.API.Abstractions;
using BingeWatching.API.Mappers;
using BingeWatching.Contracts;
using BingeWatching.Contracts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BingeWatching.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase, IHistory
    {
        private readonly IBingeWatchingService _bingeWatchService;
        private readonly IMapper _mapper;
        public HistoryController(IBingeWatchingService bingeWatchService)
        {
            _bingeWatchService = bingeWatchService;
            _mapper = BingeWatchingApiMapper.Instance;
        }

        [HttpGet]
        public async Task<ActionResult<HistoryResponse>> Get([FromQuery]HistoryRequest req)
        {
            var history = await _bingeWatchService.GetUserWatchingHistory(req.UserId);
            return Ok(new HistoryResponse
            {
                UserId = req.UserId,
                Content = _mapper.Map<List<HistoryViewModel>>(history)
            });
        }
    }
}
