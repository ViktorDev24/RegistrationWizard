using Moq;
using RegistrationWizard.Interfaces;
using RegistrationWizard.Models;
using RegistrationWizard.Services;
using Xunit;

namespace RegistrationWizard.Tests.Services
{
    public class CountryServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly CountryService _countryService;

        public CountryServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _countryService = new CountryService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetAllCountriesAsync_ReturnsCountryDtos()
        {
            // Arrange
            var countries = new List<Country>
        {
            new Country { Id = 1, Name = "Country A" },
            new Country { Id = 2, Name = "Country B" }
        };
            _mockUnitOfWork.Setup(uow => uow.Countries.GetAllCountriesAsync()).ReturnsAsync(countries);

            // Act
            var result = await _countryService.GetAllCountriesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(countries.Count, result.Count());
            _mockUnitOfWork.Verify(uow => uow.Countries.GetAllCountriesAsync(), Times.Once);
        }
    }
}
