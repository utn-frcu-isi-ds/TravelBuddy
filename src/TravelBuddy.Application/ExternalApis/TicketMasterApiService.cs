using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TravelBuddy.Application.Contracts.ExternalApis;
using System;

namespace TravelBuddy.Application.ExternalApis
{
    public class TicketMasterApiService : ITicketMasterApiService, IEventDataProvider
    {
        private readonly IConfiguration _configuration;

        public TicketMasterApiService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<TicketMasterEventDto>> GetEventsByCityAsync(string cityName, string country = null)
        {
            // TODO: Implement call to TicketMaster API
            return new List<TicketMasterEventDto>();
        }

        // IEventDataProvider implementation
        async Task<List<EventDataDto>> IEventDataProvider.GetEventsByCityAsync(string cityName, string country)
        {
            var events = await GetEventsByCityAsync(cityName, country);
            var result = new List<EventDataDto>();
            foreach (var e in events)
            {
                result.Add(new EventDataDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    StartDate = e.StartDate,
                    Venue = e.Venue,
                    City = e.City,
                    Country = e.Country,
                    Url = e.Url,
                    ImageUrl = e.ImageUrl
                });
            }
            return result;
        }
    }
}