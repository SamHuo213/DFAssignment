namespace DataFuzionBusiness.Helpers.ClientHelpers {

    public interface IHttpClientWrapper {

        public Task<HttpResponseMessage> PostAsJsonAsync<TValue>(HttpClient httpClient, string baseUriString, string requestUriString, TValue value);
    }
}