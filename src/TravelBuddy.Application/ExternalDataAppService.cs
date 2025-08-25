using System.Collections.Generic;
using System.Threading.Tasks;
using TravelBuddy.Application.Contracts.ExternalApis;
using Volo.Abp.Application.Services;

namespace TravelBuddy.Application
{
    public class ExternalDataAppService : ApplicationService
    {
        private readonly ICityDataProvider _cityDataProvider;
        private readonly IEventDataProvider _eventDataProvider;

        public ExternalDataAppService(ICityDataProvider cityDataProvider, IEventDataProvider eventDataProvider)
        {
            _cityDataProvider = cityDataProvider;
            _eventDataProvider = eventDataProvider;
        }

        public async Task<List<CityDataDto>> SearchCitiesAsync(string name, string country = null)
        {
            return await _cityDataProvider.SearchCitiesAsync(name, country);
        }

        public async Task<CityDataDto> GetCityDetailsAsync(string cityId)
        {
            return await _cityDataProvider.GetCityDetailsAsync(cityId);
        }

        public async Task<List<EventDataDto>> GetEventsByCityAsync(string cityName, string country = null)
        {
            return await _eventDataProvider.GetEventsByCityAsync(cityName, country);
        }
    }
}
