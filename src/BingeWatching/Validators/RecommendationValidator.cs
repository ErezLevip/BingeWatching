using BingeWatching.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BingeWatching.API.Validators
{
    public static class RecommendationValidator
    {
        public static (bool,string) IsValid(RecommendationRequest req)
        {
            if (req.Score > 0 && req.Score <= 10)
                return (true,null);

            return (false, "Score must be between 0 and 10");
        }
    }
}
