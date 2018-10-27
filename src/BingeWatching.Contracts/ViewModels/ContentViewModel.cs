using BingeWatching.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BingeWatching.API.ViewModels
{
    public class ContentViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public DateTime ReleasedOn { get; set; }
        public decimal? ImdbRating { get; set; }
    }
}
