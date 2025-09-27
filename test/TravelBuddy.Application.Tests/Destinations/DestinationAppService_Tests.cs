using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBuddy.Destinations;
using TravelBuddy.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;
using Volo.Abp.Validation;
using Xunit;

namespace TravelBuddy.Application.Tests.Destinations
{
    public abstract class DestinationAppService_Tests<TStartupModule> : TravelBuddyApplicationTestBase<TStartupModule> where TStartupModule : IAbpModule
    {
        private readonly IDestinationAppService _destinationAppService;
        private readonly IDestinationAppService _service;
        private readonly IDbContextProvider<TravelBuddyDbContext> _dbContextProvider;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        protected DestinationAppService_Tests()
        {
            _destinationAppService = GetRequiredService<IDestinationAppService>();
            _service = ServiceProvider.GetRequiredService<IDestinationAppService>();
            _dbContextProvider = GetRequiredService<IDbContextProvider<TravelBuddyDbContext>>();
            _unitOfWorkManager = GetRequiredService<IUnitOfWorkManager>();

        }

        [Fact]
        public async Task CreateAsync_ShouldReturnCreatedDestinationDto()
        {
            // Arrange
            var input = new CreateUpdateDestinationDto
            {
                Name = "Test City",
                Country = "Test Country"
            };

            // Act
            var result = await _service.CreateAsync(input);

            // Assert
            result.Id.ShouldNotBe(Guid.Empty);
            result.Name.ShouldBe(input.Name);
            result.Country.ShouldBe(input.Country);
        }

        [Fact]
        public async Task CreateAsync_ShouldPersistDestinationInDatabase()
        {
            using (var uow = _unitOfWorkManager.Begin())
            {
                // Arrange
                var input = new CreateUpdateDestinationDto
                {
                    Name = "Test City",
                    Country = "Test Country"
                };

                // Act
                var result = await _service.CreateAsync(input);

                // Assert
                var dbContext = await _dbContextProvider.GetDbContextAsync();
                var savedDestination = await dbContext.Destinations.FirstOrDefaultAsync(t => t.Id == result.Id);

                savedDestination.ShouldNotBeNull();
                savedDestination.Id.ShouldNotBe(Guid.Empty);
                savedDestination.Name.ShouldBe(input.Name);
                savedDestination.Country.ShouldBe(input.Country);
            }
        }


        [Fact]
        public async Task CreateAsync_ShouldThowExceptionWhenNameIsNull()
        {
            // Arrange
            var input = new CreateUpdateDestinationDto
            {                
                Country = "Test Country"
            };

            // Act & Assert
            await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                var result = await _service.CreateAsync(input);
            });
         
        }
    }
}
