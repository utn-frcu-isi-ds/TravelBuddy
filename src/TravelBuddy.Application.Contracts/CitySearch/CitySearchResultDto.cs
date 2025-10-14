using System.Collections.Generic;

namespace TravelBuddy.CitySearch;

public class CitySearchResultDto
{
    public List<CityDto> Cities { get; set; } = new();
}
