using System;

namespace TravelBuddy.Application.Contracts.Dtos
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid DestinationId { get; set; }
        public string Text { get; set; }
    }
}