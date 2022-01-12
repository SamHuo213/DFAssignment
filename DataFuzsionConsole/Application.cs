using DataFuzionBusiness.Services;
using DataFuzionModels;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace DataFuzsionConsole {

    public class Application : IApplication {
        private readonly IEnumerable<int> numbers;
        private readonly IConsoleService consoleService;

        public Application(
            IOptions<ApplicationSettingDo> applicationSettings,
            IConsoleService consoleService
        ) {
            this.consoleService = consoleService;
            this.numbers = applicationSettings.Value.Numbers;
        }

        public async Task Run() {
            var results = await consoleService.GetListStats(numbers);

            var jsonStringResult = JsonSerializer.Serialize(results);
            Console.WriteLine($"Result: {jsonStringResult}");
        }
    }
}