using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TravelBuddy.Application.Contracts.ExternalApis;

namespace TravelBuddy.Application.ExternalApis
{
    public class GeoDbCityApiService : IGeoDbCityApiService, ICityDataProvider
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public GeoDbCityApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<List<GeoDbCityDto>> SearchCitiesAsync(string name, string country = null)
        {
            // TODO: Implement call to GeoDB Cities API
            return new List<GeoDbCityDto>();
        }

        public async Task<GeoDbCityDto> GetCityDetailsAsync(string cityId)
        {
            // TODO: Implement call to GeoDB Cities API
            return null;
        }

        // ICityDataProvider implementation
        async Task<List<CityDataDto>> ICityDataProvider.SearchCitiesAsync(string name, string country)
        {
            // Map to GeoDbCityDto and convert to CityDataDto
            var geoCities = await SearchCitiesAsync(name, country);
            var result = new List<CityDataDto>();
            foreach (var c in geoCities)
            {
                result.Add(new CityDataDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Country = c.Country,
                    Population = c.Population,
                    Latitude = c.Latitude,
                    Longitude = c.Longitude,
                    PhotoUrl = c.PhotoUrl
                });
            }
            return result;
        }

        async Task<CityDataDto> ICityDataProvider.GetCityDetailsAsync(string cityId)
        {
            var c = await GetCityDetailsAsync(cityId);
            if (c == null) return null;
            return new CityDataDto
            {
                Id = c.Id,
                Name = c.Name,
                Country = c.Country,
                Population = c.Population,
                Latitude = c.Latitude,
                Longitude = c.Longitude,
                PhotoUrl = c.PhotoUrl
            };
        }
    }
}