using BingeWatching.Enums;

namespace BingeWatching.Domain.Entities
{
    public class UserContent
    {
        public string UserId { get; set; }
        public Content Content { get; set; }
        public ContentWatchingStatus Status { get; set; }
    }
}
