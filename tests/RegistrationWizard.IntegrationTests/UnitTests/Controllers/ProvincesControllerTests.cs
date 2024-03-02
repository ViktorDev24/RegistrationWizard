using Microsoft.AspNetCore.Mvc;
using Moq;
using RegistrationWizard.Controllers;
using RegistrationWizard.Interfaces.IService;
using RegistrationWizard.Models;
using RegistrationWizard.Models.DTOs;


namespace RegistrationWizard.Tests.Controllers
{
    public class ProvincesControllerTests
{
    private readonly Mock<IProvinceService> _mockProvinceService;
    private readonly ProvincesController _controller;

    public ProvincesControllerTests()
    {
        _mockProvinceService = new Mock<IProvinceService>();
        _controller = new ProvincesController(_mockProvinceService.Object);
    }

    [Fact]
    public async Task GetProvinces_ReturnsOkObjectResult_WithProvinces()
    {
        // Arrange
        int countryId = 1;
        var mockProvinces = new List<ProvinceDto>
        {
            new ProvinceDto { Id = 1, Name = "Province A" },
            new ProvinceDto { Id = 2, Name = "Province B" }
        };
        _mockProvinceService.Setup(service => service.GetProvincesByCountryIdAsync(countryId))
            .ReturnsAsync(mockProvinces);

        // Act
        var result = await _controller.GetProvinces(countryId);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedProvinces = Assert.IsType<List<ProvinceDto>>(actionResult.Value);
        Assert.Equal(2, returnedProvinces.Count);
    }

    [Fact]
    public async Task GetProvinces_ReturnsNotFound_WhenNoProvincesExist()
    {
        // Arrange
        int countryId = 1;
        _mockProvinceService.Setup(service => service.GetProvincesByCountryIdAsync(countryId))
            .ReturnsAsync(new List<ProvinceDto>()); 

        // Act
        var result = await _controller.GetProvinces(countryId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
}
    
}