using DataFuzionModels;

namespace DataFuzionBusiness.Services {

    public interface IListAggregateStatsService {

        public Task<ListAggregateStatsResponseDto> GetListStats(ListAggregateStatsRequestDto listAggregateStatsRequestDto);
    }
}