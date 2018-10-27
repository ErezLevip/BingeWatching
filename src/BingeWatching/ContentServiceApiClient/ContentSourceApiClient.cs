using BingeWatching.API.Abstractions;
using BingeWatching.API.ContentServiceApiClient;
using BingeWatching.Domain.Entities;
using BingeWatching.Enums;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace BingeWatching.API.ContentServiceApiClient
{
    public class ContentSourceApiClient : IContentSourceApiClient
    {
        private const string CONTENT_KIND = "content_kind";
        private readonly string _sourceUri;
        public ContentSourceApiClient(string sourceUri)
        {
            _sourceUri = sourceUri;
        }

        public async Task<ContentServiceApiResponse> GetRandomContentByType(ContentType contentType)
        {
            using(HttpClient client = new HttpClient())
            {
                var uriBuilder = new UriBuilder(_sourceUri);
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query[CONTENT_KIND] = contentType.ToString().ToLower();
                uriBuilder.Query = query.ToString();
                var result = await client.GetStringAsync(uriBuilder.ToString());
                return JsonConvert.DeserializeObject<ContentServiceApiResponse>(result);
            }
        }
    }
}
