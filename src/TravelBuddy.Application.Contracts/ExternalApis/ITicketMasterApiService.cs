using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TravelBuddy.Application.Contracts.ExternalApis
{
    public interface ITicketMasterApiService
    {
        Task<List<TicketMasterEventDto>> GetEventsByCityAsync(string cityName, string country = null);
    }

    public class TicketMasterEventDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public string Venue { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
    }
}