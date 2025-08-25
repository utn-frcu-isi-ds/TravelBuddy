using System;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using TravelBuddy.Application.Contracts.Dtos;
using TravelBuddy.Domain.Entities;

namespace TravelBuddy.Application
{
    public class RatingAppService : CrudAppService<
        Rating,
        RatingDto,
        Guid>, IRatingAppService
    {
        public RatingAppService(IRepository<Rating, Guid> repository)
            : base(repository)
        {
        }
    }

    public interface IRatingAppService : ICrudAppService<
        RatingDto,
        Guid>
    {
    }
}