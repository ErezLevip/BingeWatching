using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BingeWatching.API;
using BingeWatching.API.Abstractions;
using BingeWatching.API.ContentServiceApiClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BingeWatching
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // add source api
            var contentSourceApiUri = Configuration.GetValue<string>("ContentSourceApi:SourceBaseUri");
            services.AddScoped<IContentSourceApiClient>((sp) => new ContentSourceApiClient(contentSourceApiUri));

            //mongo
            var connectionString = Configuration.GetValue<string>("MongoSettings:ConnectionString");
            var database = Configuration.GetValue<string>("MongoSettings:Database");
            // add repository layer
            services.AddUserMovieRepository(database, connectionString);

            // add domain layer
            services.AddBingeWatchingService();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
