using bingeWatching.Domain.Abstractions;
using BingeWatching.Domain.Entities;
using BingeWatching.Enums;
using BingeWatching.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bingeWatching.Domain
{
    public class BingeWatchingService : IBingeWatchingService
    {
        private readonly IUserContentRepository _userContentRepository;
        public BingeWatchingService(IUserContentRepository userContentRepository)
        {
            _userContentRepository = userContentRepository;
        }

        public async Task<IEnumerable<UserHistory>> GetUserWatchingHistory(string userId)
        {
            return await _userContentRepository.GetUserWatchingHistory(userId);
        }

        public async Task<bool> RegisterAndVerifyNewContent(Content content, string userId)
        {
            return await _userContentRepository.RegisterAndVerifyNewContent(content, userId);
        }

        public async Task UpdateContentRecomendation(string contentId, string userId, int score)
        {
            await _userContentRepository.UpdateContentRecomendation(contentId, userId, score);
        }

        public async Task UpdateContentWatchingStatus(string userId, string contentId, ContentWatchingStatus watchingStatus)
        {
            await _userContentRepository.UpdateContentWatchingStatus(userId, contentId, watchingStatus);
        }
    }
}
