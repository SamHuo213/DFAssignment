using DataFuzionBusiness.Services;
using DataFuzionModels;
using System.Collections.Generic;
using Xunit;

namespace DataFuzionBusinessTest.Services {

    public class ListStatsServiceTest {

        [Fact]
        public void GetListStats_ValidInput_ReturnListStatsResponseDto() {
            // Arrange
            var service = new ListStatsService();

            var listStatsRequestDto = new ListStatsRequestDto() {
                Numbers = new List<int>() { 6, 8, 7, 5 }
            };

            // Act
            var listStatsResponseDto = service.GetListStats(listStatsRequestDto);

            // Assert
            Assert.Equal(14, listStatsResponseDto.EvenSum);
            Assert.Equal(7, listStatsResponseDto.EvenAvg);
        }

        [Fact]
        public void GetListStats_EmptyInput_ReturnListStatsResponseDto() {
            // Arrange
            var service = new ListStatsService();

            var listStatsRequestDto = new ListStatsRequestDto() {
                Numbers = new List<int>() { }
            };

            // Act
            var listStatsResponseDto = service.GetListStats(listStatsRequestDto);

            // Assert
            Assert.Equal(0, listStatsResponseDto.EvenSum);
            Assert.Equal(0, listStatsResponseDto.EvenAvg);
        }

        [Fact]
        public void GetListStats_NoEvenNumbers_ReturnListStatsResponseDto() {
            // Arrange
            var service = new ListStatsService();

            var listStatsRequestDto = new ListStatsRequestDto() {
                Numbers = new List<int>() { 7, 5 }
            };

            // Act
            var listStatsResponseDto = service.GetListStats(listStatsRequestDto);

            // Assert
            Assert.Equal(0, listStatsResponseDto.EvenSum);
            Assert.Equal(0, listStatsResponseDto.EvenAvg);
        }
    }
}