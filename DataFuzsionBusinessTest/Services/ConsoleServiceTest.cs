using DataFuzionBusiness.Helpers.ClientHelpers;
using DataFuzionBusiness.Services;
using DataFuzionModels;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace DataFuzionBusinessTest.Services {

    public class ConsoleServiceTest {

        [Fact]
        public async Task GetListStats_ValidInput_ReturnListAggregateStatsResponseDto() {
            // Arrange
            var service = GetConsoleService();

            // Act
            var listAggregateStatsResponseDto = await service.GetListStats(new List<int>() { 6, 8, 7, 5 });

            // Assert
            Assert.Equal(
                new List<int>() { 8, 7, 6, 5 },
                listAggregateStatsResponseDto.Sorted
            );

            Assert.Equal(14, listAggregateStatsResponseDto.EvenSum);
            Assert.Equal(7, listAggregateStatsResponseDto.EvenAvg);
        }

        [Fact]
        public async Task GetListStats_EmptyNumbers_ReturnListAggregateStatsResponseDto() {
            // Arrange
            var service = GetConsoleService();

            // Act
            var listAggregateStatsResponseDto = await service.GetListStats(new List<int>() { });

            // Assert
            Assert.Equal(
                new List<int>() { },
                listAggregateStatsResponseDto.Sorted
            );

            Assert.Equal(0, listAggregateStatsResponseDto.EvenSum);
            Assert.Equal(0, listAggregateStatsResponseDto.EvenAvg);
        }

        [Fact]
        public async Task GetListStats_ValidInput_StatusNotSuccess_Throws() {
            // Arrange
            var service = GetConsoleService(
                new HttpResponseMessage(HttpStatusCode.InternalServerError)
            );

            // Act Assert
            await Assert.ThrowsAsync<Exception>(
                () => service.GetListStats(new List<int>() { 6, 8, 7, 5 })
            );
        }

        private void setupMockListAggregateStatsResponse(Mock<IHttpClientWrapper> httpClientWrapperMock, HttpResponseMessage? listAggregateStatsResponseMessage = null) {
            if ( listAggregateStatsResponseMessage == null ) {
                listAggregateStatsResponseMessage = new HttpResponseMessage(HttpStatusCode.OK) {
                    Content = JsonContent.Create(new ListAggregateStatsResponseDto() {
                        Sorted = new List<int>() { 8, 7, 6, 5 },
                        EvenSum = 14,
                        EvenAvg = 7
                    })
                };
            }

            httpClientWrapperMock.Setup(x =>
                x.PostAsJsonAsync(
                    It.IsAny<HttpClient>(),
                    "",
                    "ListAggregateStats",
                    It.IsAny<ListAggregateStatsRequestDto>()
            )).ReturnsAsync(listAggregateStatsResponseMessage);
        }

        private ConsoleService GetConsoleService(HttpResponseMessage? listAggregateStatsResponseMessage = null) {
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var httpClientWrapperMock = new Mock<IHttpClientWrapper>();

            httpClientFactoryMock.Setup(x => x.CreateClient("DataFuzsion"))
                .Returns(new HttpClient());

            setupMockListAggregateStatsResponse(httpClientWrapperMock, listAggregateStatsResponseMessage);

            return new ConsoleService(
                Options.Create(new ApplicationSettingDo() { BaseApiUrl = "" }),
                httpClientFactoryMock.Object,
                httpClientWrapperMock.Object
            );
        }
    }
}