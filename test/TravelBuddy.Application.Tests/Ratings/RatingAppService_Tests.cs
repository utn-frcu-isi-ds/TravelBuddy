using System;
using System.Threading.Tasks;
using Shouldly;
using Xunit;
using TravelBuddy.Ratings;
using TravelBuddy.Application.Contracts.Ratings;
using Volo.Abp.Users;
using NSubstitute;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Authorization;

namespace TravelBuddy.Application.Tests.Ratings
{
    public class RatingAppService_Tests
    {
        [Fact]
        public async Task CreateAsync_Should_Associate_Rating_With_Current_User()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var currentUser = Substitute.For<ICurrentUser>();
            currentUser.IsAuthenticated.Returns(true);
            currentUser.GetId().Returns(userId);

            var repo = Substitute.For<IRepository<Rating, Guid>>();
            repo.InsertAsync(Arg.Any<Rating>()).Returns(ci => ci.Arg<Rating>());

            // Usa el namespace correcto para RatingAppService
            var service = new TravelBuddy.Ratings.RatingAppService(repo, currentUser);
            var input = new CreateUpdateRatingDto
            {
                DestinationId = Guid.NewGuid(),
                Stars = 5,
                Comment = "Excelente"
            };

            // Act
            var result = await service.CreateAsync(input);

            // Assert
            result.ShouldNotBeNull();
            result.Stars.ShouldBe(5);
            result.Comment.ShouldBe("Excelente");
            result.DestinationId.ShouldBe(input.DestinationId);
            result.UserId.ShouldBe(userId);
        }

        [Fact]
        public async Task CreateAsync_Should_Throw_If_Not_Authenticated()
        {
            // Arrange
            var currentUser = Substitute.For<ICurrentUser>();
            currentUser.IsAuthenticated.Returns(false);
            var repo = Substitute.For<IRepository<Rating, Guid>>();
            var service = new TravelBuddy.Ratings.RatingAppService(repo, currentUser);
            var input = new CreateUpdateRatingDto
            {
                DestinationId = Guid.NewGuid(),
                Stars = 4
            };

            // Act & Assert
            await Assert.ThrowsAsync<AbpAuthorizationException>(() => service.CreateAsync(input));
        }
    }
}
