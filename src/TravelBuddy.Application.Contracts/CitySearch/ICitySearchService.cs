using System.Threading.Tasks;

namespace TravelBuddy.CitySearch;

public interface ICitySearchService
{
    Task<CitySearchResultDto> SearchCitiesAsync(CitySearchRequestDto request);
}
