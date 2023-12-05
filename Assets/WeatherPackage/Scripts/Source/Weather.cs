using System.Collections.Generic;
using System.Text;

namespace WeatherSystem
{
  public class Weather
  {
    private readonly Dictionary<IWeatherService, WeatherInfo> _weatherInfoByServiceType = new();
    public static Weather Empty => new();

    public WeatherInfo GetWeatherInfoFrom(IWeatherService weatherService)
    {
      if (_weatherInfoByServiceType.TryGetValue(weatherService, out WeatherInfo weatherInfo))
        return weatherInfo;
      return null;
    }

    public override string ToString()
    {
      var stringBuilder = new StringBuilder();
      foreach (IWeatherService service in _weatherInfoByServiceType.Keys)
      {
        stringBuilder.Append(service.GetType().Name);
        stringBuilder.AppendLine(":");
        stringBuilder.Append(_weatherInfoByServiceType[service]);
        stringBuilder.AppendLine();
      }

      return stringBuilder.ToString();
    }

    internal void AddWeatherInfo(IWeatherService service, WeatherInfo weatherInfo)
    {
      _weatherInfoByServiceType.Add(service, weatherInfo);
    }
  }
}