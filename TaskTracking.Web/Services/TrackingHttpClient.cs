namespace TaskTracking.Web.Services
{
    public class TrackingHttpClient : ITrackingHttpClient
    {

        private readonly HttpClient _httpClient;

        public TrackingHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
