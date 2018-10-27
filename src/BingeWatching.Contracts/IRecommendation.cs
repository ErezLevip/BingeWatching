using BingeWatching.API.ViewModels;
using BingeWatching.Contracts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BingeWatching.Contracts
{
    public interface IRecommendation
    {
        Task<ActionResult> Post(RecommendationRequest req);
    }
}
