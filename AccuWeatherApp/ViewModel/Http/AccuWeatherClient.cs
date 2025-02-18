using System.Net.Http;
using AccuWeatherApp.Model;
using Newtonsoft.Json;

namespace AccuWeatherApp.ViewModel.Http;

public class AccuWeatherClient
{
    private const string BASE_URL = "http://dataservice.accuweather.com/";
    private const string AUTOCOMPLETE_ENDPOINT = "locations/v1/cities/autocomplete?apikey={0}&q={1}";
    private const string CURRENT_CONDITIONS_ENDPOINT = "currentconditions/v1/{0}?apikey={1}&language=ru-ru";
    private const string API_KEY = "API_KEY"; 
    

    public static async Task<List<City>?> GetCities(string query)
    {
        string url = $"{BASE_URL}{string.Format(AUTOCOMPLETE_ENDPOINT, API_KEY, query)}";

        using HttpClient client = new();
        var response = await client.GetAsync(url);
        string json = await response.Content.ReadAsStringAsync();
        List<City>? cities = JsonConvert.DeserializeObject<List<City>>(json);

        return cities;
    }

    public static async Task<CurrentConditions?> GetCurrentConditions(string city)
    {
        string url = $"{BASE_URL}{string.Format(CURRENT_CONDITIONS_ENDPOINT, city, API_KEY)}";

        using HttpClient client = new();
        var response = await client.GetAsync(url);
        string json = await response.Content.ReadAsStringAsync();
        var currentConditions = (JsonConvert.DeserializeObject<List<CurrentConditions>>(json)).FirstOrDefault();

        return currentConditions;
    }
}