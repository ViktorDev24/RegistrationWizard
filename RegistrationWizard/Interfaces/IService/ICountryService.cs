using RegistrationWizard.Models.DTOs;

namespace RegistrationWizard.Interfaces.IService
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryDto>> GetAllCountriesAsync();
    }
}
