using RegistrationWizard.Models;

namespace RegistrationWizard.Interfaces.IRepository
{
    public interface IProvinceRepository
    {
        Task<IEnumerable<Province>> GetAllProvincesAsync();
        Task<Province> GetProvinceByIdAsync(int id);
        Task<IEnumerable<Province>> GetProvincesByCountryIdAsync(int countryId);
    }
}
