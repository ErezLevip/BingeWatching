using bingeWatching.Domain.Abstractions;
using BingeWatching.API.Abstractions;
using BingeWatching.API.Validators;
using BingeWatching.API.ViewModels;
using BingeWatching.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BingeWatching.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationController : ControllerBase, IRecommendation
    {
        private readonly IBingeWatchingService _bingeWatchService;
        public RecommendationController(IBingeWatchingService bingeWatchService)
        {
            _bingeWatchService = bingeWatchService;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]RecommendationRequest req)
        {
            var isValid = RecommendationValidator.IsValid(req);
            if (isValid.Item1)
            {
                await _bingeWatchService.UpdateContentWatchingStatus(req.UserId, req.ContentId, Enums.ContentWatchingStatus.Finished);
                await _bingeWatchService.UpdateContentRecomendation(req.ContentId, req.UserId, req.Score);
                return Ok();
            }

            return BadRequest(isValid.Item2);
        }
    }
}
