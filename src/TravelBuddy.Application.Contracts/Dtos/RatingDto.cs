using System;

namespace TravelBuddy.Application.Contracts.Dtos
{
    public class RatingDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid DestinationId { get; set; }
        public int Stars { get; set; }
        public string Comment { get; set; }
    }
}