using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace TravelBuddy.Domain.Entities
{
    public class Notification : FullAuditedAggregateRoot<Guid>
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime NotificationDate { get; set; }
        public Guid? DestinationId { get; set; }
    }
}