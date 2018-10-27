using BingeWatching.Domain.Entities;
using BingeWatching.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BingeWatching.Repository.Abstractions
{
    public interface IUserContentRepository
    {
        Task<IEnumerable<UserHistory>> GetUserWatchingHistory(string userId);
        Task<bool> RegisterAndVerifyNewContent(Content content, string userId);
        Task UpdateContentWatchingStatus(string userId, string contentId,ContentWatchingStatus watchingStatus);
        Task UpdateContentRecomendation(string contentId, string userId, int score);
    }
}