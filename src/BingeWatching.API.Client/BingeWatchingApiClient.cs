using BingeWatching.API.Client.Abstractions;
using BingeWatching.API.ViewModels;
using BingeWatching.Contracts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BingeWatching.API.Client
{
    public class BingeWatchingApiClient : IBingeWatchingApiClient
    {
        private readonly string _baseUrl;
        public BingeWatchingApiClient(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public async Task<ActionResult<ContentViewModel>> Get(ContentRequest req)
        {
            var url = $"{_baseUrl}/Content";
            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[nameof(req.UserId)] = req.UserId;
            query[nameof(req.ContentType)] = req.ContentType.ToString();
            uriBuilder.Query = query.ToString();

            using (HttpClient httpClient = new HttpClient())
            {
                var res = await httpClient.GetStringAsync(uriBuilder.ToString());
                if (res == null)
                    return null;

                return JsonConvert.DeserializeObject<ContentViewModel>(res);
            }
        }

        public async Task<ActionResult<HistoryResponse>> Get(HistoryRequest req)
        {
            var url = $"{_baseUrl}/History";
            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[nameof(req.UserId)] = req.UserId;
            uriBuilder.Query = query.ToString();

            using (HttpClient httpClient = new HttpClient())
            {
                var res = await httpClient.GetStringAsync(uriBuilder.ToString());
                if (res == null)
                    return null;

                return JsonConvert.DeserializeObject<HistoryResponse>(res);
            }
        }

        public async Task<ActionResult> Post(RecommendationRequest req)
        {
            string url = $"{_baseUrl}/Recommendation/";
            using (HttpClient httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
                var res = await httpClient.PostAsync(url, content);
                if(!res.IsSuccessStatusCode)
                {
                    var response = await res.Content.ReadAsStringAsync();
                    return new BadRequestObjectResult(response);
                }
            }
            return new OkResult();
        }
    }
}
