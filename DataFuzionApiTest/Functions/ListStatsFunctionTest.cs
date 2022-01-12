using DataFuzionAPI.Functions;
using DataFuzionBusiness.Services;
using DataFuzionModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace DataFuzionApiTest.Functions {

    public class ListStatsFunctionTest {

        private ListStatsFunction GetListStatsFunction(ListStatsResponseDto listStatsResponseDto) {
            var listStatsService = new Mock<IListStatsService>();

            listStatsService.Setup(x => x.GetListStats(It.IsAny<ListStatsRequestDto>()))
                .Returns(listStatsResponseDto);

            return new ListStatsFunction(
                listStatsService.Object
            );
        }

        [Fact]
        public void ListStatsFunctionRun_ValidInput_ReturnListStatsResponseDto() {
            // Arrange
            var listStatsResponseDto = new ListStatsResponseDto() { EvenSum = 14, EvenAvg = 7 };
            var listStatsRequestDto = new ListStatsRequestDto() { Numbers = new List<int>() { 6, 8, 7, 5 } };

            var loggerMock = new Mock<ILogger>();

            var listStatsFunction = GetListStatsFunction(listStatsResponseDto);

            // Act
            var result = listStatsFunction.Run(
                listStatsRequestDto,
                loggerMock.Object
            );

            // Assert
            var okResult = result as OkObjectResult;
            var actualListStatsResponseDto = okResult?.Value as ListStatsResponseDto;

            Assert.Equal(14, actualListStatsResponseDto?.EvenSum);
            Assert.Equal(7, actualListStatsResponseDto?.EvenAvg);
        }

        [Fact]
        public void ListStatsFunctionRun_NullNumbers_ReturnListStatsResponseDto_ZeroValues() {
            // Arrange
            var listStatsResponseDto = new ListStatsResponseDto() { EvenSum = 0, EvenAvg = 0 };
            var listStatsRequestDto = new ListStatsRequestDto() { };

            var loggerMock = new Mock<ILogger>();

            var listStatsFunction = GetListStatsFunction(listStatsResponseDto);

            // Act
            var result = listStatsFunction.Run(
                listStatsRequestDto,
                loggerMock.Object
            );

            // Assert
            var okResult = result as OkObjectResult;
            var actualListStatsResponseDto = okResult?.Value as ListStatsResponseDto;

            Assert.Equal(0, actualListStatsResponseDto?.EvenSum);
            Assert.Equal(0, actualListStatsResponseDto?.EvenAvg);
        }
    }
}