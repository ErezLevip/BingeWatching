using AutoMapper;
using BingeWatching.Domain.Entities;
using BingeWatching.Enums;
using BingeWatching.Repository.Abstractions;
using BingeWatching.Repository.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BingeWatching.Repository
{
    public class UserContentRepository : IUserContentRepository
    {
        private readonly IMongoCollection<ContentModel> _contentCollection;
        private readonly IMongoCollection<UserContentModel> _userContentCollection;
        private readonly IMapper _mapper;
        public UserContentRepository(IMongoCollection<UserContentModel> userContentCollection,
                                     IMongoCollection<ContentModel> contentCollection)
        {
            _userContentCollection = userContentCollection;
            _contentCollection = contentCollection;
            _mapper = RepositoryMapper.Instance;
        }

        public async Task<IEnumerable<UserHistory>> GetUserWatchingHistory(string userId)
        {
            var userContent = await (await _userContentCollection.FindAsync(f => f.UserId == userId && f.Status == Enums.ContentWatchingStatus.Finished)).ToListAsync();
            var contentIds = userContent.Select(m => m.ContentId);
            var contents = await (await _contentCollection.FindAsync(m => contentIds.Contains(m.Id))).ToListAsync();
            return contents.Select(m =>
            {
                var uc = userContent.First(um => um.ContentId == m.Id);
                return new UserHistory
                {
                    UserRanking = uc.UserRanking,
                    Content = _mapper.Map<Content>(m)
                };
            });
        }

        public async Task<bool> RegisterAndVerifyNewContent(Content content, string userId)
        {
            await UpsertContent(content);
            var updateResult = await UpsertUserContent(content, userId);

            return updateResult.MatchedCount == 0;
        }

        public async Task UpdateContentWatchingStatus(string userId, string contentId, ContentWatchingStatus watchingStatus)
        {
            await _userContentCollection.UpdateOneAsync(uc => uc.UserId == userId && uc.ContentId == contentId, Builders<UserContentModel>.Update.Set(c => c.Status, ContentWatchingStatus.Finished));
        }

        private async Task UpsertContent(Content content)
        {
            await _contentCollection.UpdateOneAsync(Builders<ContentModel>
                .Filter
                .Where(r => r.Id == content.Id),
                    Builders<ContentModel>.Update
                                          .SetOnInsert(c => c.Id, content.Id)
                                          .SetOnInsert(c => c.Title, content.Title)
                                          .SetOnInsert(c => c.ReleasedOn, content.ReleasedOn)
                                          .SetOnInsert(c => c.ImdbRating, content.ImdbRating),
                new UpdateOptions
                {
                    IsUpsert = true,
                });
        }

        private async Task<UpdateResult> UpsertUserContent(Content content, string userId)
        {
            return await _userContentCollection.UpdateOneAsync(Builders<UserContentModel>
                               .Filter
                               .Where(r => r.ContentId == content.Id && r.UserId == userId),
                                   Builders<UserContentModel>.Update
                                                             .SetOnInsert(c => c.UserId, userId)
                                                             .SetOnInsert(c => c.ContentId, content.Id)
                                                             .SetOnInsert(c => c.Status, ContentWatchingStatus.Watching),
                               new UpdateOptions
                               {
                                   IsUpsert = true,
                               });
        }

        public async Task UpdateContentRecomendation(string contentId, string userId, int score)
        {
            await _userContentCollection.UpdateOneAsync(uc => uc.UserId == userId && uc.ContentId == contentId, Builders<UserContentModel>.Update.Set(c => c.UserRanking, score));
        }
    }
}
