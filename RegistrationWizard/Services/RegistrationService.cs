using RegistrationWizard.Interfaces.IService;
using RegistrationWizard.Interfaces;
using RegistrationWizard.Models.DTOs;
using RegistrationWizard.DTOs;
using RegistrationWizard.Models;
using Microsoft.AspNetCore.Identity;

namespace RegistrationWizard.Services
{
    public class RegistrationService(IUnitOfWork unitOfWork, IPasswordHasher<User> passwordHasher) : IRegistrationService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;

        public async Task<RegistrationResult> RegisterUserAsync(RegistrationDto registrationDto)
        {
            var userExists = await _unitOfWork.Users.UserExists(registrationDto.Email);
            if (userExists)
            {
                return new RegistrationResult
                {
                    Success = false,
                    Errors = new List<string> { "A user with this email already exists." }
                };
            }

            var newUser = new User
            {
                Email = registrationDto.Email,
                PasswordHash = _passwordHasher.HashPassword(null, registrationDto.Password),
                CountryId = registrationDto.CountryId,
                ProvinceId = registrationDto.ProvinceId
            };

             _unitOfWork.Users.Add(newUser);

            try
            {
                await _unitOfWork.CompleteAsync();

                return new RegistrationResult
                {
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new RegistrationResult
                {
                    Success = false,
                    Errors = new List<string> { "An error occurred while registering the user. " + ex.Message }
                };
            }
        }

    }

}
