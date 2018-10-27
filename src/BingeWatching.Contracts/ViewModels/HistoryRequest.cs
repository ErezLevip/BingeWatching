using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace BingeWatching.Contracts.ViewModels
{
    public class HistoryRequest
    {
        [FromQuery]
        public string UserId { get; set; }
    }
}
