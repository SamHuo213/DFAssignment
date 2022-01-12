using DataFuzionBusiness.Helpers.ClientHelpers;
using DataFuzionBusiness.Services;
using DataFuzionModels;
using DataFuzsionConsole;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile($"appsettings.json", false, true)
    .AddEnvironmentVariables()
    .Build();

var collection = new ServiceCollection();
collection.AddSingleton<IApplication, Application>();
collection.AddSingleton<IHttpClientWrapper, HttpClientWrapper>();
collection.AddSingleton<IConsoleService, ConsoleService>();
collection.AddHttpClient("DataFuzsion");

collection.AddOptions();
collection.Configure<ApplicationSettingDo>(configuration.GetSection("ApplicationSettings"));

var serviceProvider = collection.BuildServiceProvider();
var application = serviceProvider.GetService<IApplication>();

await application.Run();