using Moq;
using RegistrationWizard.Interfaces;
using RegistrationWizard.Models;
using RegistrationWizard.Services;
using Xunit;

namespace RegistrationWizard.Tests.Services
{
    public class ProvinceServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly ProvinceService _provinceService;

        public ProvinceServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _provinceService = new ProvinceService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetProvincesByCountryIdAsync_ReturnsProvinceDtos()
        {
            // Arrange
            int countryId = 1;
            var provinces = new List<Province>
        {
            new Province { Id = 1, Name = "Province A", CountryId = countryId },
            new Province { Id = 2, Name = "Province B", CountryId = countryId }
        };
            _mockUnitOfWork.Setup(uow => uow.Provinces.GetProvincesByCountryIdAsync(countryId))
                           .ReturnsAsync(provinces);

            // Act
            var result = await _provinceService.GetProvincesByCountryIdAsync(countryId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(provinces.Count, result.Count());
            _mockUnitOfWork.Verify(uow => uow.Provinces.GetProvincesByCountryIdAsync(countryId), Times.Once);
        }

    }

}
