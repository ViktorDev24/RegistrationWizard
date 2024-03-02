using Microsoft.AspNetCore.Mvc;
using Moq;
using RegistrationWizard.Controllers;
using RegistrationWizard.Interfaces.IService;
using RegistrationWizard.Models.DTOs;
using Xunit;

namespace RegistrationWizard.Tests.Controllers
{
    public class CountriesControllerTests
    {
        private readonly Mock<ICountryService> _mockCountryService;
        private readonly CountriesController _controller;

        public CountriesControllerTests()
        {
            _mockCountryService = new Mock<ICountryService>();
            _controller = new CountriesController(_mockCountryService.Object);
        }

        [Fact]
        public async Task GetCountries_ReturnsOkObjectResult_WithCountries()
        {
            // Arrange
            var mockCountries = new List<CountryDto> 
            {
                new CountryDto { Id = 1, Name = "Country A" },
                new CountryDto { Id = 2, Name = "Country B" }
            };
            _mockCountryService.Setup(s => s.GetAllCountriesAsync())
                .ReturnsAsync(mockCountries); 

            // Act
            var result = await _controller.GetCountries();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnedCountries = Assert.IsType<List<CountryDto>>(actionResult.Value); 
            Assert.Equal(2, returnedCountries.Count);
        }


        [Fact]
        public async Task GetCountries_ReturnsOk_WithEmptyListWhenNoCountriesExist()
        {
            // Arrange
            _mockCountryService.Setup(s => s.GetAllCountriesAsync()).ReturnsAsync(new List<CountryDto>());

            // Act
            var result = await _controller.GetCountries();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var countries = Assert.IsAssignableFrom<IEnumerable<CountryDto>>(actionResult.Value);
            Assert.Empty(countries);
        }

    }

}
