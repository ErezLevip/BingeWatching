using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BingeWatching.API.ContentServiceApiClient
{
    public class ContentServiceApiResponse
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public DateTime Released_On { get; set; }
        public decimal? Imdb_Rating { get; set; }
    }
}
