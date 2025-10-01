using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using TravelBuddy.Ratings;
using TravelBuddy.Application.Contracts.Ratings;

namespace TravelBuddy.Ratings
{
    public class RatingAppService : CrudAppService<
        Rating,
        RatingDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateRatingDto>, IRatingAppService
    {
        public RatingAppService(IRepository<Rating, Guid> repository)
            : base(repository)
        {
        }
    }

    public interface IRatingAppService : ICrudAppService<
        RatingDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateRatingDto>
    {
    }
}
