using DataFuzionBusiness.Services;
using DataFuzionModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace DataFuzionAPI.Functions {

    public class ListStatsFunction {
        private readonly IListStatsService listStatsService;

        public ListStatsFunction(IListStatsService listStatsService) {
            this.listStatsService = listStatsService;
        }

        [FunctionName("ListStatsFunction")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] ListStatsRequestDto listStatsRequestDto,
            ILogger log
        ) {
            listStatsRequestDto.Numbers ??= Enumerable.Empty<int>();
            log.LogInformation($"ListStatsFunction with Numbers: {string.Join(", ", listStatsRequestDto.Numbers)}");

            var listStatsResponseDto = listStatsService.GetListStats(listStatsRequestDto);
            return new OkObjectResult(listStatsResponseDto);
        }
    }
}