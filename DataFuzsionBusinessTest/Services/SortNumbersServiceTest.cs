using DataFuzionBusiness.Services;
using DataFuzionModels;
using System.Collections.Generic;
using Xunit;

namespace DataFuzionBusinessTest.Services {

    public class SortNumbersServiceTest {

        [Fact]
        public void SortByDescending_ValidInput_ReturnSortListResponseDto_SortedNumbers() {
            // Arrange
            var service = new SortNumbersService();

            var sortListRequestDto = new SortListRequestDto() {
                Numbers = new List<int>() { 6, 2, 87, 2 }
            };

            // Act
            var sortListResponseDto = service.SortByDescending(sortListRequestDto);

            // Assert
            Assert.Equal(
                new List<int>() { 87, 6, 2, 2 },
                sortListResponseDto.Numbers
            );

            Assert.NotEqual(
                new List<int>() { 6, 2, 87, 2 },
                sortListResponseDto.Numbers
            );
        }

        [Fact]
        public void SortByDescending_ValidInput_ReturnSortListResponseDto_EmptyNumbers() {
            // Arrange
            var service = new SortNumbersService();

            var sortListRequestDto = new SortListRequestDto() {
                Numbers = new List<int>() { }
            };

            // Act
            var sortListResponseDto = service.SortByDescending(sortListRequestDto);

            // Assert
            Assert.Equal(
                new List<int>() { },
                sortListResponseDto.Numbers
            );
        }
    }
}