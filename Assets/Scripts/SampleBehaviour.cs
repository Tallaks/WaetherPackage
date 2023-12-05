using System.Threading;
using UnityEngine;
using WeatherSystem;

public class SampleBehaviour : MonoBehaviour
{
  private async void Awake()
  {
    var weatherProvider = new WeatherProvider();
    var cancellationTokenSource = new CancellationTokenSource();
    Weather weather = await weatherProvider.GetWeather(40.11, 44.31, 3, cancellationTokenSource.Token);
    Debug.Log(weather.ToString());
  }
}