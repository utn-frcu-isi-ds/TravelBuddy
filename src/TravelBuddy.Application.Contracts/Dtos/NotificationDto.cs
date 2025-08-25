using System;

namespace TravelBuddy.Application.Contracts.Dtos
{
    public class NotificationDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime NotificationDate { get; set; }
        public Guid? DestinationId { get; set; }
    }
}