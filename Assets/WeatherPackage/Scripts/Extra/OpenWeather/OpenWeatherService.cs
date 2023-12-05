using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace WeatherSystem.Extra.OpenWeather
{
  public class OpenWeatherService : IWeatherService
  {
    private const string ApiKey = "bdac50fa2e57d62e828edce565c66e81";

    public async Task<WeatherInfo> GetWeatherInfo(double latitude, double longitude, float timeout,
      CancellationToken cancellationToken)
    {
      var url = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={ApiKey}";
      using (var client = new HttpClient())
      {
        client.Timeout = TimeSpan.FromSeconds(timeout);

        try
        {
          HttpResponseMessage response = await client.GetAsync(url, cancellationToken);
          response.EnsureSuccessStatusCode();
          string result = await response.Content.ReadAsStringAsync();
          var weatherData = JsonConvert.DeserializeObject<OpenWeatherData>(result);
          return weatherData.ToWeather();
        }
        catch (HttpRequestException httpRequestException)
        {
          Debug.LogError(httpRequestException.Message);
          throw;
        }
        catch (TaskCanceledException taskCanceledException)
        {
          Debug.LogError(taskCanceledException.Message);
          throw;
        }
        catch (Exception e)
        {
          Debug.LogError(e.Message);
          throw;
        }
      }
    }
  }
}