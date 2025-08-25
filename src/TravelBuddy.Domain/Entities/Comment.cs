using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace TravelBuddy.Domain.Entities
{
    public class Comment : FullAuditedAggregateRoot<Guid>
    {
        public Guid UserId { get; set; }
        public Guid DestinationId { get; set; }
        public string Text { get; set; }
    }
}