using DataFuzionModels;

namespace DataFuzionBusiness.Services {

    public class SortNumbersService : ISortNumbersService {

        public SortListResponseDto SortByDescending(SortListRequestDto sortListRequestDto) {
            var sortedNumbers = sortListRequestDto.Numbers.OrderByDescending(x => x);
            return new SortListResponseDto() { Numbers = sortedNumbers };
        }
    }
}