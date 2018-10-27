using BingeWatching.API.Client;
using BingeWatching.API.Client.Abstractions;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Container
    {
        public static IServiceProvider Create()
        {
            string baseUrl = "https://localhost:5001/api";
            return new ServiceCollection()
              .AddScoped<IBingeWatchingApiClient>((sp) => new BingeWatchingApiClient(baseUrl))
            .BuildServiceProvider();
        }
    }
}
