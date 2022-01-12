using DataFuzionBusiness.Helpers.ClientHelpers;
using DataFuzionModels;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace DataFuzionBusiness.Services {

    public class ConsoleService : IConsoleService {
        private readonly string baseDataFuzionApi;

        private readonly HttpClient httpClient;
        private readonly IHttpClientWrapper httpClientWrapper;

        public ConsoleService(
            IOptions<ApplicationSettingDo> applicationSettings,
            IHttpClientFactory httpClientFactory,
            IHttpClientWrapper httpClientWrapper
        ) {
            this.baseDataFuzionApi = applicationSettings.Value.BaseApiUrl;
            this.httpClient = httpClientFactory.CreateClient("DataFuzsion");
            this.httpClientWrapper = httpClientWrapper;
        }

        public async Task<ListAggregateStatsResponseDto> GetListStats(IEnumerable<int> numbers) {
            if ( !numbers.Any() ) {
                return new ListAggregateStatsResponseDto() {
                    Sorted = new List<int>(),
                    EvenSum = 0,
                    EvenAvg = 0
                };
            }

            var aggregateStatsResponseMessage = await GetAggregateStats(numbers);
            if ( !aggregateStatsResponseMessage.IsSuccessStatusCode ) {
                throw new Exception("Failed to retrieve information from aggregateStats");
            }

            return await aggregateStatsResponseMessage
                .Content
                .ReadFromJsonAsync<ListAggregateStatsResponseDto>();
        }

        private Task<HttpResponseMessage> GetAggregateStats(IEnumerable<int> numbers) {
            return httpClientWrapper.PostAsJsonAsync(
                httpClient,
                baseDataFuzionApi,
                "ListAggregateStats",
                new ListAggregateStatsRequestDto() {
                    Numbers = numbers,
                }
            );
        }
    }
}