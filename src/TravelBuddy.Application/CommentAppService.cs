using System;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using TravelBuddy.Application.Contracts.Dtos;
using TravelBuddy.Domain.Entities;

namespace TravelBuddy.Application
{
    public class CommentAppService : CrudAppService<
        Comment,
        CommentDto,
        Guid>, ICommentAppService
    {
        public CommentAppService(IRepository<Comment, Guid> repository)
            : base(repository)
        {
        }
    }

    public interface ICommentAppService : ICrudAppService<
        CommentDto,
        Guid>
    {
    }
}