using System.Threading;
using System.Threading.Tasks;

namespace WeatherSystem
{
  public class WeatherProvider : IWeatherProvider
  {
    public void AddWeatherService(IWeatherService service)
    {
    }

    public Task<Weather> GetWeather(double latitude, double longitude, float timeout,
      CancellationToken cancellationToken)
    {
      return null;
    }
  }
}