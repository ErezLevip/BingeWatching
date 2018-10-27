using BingeWatching.Repository;
using BingeWatching.Repository.Abstractions;
using BingeWatching.Repository.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class BingeWatchingRepositoryDI
    {
        public static IServiceCollection AddUserMovieRepository(this IServiceCollection services, string database, string connectionString)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(database);
            return services.AddSingleton(db.GetCollection<ContentModel>("Content"))
                           .AddSingleton(db.GetCollection<UserContentModel>("UserContent"))
                           .AddSingleton<IUserContentRepository, UserContentRepository>();
        }
    }
}
