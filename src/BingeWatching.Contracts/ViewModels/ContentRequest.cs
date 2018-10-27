using BingeWatching.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BingeWatching.API.ViewModels
{
    public class ContentRequest
    {
        [FromQuery]
        public string UserId { get; set; }
        [FromQuery]
        public ContentType ContentType { get; set; } = ContentType.Both;
    }
}
