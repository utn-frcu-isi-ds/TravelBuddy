using System;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using TravelBuddy.Application.Contracts.Dtos;
using TravelBuddy.Domain.Entities;

namespace TravelBuddy.Application
{
    public class NotificationAppService : CrudAppService<
        Notification,
        NotificationDto,
        Guid>, INotificationAppService
    {
        public NotificationAppService(IRepository<Notification, Guid> repository)
            : base(repository)
        {
        }
    }

    public interface INotificationAppService : ICrudAppService<
        NotificationDto,
        Guid>
    {
    }
}