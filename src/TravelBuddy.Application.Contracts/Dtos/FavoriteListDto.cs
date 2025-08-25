using System;
using System.Collections.Generic;

namespace TravelBuddy.Application.Contracts.Dtos
{
    public class FavoriteListDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<FavoriteListItemDto> Items { get; set; } = new();
    }

    public class FavoriteListItemDto
    {
        public Guid Id { get; set; }
        public Guid FavoriteListId { get; set; }
        public Guid DestinationId { get; set; }
        public DateTime AddedAt { get; set; }
    }
}