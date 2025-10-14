using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Collections.Generic;
using TravelBuddy.CitySearch;

namespace TravelBuddy.External.CitySearch;

public class GeoDbCitySearchService : ICitySearchService
{
    private const string ApiKey = "1b87288382msh04081de1250362fp1acf94jsn6c66e7e31d14";
    private const string BaseUrl = "https://wft-geo-db.p.rapidapi.com/v1/geo";
    private const string Host = "wft-geo-db.p.rapidapi.com";
    private readonly HttpClient _httpClient;

    public GeoDbCitySearchService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CitySearchResultDto> SearchCitiesAsync(CitySearchRequestDto request)
    {
        var result = new CitySearchResultDto();
        if (string.IsNullOrWhiteSpace(request?.PartialName))
            return result;

        var url = $"{BaseUrl}/cities?namePrefix={Uri.EscapeDataString(request.PartialName)}&limit=5";
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
        httpRequest.Headers.Add("X-RapidAPI-Key", ApiKey);
        httpRequest.Headers.Add("X-RapidAPI-Host", Host);

        try
        {
            var response = await _httpClient.SendAsync(httpRequest);
            if (!response.IsSuccessStatusCode)
                return result;

            var json = await response.Content.ReadAsStringAsync();
            var geoDbResponse = JsonSerializer.Deserialize<GeoDbCitiesResponse>(json);
            if (geoDbResponse?.Data != null)
            {
                foreach (var city in geoDbResponse.Data)
                {
                    result.Cities.Add(new CityDto
                    {
                        Name = city.Name,
                        Country = city.Country,
                        Region = city.Region,
                        Id = city.Id.ToString()
                    });
                }
            }
        }
        catch
        {
            // Manejo de error: retorna lista vacía
        }
        return result;
    }

    private class GeoDbCitiesResponse
    {
        [JsonPropertyName("data")]
        public List<GeoDbCity> Data { get; set; }
    }

    private class GeoDbCity
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }
    }
}
