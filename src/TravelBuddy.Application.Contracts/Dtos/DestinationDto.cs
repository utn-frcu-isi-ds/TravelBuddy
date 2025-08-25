using System;

namespace TravelBuddy.Application.Contracts.Dtos
{
    public class DestinationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public long? Population { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}