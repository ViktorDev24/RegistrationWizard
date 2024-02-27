using RegistrationWizard.DTOs;
using RegistrationWizard.Models.DTOs;

namespace RegistrationWizard.Interfaces.IService
{
    public interface IRegistrationService
    {
        Task<RegistrationResult> RegisterUserAsync(RegistrationDto registrationDto);
    }
}
