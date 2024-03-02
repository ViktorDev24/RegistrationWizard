using Microsoft.AspNetCore.Mvc;
using Moq;
using RegistrationWizard.DTOs;
using RegistrationWizard.Interfaces.IService;
using RegistrationWizard.Models.DTOs;
using Xunit;


namespace RegistrationWizard.Tests.Controllers
{
    public class RegistrationControllerTests
    {
        private readonly Mock<IRegistrationService> _mockRegistrationService;
        private readonly RegistrationController _controller;

        public RegistrationControllerTests()
        {
            _mockRegistrationService = new Mock<IRegistrationService>();
            _controller = new RegistrationController(_mockRegistrationService.Object);
        }

        [Fact]
        public async Task RegisterUser_ReturnsBadRequest_WhenRegistrationFails()
        {
            // Arrange
            var registrationDto = new RegistrationDto { /* Populate with test data */ };
            var registrationResult = new RegistrationResult
            {
                Success = false,
                Errors = new List<string> { "Email already exists" }
            };
            _mockRegistrationService.Setup(service => service.RegisterUserAsync(registrationDto))
                .ReturnsAsync(registrationResult);

            // Act
            var result = await _controller.RegisterUser(registrationDto);

            // Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            var errors = Assert.IsType<List<string>>(actionResult.Value);
            Assert.Contains("Email already exists", errors);
        }

        [Fact]
        public async Task RegisterUser_ReturnsOk_WhenRegistrationSucceeds()
        {
            // Arrange
            var registrationDto = new RegistrationDto { /* Populate with test data */ };
            var registrationResult = new RegistrationResult
            {
                Success = true
            };
            _mockRegistrationService.Setup(service => service.RegisterUserAsync(registrationDto))
                .ReturnsAsync(registrationResult);

            // Act
            var result = await _controller.RegisterUser(registrationDto);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnedResult = Assert.IsType<RegistrationResult>(actionResult.Value);
            Assert.True(returnedResult.Success);
        }
    }


}