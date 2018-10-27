using BingeWatching.Contracts;
using BingeWatching.Contracts.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BingeWatching.API.Client
{
    public class HistoryApiClient : IHistory
    {
        private readonly string _baseUrl;
        public HistoryApiClient(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public async Task<HistoryResponse> Get(HistoryRequest req)
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
    }
}
