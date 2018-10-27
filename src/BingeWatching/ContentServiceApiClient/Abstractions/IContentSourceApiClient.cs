using BingeWatching.API.ContentServiceApiClient;
using BingeWatching.Domain.Entities;
using BingeWatching.Enums;
using System.Threading.Tasks;

namespace BingeWatching.API.Abstractions
{
    public interface IContentSourceApiClient
    {
        Task<ContentServiceApiResponse> GetRandomContentByType(ContentType contentType);
    }
}