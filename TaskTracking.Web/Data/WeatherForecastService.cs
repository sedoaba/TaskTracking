using TaskTracking.Web.Services;

namespace TaskTracking.Web.Data
{
    public class WeatherForecastService
    {

        private ITrackingHttpClient _trackingHttpClient;

        public WeatherForecastService(ITrackingHttpClient trackingHttpClient)
        {
            _trackingHttpClient = trackingHttpClient;
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public Task<WeatherForecast[]> GetForecastAsync(DateOnly startDate)
        {
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray());
        }
    }
}
