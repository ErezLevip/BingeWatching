using BingeWatching.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace BingeWatching.API.Client.Abstractions
{
    public interface IBingeWatchingApiClient : IHistory, IContent, IRecommendation
    {
    }
}
