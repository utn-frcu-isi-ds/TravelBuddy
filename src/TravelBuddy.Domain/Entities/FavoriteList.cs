using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace TravelBuddy.Domain.Entities
{
    public class FavoriteList : FullAuditedAggregateRoot<Guid>
    {
        public Guid UserId { get; set; }
        public ICollection<FavoriteListItem> Items { get; set; } = new List<FavoriteListItem>();
    }

    public class FavoriteListItem : Entity<Guid>
    {
        public Guid FavoriteListId { get; set; }
        public Guid DestinationId { get; set; }
        public DateTime AddedAt { get; set; }
    }
}