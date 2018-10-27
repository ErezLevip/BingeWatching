using System;
using System.Collections.Generic;
using System.Text;

namespace BingeWatching.Contracts.ViewModels
{
    public class HistoryResponse
    {
        public string UserId { get; set; }
        public List<HistoryViewModel> Content { get; set; }
    }
}
