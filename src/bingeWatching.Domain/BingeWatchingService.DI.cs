using bingeWatching.Domain;
using bingeWatching.Domain.Abstractions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class BingeWatchingServiceDI
    {
        public static IServiceCollection AddBingeWatchingService(this IServiceCollection services)
        {
            return services.AddScoped<IBingeWatchingService, BingeWatchingService>();
        }
    }
}
