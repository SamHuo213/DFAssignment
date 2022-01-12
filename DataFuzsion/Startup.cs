using DataFuzionAPI;
using DataFuzionBusiness.Helpers.ClientHelpers;
using DataFuzionBusiness.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace DataFuzionAPI {

    public class Startup : FunctionsStartup {

        public override void Configure(IFunctionsHostBuilder builder) {
            // DI Services
            builder.Services.AddScoped<ISortNumbersService, SortNumbersService>();
            builder.Services.AddScoped<IListStatsService, ListStatsService>();
            builder.Services.AddScoped<IListAggregateStatsService, ListAggregateStatsService>();
            builder.Services.AddScoped<IHttpClientWrapper, HttpClientWrapper>();

            // Http Clients
            builder.Services.AddHttpClient("DataFuzsion");

            // Logger
            builder.Services.AddLogging();
        }
    }
}