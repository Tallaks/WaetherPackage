using System.Collections.Generic;
using System.Text;

namespace WeatherSystem
{
  public class Weather
  {
    private readonly Dictionary<string, WeatherInfo> _weatherInfoByServiceType = new();
    public static Weather Empty => new();

    public WeatherInfo GetWeatherInfoFrom(string weatherServiceName)
    {
      if (_weatherInfoByServiceType.TryGetValue(weatherServiceName, out WeatherInfo weatherInfo))
        return weatherInfo;
      return null;
    }

    public override string ToString()
    {
      var stringBuilder = new StringBuilder();
      if (_weatherInfoByServiceType.Count == 0)
        return "Empty weather";
      foreach (string serviceName in _weatherInfoByServiceType.Keys)
      {
        stringBuilder.Append(serviceName);
        stringBuilder.AppendLine(":");
        stringBuilder.Append(_weatherInfoByServiceType[serviceName]);
        stringBuilder.AppendLine();
      }

      return stringBuilder.ToString();
    }

    internal void AddWeatherInfo(IWeatherService service, WeatherInfo weatherInfo)
    {
      _weatherInfoByServiceType.Add(service.GetType().Name, weatherInfo);
    }
  }
}