using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace TravelBuddy.Domain.Entities
{
    public class Destination : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public long? Population { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}