using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using RegistrationWizard.Models.DTOs;


namespace RegistrationWizard.IntegrationTests.IntegrationTests
{
    public class CountryServiceIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public CountryServiceIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task GetAllCountries_ReturnsSuccessStatusCode()
        {
            // Arrange
            var request = "/api/countries";

            // Act
            var response = await _client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetAllCountries_ReturnsCorrectContentType()
        {
            // Arrange
            var request = "/api/countries";

            // Act
            var response = await _client.GetAsync(request);

            // Assert
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task GetAllCountries_ReturnsNonEmptyResponse()
        {
            // Arrange
            var request = "/api/countries";

            // Act
            var response = await _client.GetAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            var countries = JsonConvert.DeserializeObject<IEnumerable<CountryDto>>(content);

            // Assert
            Assert.NotNull(countries);
            Assert.NotEmpty(countries);
        }
    }
}
