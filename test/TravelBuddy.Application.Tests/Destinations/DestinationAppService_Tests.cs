using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Xunit;
using Shouldly;
using TravelBuddy.Destinations;
using Microsoft.Extensions.DependencyInjection;

namespace TravelBuddy.Application.Tests.Destinations
{
    public class DestinationAppService_Tests : TravelBuddyApplicationTestBase<TravelBuddyApplicationModule>
    {
        private readonly IDestinationAppService _service;

        public DestinationAppService_Tests()
        {
            _service = ServiceProvider.GetRequiredService<IDestinationAppService>();
        }

        [Fact]
        public async Task Should_Create_Destination()
        {
            var input = new CreateUpdateDestinationDto
            {
                Name = "Test City",
                Country = "Test Country"
            };
            var result = await _service.CreateAsync(input);
            result.Id.ShouldNotBe(Guid.Empty);
            result.Name.ShouldBe(input.Name);
            result.Country.ShouldBe(input.Country);
        }

        [Fact]
        public async Task Should_Get_Destination_By_Id()
        {
            var input = new CreateUpdateDestinationDto { Name = "Get City", Country = "Get Country" };
            var created = await _service.CreateAsync(input);
            var result = await _service.GetAsync(created.Id);
            result.ShouldNotBeNull();
            result.Name.ShouldBe(input.Name);
        }

        [Fact]
        public async Task Should_Update_Destination()
        {
            var input = new CreateUpdateDestinationDto { Name = "Update City", Country = "Update Country" };
            var created = await _service.CreateAsync(input);
            var updateInput = new CreateUpdateDestinationDto { Name = "Updated City", Country = "Updated Country" };
            var updated = await _service.UpdateAsync(created.Id, updateInput);
            updated.Name.ShouldBe(updateInput.Name);
            updated.Country.ShouldBe(updateInput.Country);
        }

        [Fact]
        public async Task Should_Delete_Destination()
        {
            var input = new CreateUpdateDestinationDto { Name = "Delete City", Country = "Delete Country" };
            var created = await _service.CreateAsync(input);
            await _service.DeleteAsync(created.Id);
            var all = await _service.GetListAsync(new PagedAndSortedResultRequestDto());
            all.Items.ShouldNotContain(x => x.Id == created.Id);
        }

        [Fact]
        public async Task Should_Get_List_Of_Destinations()
        {
            var input1 = new CreateUpdateDestinationDto { Name = "List City 1", Country = "Country 1" };
            var input2 = new CreateUpdateDestinationDto { Name = "List City 2", Country = "Country 2" };
            await _service.CreateAsync(input1);
            await _service.CreateAsync(input2);
            var result = await _service.GetListAsync(new PagedAndSortedResultRequestDto());
            result.Items.Count.ShouldBeGreaterThanOrEqualTo(2);
        }
    }
}
