using RegistrationWizard.Models.DTOs;

namespace RegistrationWizard.Interfaces.IService
{
    public interface IProvinceService
    {
        Task<IEnumerable<ProvinceDto>> GetProvincesByCountryIdAsync(int countryId);
        Task<ProvinceDto?> GetProvinceByIdAsync(int provinceId);
    }
}
