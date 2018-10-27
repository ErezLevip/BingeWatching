using BingeWatching.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BingeWatching.Contracts
{
    public interface IContent
    {
        Task<ActionResult<ContentViewModel>> Get(ContentRequest req);

    }
}
