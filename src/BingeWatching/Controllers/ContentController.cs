using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BingeWatching.Domain;
using bingeWatching.Domain.Abstractions;
using BingeWatching.API.ViewModels;
using BingeWatching.API.Abstractions;
using BingeWatching.Domain.Entities;
using BingeWatching.API.ContentServiceApiClient;
using AutoMapper;
using BingeWatching.API.Mappers;
using BingeWatching.Enums;
using BingeWatching.Contracts;

namespace BingeWatching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase, IContent
    {
        private readonly IBingeWatchingService _bingeWatchService;
        private readonly IContentSourceApiClient _contentClient;
        private readonly IMapper _mapper;
        public ContentController(IBingeWatchingService bingeWatchService, IContentSourceApiClient contentClient)
        {
            _bingeWatchService = bingeWatchService;
            _contentClient = contentClient;
            _mapper = ContentServiceApiMapper.Instance;
        }

        [HttpGet]
        public async Task<ActionResult<ContentViewModel>> Get([FromQuery]ContentRequest req)
        {
            var resultContent = new List<ContentViewModel>();

            ContentServiceApiResponse content = null;
            while (true)
            {
                content = await _contentClient.GetRandomContentByType(req.ContentType);
                if (await _bingeWatchService.RegisterAndVerifyNewContent(_mapper.Map<Content>(content), req.UserId))
                    break;
            }

            var contentViewModel = _mapper.Map<ContentViewModel>(content);

            return Ok(contentViewModel);
        }
    }
}
