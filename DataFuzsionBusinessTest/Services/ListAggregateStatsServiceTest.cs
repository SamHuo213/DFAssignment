using DataFuzionBusiness.Helpers.ClientHelpers;
using DataFuzionBusiness.Services;
using DataFuzionModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace DataFuzionBusinessTest.Services {

    public class ListAggregateStatsServiceTest {

        [Fact]
        public async Task GetListStats_ValidInput_ReturnListAggregateStatsResponseDto() {
            // Arrange
            var service = GetListAggregateStatsService();

            var listAggregateStatsRequestDto = new ListAggregateStatsRequestDto() {
                Numbers = new List<int>() { 6, 8, 7, 5 }
            };

            // Act
            var listAggregateStatsResponseDto = await service.GetListStats(listAggregateStatsRequestDto);

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
            var service = GetListAggregateStatsService();

            var listAggregateStatsRequestDto = new ListAggregateStatsRequestDto() {
                Numbers = new List<int>() { }
            };

            // Act
            var listAggregateStatsResponseDto = await service.GetListStats(listAggregateStatsRequestDto);

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
            var service = GetListAggregateStatsService(
                new HttpResponseMessage(HttpStatusCode.InternalServerError),
                new HttpResponseMessage(HttpStatusCode.InternalServerError)
            );

            var listAggregateStatsRequestDto = new ListAggregateStatsRequestDto() {
                Numbers = new List<int>() { 6, 8, 7, 5 }
            };

            // Act Assert
            await Assert.ThrowsAsync<Exception>(
                () => service.GetListStats(listAggregateStatsRequestDto)
            );
        }

        private void setupMockSortListResponse(Mock<IHttpClientWrapper> httpClientWrapperMock, HttpResponseMessage? sortListResponseMessage = null) {
            if ( sortListResponseMessage == null ) {
                sortListResponseMessage = new HttpResponseMessage(HttpStatusCode.OK) {
                    Content = JsonContent.Create(new SortListResponseDto() {
                        Numbers = new List<int> { 8, 7, 6, 5 }
                    })
                };
            }

            httpClientWrapperMock.Setup(x =>
            x.PostAsJsonAsync(
                It.IsAny<HttpClient>(),
                "",
                "SortListFunction",
                It.IsAny<SortListRequestDto>()
            )).ReturnsAsync(sortListResponseMessage);
        }

        private void setupMockListStatsResponse(Mock<IHttpClientWrapper> httpClientWrapperMock, HttpResponseMessage? listStatsResponseMessage = null) {
            if ( listStatsResponseMessage == null ) {
                listStatsResponseMessage = new HttpResponseMessage(HttpStatusCode.OK) {
                    Content = JsonContent.Create(new ListStatsResponseDto() {
                        EvenSum = 14,
                        EvenAvg = 7
                    })
                };
            }

            httpClientWrapperMock.Setup(x =>
            x.PostAsJsonAsync(
                It.IsAny<HttpClient>(),
                "",
                "ListStatsFunction",
                It.IsAny<ListStatsRequestDto>()
            )).ReturnsAsync(listStatsResponseMessage);
        }

        private ListAggregateStatsService GetListAggregateStatsService(HttpResponseMessage? sortListResponseMessage = null, HttpResponseMessage? listStatsResponseMessage = null) {
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var httpClientWrapperMock = new Mock<IHttpClientWrapper>();

            httpClientFactoryMock.Setup(x => x.CreateClient("DataFuzsion"))
                .Returns(new HttpClient());

            setupMockSortListResponse(httpClientWrapperMock, sortListResponseMessage);
            setupMockListStatsResponse(httpClientWrapperMock, listStatsResponseMessage);

            return new ListAggregateStatsService(
                httpClientFactoryMock.Object,
                httpClientWrapperMock.Object
            );
        }
    }
}