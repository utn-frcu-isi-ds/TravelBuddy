using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace TravelBuddy.Domain.Entities
{
    public class Rating : FullAuditedAggregateRoot<Guid>
    {
        public Guid UserId { get; set; }
        public Guid DestinationId { get; set; }
        public int Stars { get; set; } // 1 to 5
        public string Comment { get; set; }
    }
}