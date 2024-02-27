using RegistrationWizard.Models;

namespace RegistrationWizard.Interfaces.IRepository
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetAllCountriesAsync();
    }
}
