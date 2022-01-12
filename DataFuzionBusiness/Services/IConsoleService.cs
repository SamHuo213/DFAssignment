using DataFuzionModels;

namespace DataFuzionBusiness.Services {

    public interface IConsoleService {

        public Task<ListAggregateStatsResponseDto> GetListStats(IEnumerable<int> numbers);
    }
}