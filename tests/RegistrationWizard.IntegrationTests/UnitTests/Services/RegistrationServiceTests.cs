using Microsoft.AspNetCore.Identity;
using Moq;
using RegistrationWizard.Interfaces;
using RegistrationWizard.Models.DTOs;
using RegistrationWizard.Models;
using RegistrationWizard.Services;
using Xunit;

namespace RegistrationWizard.Tests.Services
{
    public class RegistrationServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IPasswordHasher<User>> _mockPasswordHasher;
        private readonly RegistrationService _registrationService;

        public RegistrationServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockPasswordHasher = new Mock<IPasswordHasher<User>>();
            _registrationService = new RegistrationService(_mockUnitOfWork.Object, _mockPasswordHasher.Object);
        }

        [Fact]
        public async Task RegisterUserAsync_ReturnsSuccess_WhenUserDoesNotExist()
        {
            // Arrange
            var registrationDto = new RegistrationDto
            {
                Email = "test@example.com",
                Password = "Password123",
                CountryId = 1,
                ProvinceId = 1
            };
            _mockUnitOfWork.Setup(uow => uow.Users.UserExists(registrationDto.Email)).ReturnsAsync(false);
            _mockPasswordHasher.Setup(ph => ph.HashPassword(null, registrationDto.Password))
                               .Returns("hashedpassword");

            // Act
            var result = await _registrationService.RegisterUserAsync(registrationDto);

            // Assert
            Assert.True(result.Success);
            _mockUnitOfWork.Verify(uow => uow.Users.Add(It.IsAny<User>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task RegisterUserAsync_ReturnsError_WhenUserExists()
        {
            // Arrange
            var registrationDto = new RegistrationDto { Email = "existing@example.com", Password = "Password123" };
            _mockUnitOfWork.Setup(uow => uow.Users.UserExists(registrationDto.Email)).ReturnsAsync(true);

            // Act
            var result = await _registrationService.RegisterUserAsync(registrationDto);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("A user with this email already exists.", result.Errors);
            _mockUnitOfWork.Verify(uow => uow.Users.Add(It.IsAny<User>()), Times.Never);
        }

    }

}
