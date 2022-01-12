using DataFuzionModels;

namespace DataFuzionBusiness.Services {

    public class ListStatsService : IListStatsService {

        public ListStatsResponseDto GetListStats(ListStatsRequestDto listStatsRequestDto) {
            var evenNumbers = listStatsRequestDto
                .Numbers
                .Where(x => x % 2 == 0);

            if ( !evenNumbers.Any() ) {
                return new ListStatsResponseDto() {
                    EvenSum = evenNumbers.Sum(),
                    EvenAvg = 0
                };
            }

            return new ListStatsResponseDto() {
                EvenSum = evenNumbers.Sum(),
                EvenAvg = (int) evenNumbers.Average()
            };
        }
    }
}