using DataFuzionBusiness.Helpers.ClientHelpers;
using DataFuzionModels;
using System.Net.Http.Json;

namespace DataFuzionBusiness.Services {

    public class ListAggregateStatsService : IListAggregateStatsService {
        private readonly string baseDataFuzionApi;

        private readonly HttpClient httpClient;
        private readonly IHttpClientWrapper httpClientWrapper;

        public ListAggregateStatsService(
            IHttpClientFactory httpClientFactory,
            IHttpClientWrapper httpClientWrapper
        ) {
            this.baseDataFuzionApi = Environment.GetEnvironmentVariable("BaseApiUrl") ?? "";
            this.httpClient = httpClientFactory.CreateClient("DataFuzsion");
            this.httpClientWrapper = httpClientWrapper;
        }

        public async Task<ListAggregateStatsResponseDto> GetListStats(ListAggregateStatsRequestDto listAggregateStatsRequestDto) {
            if ( !listAggregateStatsRequestDto.Numbers.Any() ) {
                return new ListAggregateStatsResponseDto() {
                    Sorted = new List<int>(),
                    EvenSum = 0,
                    EvenAvg = 0
                };
            }

            var sortListRequest = GetSortListRequest(listAggregateStatsRequestDto.Numbers);
            var listStatsRequest = GetListStatsRequest(listAggregateStatsRequestDto.Numbers);

            await Task.WhenAll(sortListRequest, listStatsRequest);

            var sortListResponseMessage = await sortListRequest;
            var listStatsResponseMessage = await listStatsRequest;

            if ( !sortListResponseMessage.IsSuccessStatusCode || !listStatsResponseMessage.IsSuccessStatusCode ) {
                throw new Exception("Failed to retrieve information from sortList or listStats");
            }

            var sortListResponseDto = await sortListResponseMessage.Content.ReadFromJsonAsync<SortListResponseDto>();
            var listStatsResponseDto = await listStatsResponseMessage.Content.ReadFromJsonAsync<ListStatsResponseDto>();

            return new ListAggregateStatsResponseDto() {
                Sorted = sortListResponseDto.Numbers,
                EvenSum = listStatsResponseDto.EvenSum,
                EvenAvg = listStatsResponseDto.EvenAvg
            };
        }

        private Task<HttpResponseMessage> GetSortListRequest(IEnumerable<int> numbers) {
            return httpClientWrapper.PostAsJsonAsync(
                httpClient,
                baseDataFuzionApi,
                "SortListFunction",
                new SortListRequestDto() {
                    Numbers = numbers,
                }
            );
        }

        private Task<HttpResponseMessage> GetListStatsRequest(IEnumerable<int> numbers) {
            return httpClientWrapper.PostAsJsonAsync(
                httpClient,
                baseDataFuzionApi,
                "ListStatsFunction",
                new ListStatsRequestDto() {
                    Numbers = numbers,
                }
            );
        }
    }
}