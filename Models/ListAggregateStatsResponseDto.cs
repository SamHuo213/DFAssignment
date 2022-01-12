namespace DataFuzionModels {

    public class ListAggregateStatsResponseDto {
        public IEnumerable<int> Sorted { get; set; }

        public int EvenSum { get; set; }

        public int EvenAvg { get; set; }
    }
}