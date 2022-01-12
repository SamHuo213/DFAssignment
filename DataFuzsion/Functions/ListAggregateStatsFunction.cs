using DataFuzionBusiness.Services;
using DataFuzionModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace DataFuzionAPI.Functions {

    public class ListAggregateStatsFunction {
        private readonly IListAggregateStatsService listAggregateStatsService;

        public ListAggregateStatsFunction(IListAggregateStatsService listAggregateStatsService) {
            this.listAggregateStatsService = listAggregateStatsService;
        }

        [FunctionName("ListAggregateStats")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] ListAggregateStatsRequestDto listAggregateStatsRequestDto,
            ILogger log
        ) {
            listAggregateStatsRequestDto.Numbers ??= Enumerable.Empty<int>();
            log.LogInformation($"ListAggregateStats with Numbers: {string.Join(", ", listAggregateStatsRequestDto.Numbers)}");

            var listAggregateStatsResponse = await listAggregateStatsService.GetListStats(listAggregateStatsRequestDto);
            return new OkObjectResult(listAggregateStatsResponse);
        }
    }
}