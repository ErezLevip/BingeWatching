using System;

namespace BingeWatching.Domain.Entities
{
    public class Content
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public DateTime ReleasedOn { get; set; }
        public decimal? ImdbRating { get; set; }
    }
}
