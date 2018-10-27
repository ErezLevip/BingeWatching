using BingeWatching.API.ViewModels;
using BingeWatching.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BingeWatching.API.Client
{
    public class RecommendationApiClient : IRecommendation
    {
        private readonly string _baseUrl;
        public RecommendationApiClient(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public async Task Post(RecommendationRequest req)
        {
            string url = $"{_baseUrl}/Recommendation/";
            using (HttpClient httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
                await httpClient.PostAsync(url, content);
            }
        }
    }
}
