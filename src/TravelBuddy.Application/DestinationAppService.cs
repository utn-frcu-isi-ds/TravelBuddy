using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using TravelBuddy.Application.Contracts.Dtos;
using TravelBuddy.Application.Contracts.ExternalApis;
using TravelBuddy.Domain.Entities;

namespace TravelBuddy.Application
{
    public class DestinationAppService : CrudAppService<
        Destination,
        DestinationDto,
        Guid>, IDestinationAppService
    {
        private readonly ICityDataProvider _cityDataProvider;

        public DestinationAppService(IRepository<Destination, Guid> repository, ICityDataProvider cityDataProvider)
            : base(repository)
        {
            _cityDataProvider = cityDataProvider;
        }

        // Search external cities using the generic provider
        public async Task<List<CityDataDto>> SearchExternalCitiesAsync(string name, string country = null)
        {
            return await _cityDataProvider.SearchCitiesAsync(name, country);
        }

        public async Task<CityDataDto> GetExternalCityDetailsAsync(string cityId)
        {
            return await _cityDataProvider.GetCityDetailsAsync(cityId);
        }
    }

    public interface IDestinationAppService : ICrudAppService<
        DestinationDto,
        Guid>
    {
        Task<List<CityDataDto>> SearchExternalCitiesAsync(string name, string country = null);
        Task<CityDataDto> GetExternalCityDetailsAsync(string cityId);
    }
}