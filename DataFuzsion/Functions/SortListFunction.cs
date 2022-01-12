using DataFuzionBusiness.Services;
using DataFuzionModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace DataFuzionAPI.Functions {

    public class SortListFunction {
        private readonly ISortNumbersService sortNumbersService;

        public SortListFunction(ISortNumbersService sortNumbersService) {
            this.sortNumbersService = sortNumbersService;
        }

        [FunctionName("SortListFunction")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] SortListRequestDto sortListRequest,
            ILogger log
        ) {
            sortListRequest.Numbers ??= Enumerable.Empty<int>();
            log.LogInformation($"SortListFunction with Numbers: {string.Join(", ", sortListRequest.Numbers)}");

            var sortedListResponse = sortNumbersService.SortByDescending(sortListRequest);
            return new OkObjectResult(sortedListResponse);
        }
    }
}