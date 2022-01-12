using System.Text;
using System.Text.Json;

namespace DataFuzionBusiness.Helpers.ClientHelpers {

    public class HttpClientWrapper : IHttpClientWrapper {

        public Task<HttpResponseMessage> PostAsJsonAsync<TValue>(HttpClient httpClient, string baseUriString, string requestUriString, TValue value) {
            var baseUrl = new Uri(baseUriString);
            var requestUrl = new Uri(baseUrl, requestUriString);

            var content = new StringContent(
                JsonSerializer.Serialize(value),
                Encoding.UTF8,
                "application/json"
            );

            return httpClient.PostAsync(
                requestUrl,
                content
            );
        }
    }
}