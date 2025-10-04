using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using TravelBuddy.External.CitySearch;
using TravelBuddy.CitySearch;

namespace TravelBuddy.Application.Tests.External
{
    public class GeoDbCitySearchService_IntegrationTests
    {
        private class FailingHandler : HttpMessageHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                throw new HttpRequestException("Simulated network error");
            }
        }

        private GeoDbCitySearchService CreateService()
        {
            var httpClient = new HttpClient();
            return new GeoDbCitySearchService(httpClient);
        }

        [Fact]
        [Trait("Category", "IntegrationTest")]
        public async Task SearchCitiesAsync_ReturnsResults_ForValidPartialName()
        {
            var service = CreateService();
            var request = new CitySearchRequestDto { PartialName = "Río" };
            var result = await service.SearchCitiesAsync(request);
            Assert.NotNull(result);
            Assert.NotEmpty(result.Cities);
        }

        [Fact]
        [Trait("Category", "IntegrationTest")]
        public async Task SearchCitiesAsync_ReturnsEmpty_ForNoMatch()
        {
            var service = CreateService();
            var request = new CitySearchRequestDto { PartialName = "zzzzzzzzzz" };
            var result = await service.SearchCitiesAsync(request);
            Assert.NotNull(result);
            Assert.Empty(result.Cities);
        }

        [Fact]
        [Trait("Category", "IntegrationTest")]
        public async Task SearchCitiesAsync_ReturnsEmpty_ForInvalidInput()
        {
            var service = CreateService();
            var request = new CitySearchRequestDto { PartialName = "" };
            var result = await service.SearchCitiesAsync(request);
            Assert.NotNull(result);
            Assert.Empty(result.Cities);
        }

        [Fact]
        [Trait("Category", "IntegrationTest")]
        public async Task SearchCitiesAsync_HandlesNetworkError()
        {
            // Simula error de red usando un HttpClient con un handler que lanza excepción
            var httpClient = new HttpClient(new FailingHandler());
            var service = new GeoDbCitySearchService(httpClient);
            var request = new CitySearchRequestDto { PartialName = "Rio" };
            var result = await service.SearchCitiesAsync(request);
            Assert.NotNull(result);
            Assert.Empty(result.Cities);
        }
    }
}
