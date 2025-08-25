using System;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using TravelBuddy.Application.Contracts.Dtos;
using TravelBuddy.Domain.Entities;

namespace TravelBuddy.Application
{
    public class FavoriteListAppService : CrudAppService<
        FavoriteList,
        FavoriteListDto,
        Guid>, IFavoriteListAppService
    {
        public FavoriteListAppService(IRepository<FavoriteList, Guid> repository)
            : base(repository)
        {
        }
    }

    public interface IFavoriteListAppService : ICrudAppService<
        FavoriteListDto,
        Guid>
    {
    }
}