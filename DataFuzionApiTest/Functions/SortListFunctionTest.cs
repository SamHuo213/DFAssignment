using DataFuzionAPI.Functions;
using DataFuzionBusiness.Services;
using DataFuzionModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace DataFuzionApiTest.Functions {

    public class SortListFunctionTest {

        private SortListFunction GetSortListFunction(SortListResponseDto sortListResponseDto) {
            var sortNumbersServiceMock = new Mock<ISortNumbersService>();

            sortNumbersServiceMock.Setup(x => x.SortByDescending(It.IsAny<SortListRequestDto>()))
                .Returns(sortListResponseDto);

            return new SortListFunction(
                sortNumbersServiceMock.Object
            );
        }

        [Fact]
        public void SortListFunctionRun_ValidInput_ReturnSortListResponseDto_SortedNumbers() {
            // Arrange
            var sortListResponseDto = new SortListResponseDto() { Numbers = new List<int>() { 87, 6, 2, 2 } };
            var sortListRequest = new SortListRequestDto() { Numbers = new List<int>() { 87, 6, 2, 2 } };

            var loggerMock = new Mock<ILogger>();

            var sortListFunction = GetSortListFunction(sortListResponseDto);

            // Act
            var result = sortListFunction.Run(
                sortListRequest,
                loggerMock.Object
            );

            // Assert
            var okResult = result as OkObjectResult;
            var actualSortListResponseDto = okResult?.Value as SortListResponseDto;

            Assert.Equal(
                sortListResponseDto.Numbers,
                actualSortListResponseDto?.Numbers
            );
        }

        [Fact]
        public void SortListFunctionRun_NullNumbers_ReturnSortListResponseDto_EmptyNumbers() {
            // Arrange
            var sortListResponseDto = new SortListResponseDto() { Numbers = new List<int>() { } };
            var sortListRequest = new SortListRequestDto();

            var loggerMock = new Mock<ILogger>();

            var sortListFunction = GetSortListFunction(sortListResponseDto);

            // Act
            var result = sortListFunction.Run(
                sortListRequest,
                loggerMock.Object
            );

            // Assert
            var okResult = result as OkObjectResult;
            var actualSortListResponseDto = okResult?.Value as SortListResponseDto;

            Assert.Equal(
                sortListResponseDto.Numbers,
                actualSortListResponseDto?.Numbers
            );
        }
    }
}