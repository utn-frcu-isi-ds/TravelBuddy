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
using NSubstitute;
using TravelBuddy.CitySearch;
using Volo.Abp.Domain.Repositories;

namespace TravelBuddy.Application.Tests.Destinations
{
    public abstract class DestinationAppService_Tests<TStartupModule> : TravelBuddyApplicationTestBase<TStartupModule> where TStartupModule : IAbpModule
    {        
        private readonly IDestinationAppService _service;
        private readonly ICitySearchService _citySearchService;
        private readonly IRepository<Destination, Guid> _repository;
        private readonly IDbContextProvider<TravelBuddyDbContext> _dbContextProvider;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        protected DestinationAppService_Tests()
        {         
            
            _dbContextProvider = GetRequiredService<IDbContextProvider<TravelBuddyDbContext>>();
            _unitOfWorkManager = GetRequiredService<IUnitOfWorkManager>();
            _repository = GetRequiredService<IRepository<Destination, Guid>>();
            _citySearchService = Substitute.For<ICitySearchService>();
            _service = GetRequiredService<IDestinationAppService>();
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

        [Fact]
        public async Task SearchCitiesAsync_ReturnsResults()
        {
            // Arrange
            var request = new CitySearchRequestDto { PartialName = "Test" };
            var expected = new CitySearchResultDto
            {
                Cities = new List<CityDto> { new CityDto { Name = "TestCity", Country = "TestCountry", Region = "TestRegion", Id = "1" } }
            };
            var repoMock = Substitute.For<IRepository<Destination, Guid>>();
            var citySearchMock = Substitute.For<ICitySearchService>();
            citySearchMock.SearchCitiesAsync(request).Returns(expected);
            var service = new DestinationAppService(repoMock, citySearchMock);

            // Act
            var result = await service.SearchCitiesAsync(request);

            // Assert
            result.ShouldNotBeNull();
            result.Cities.Count.ShouldBe(1);
            result.Cities[0].Name.ShouldBe("TestCity");
        }

        [Fact]
        public async Task SearchCitiesAsync_ReturnsEmpty()
        {
            // Arrange
            var request = new CitySearchRequestDto { PartialName = "NoMatch" };
            var expected = new CitySearchResultDto { Cities = new List<CityDto>() };
            var repoMock = Substitute.For<IRepository<Destination, Guid>>();
            var citySearchMock = Substitute.For<ICitySearchService>();
            citySearchMock.SearchCitiesAsync(request).Returns(expected);
            var service = new DestinationAppService(repoMock, citySearchMock);

            // Act
            var result = await service.SearchCitiesAsync(request);

            // Assert
            result.ShouldNotBeNull();
            result.Cities.ShouldBeEmpty();
        }

        [Fact]
        public async Task SearchCitiesAsync_InvalidInput_ReturnsEmpty()
        {
            // Arrange
            var request = new CitySearchRequestDto { PartialName = "" };
            var expected = new CitySearchResultDto { Cities = new List<CityDto>() };
            var repoMock = Substitute.For<IRepository<Destination, Guid>>();
            var citySearchMock = Substitute.For<ICitySearchService>();
            citySearchMock.SearchCitiesAsync(request).Returns(expected);
            var service = new DestinationAppService(repoMock, citySearchMock);

            // Act
            var result = await service.SearchCitiesAsync(request);

            // Assert
            result.ShouldNotBeNull();
            result.Cities.ShouldBeEmpty();
        }

        [Fact]
        public async Task SearchCitiesAsync_ApiError_ThrowsException()
        {
            // Arrange
            var request = new CitySearchRequestDto { PartialName = "Test" };
            var repoMock = Substitute.For<IRepository<Destination, Guid>>();
            var citySearchMock = Substitute.For<ICitySearchService>();
            citySearchMock
                .When(x => x.SearchCitiesAsync(request))
                .Do(x => { throw new Exception("API error"); });
            var service = new DestinationAppService(repoMock, citySearchMock);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => service.SearchCitiesAsync(request));
        }
    }
}
