using BingeWatching.Domain.Entities;
using BingeWatching.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bingeWatching.Domain.Abstractions
{
    public interface IBingeWatchingService
    {
        Task UpdateContentWatchingStatus(string userId, string contentId, ContentWatchingStatus watchingStatus);
        Task<IEnumerable<UserHistory>> GetUserWatchingHistory(string userId);
        Task UpdateContentRecomendation(string contentId, string userId, int score);
        Task<bool> RegisterAndVerifyNewContent(Content content, string userId);
    }
}