using DataFuzionModels;

namespace DataFuzionBusiness.Services {

    public interface IListStatsService {

        public ListStatsResponseDto GetListStats(ListStatsRequestDto listStatsRequestDto);
    }
}