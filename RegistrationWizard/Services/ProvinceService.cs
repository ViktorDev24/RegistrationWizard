using RegistrationWizard.Interfaces;
using RegistrationWizard.Interfaces.IService;
using RegistrationWizard.Models.DTOs;

namespace RegistrationWizard.Services
{
    public class ProvinceService(IUnitOfWork unitOfWork) : IProvinceService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<ProvinceDto>> GetProvincesByCountryIdAsync(int countryId)
        {
            var provinces = await _unitOfWork.Provinces.GetProvincesByCountryIdAsync(countryId);
            return provinces.Select(p => new ProvinceDto
            {
                Id = p.Id,
                Name = p.Name
            });
        }

        public async Task<ProvinceDto?> GetProvinceByIdAsync(int provinceId)
        {
            var province = await _unitOfWork.Provinces.GetProvinceByIdAsync(provinceId);
            if (province != null)
            {
                return new ProvinceDto
                {
                    Id = province.Id,
                    Name = province.Name,
                };
            }
            return null;
        }

    }
}
