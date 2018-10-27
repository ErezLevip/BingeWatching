using BingeWatching.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BingeWatching.Repository.Models
{
    public class ContentModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public DateTime ReleasedOn { get; set; }
        public decimal? ImdbRating { get; set; }
    }
}
