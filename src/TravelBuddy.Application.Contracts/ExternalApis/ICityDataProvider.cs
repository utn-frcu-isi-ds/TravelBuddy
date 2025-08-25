using System.Collections.Generic;
using System.Threading.Tasks;

namespace TravelBuddy.Application.Contracts.ExternalApis
{
    public interface ICityDataProvider
    {
        Task<List<CityDataDto>> SearchCitiesAsync(string name, string country = null);
        Task<CityDataDto> GetCityDetailsAsync(string cityId);
    }

    public class CityDataDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public long? Population { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string PhotoUrl { get; set; }
    }
}