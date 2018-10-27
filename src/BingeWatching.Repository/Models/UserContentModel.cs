using BingeWatching.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BingeWatching.Repository.Models
{
    [BsonIgnoreExtraElements]
    public class UserContentModel
    {
        public int? UserRanking { get; set; }
        public string UserId { get; set; }
        public string ContentId { get; set; }
        public ContentWatchingStatus Status { get; set; }
    }
}
