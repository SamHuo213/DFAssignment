using DataFuzionModels;

namespace DataFuzionBusiness.Services {

    public interface ISortNumbersService {

        public SortListResponseDto SortByDescending(SortListRequestDto sortListRequestDto);
    }
}