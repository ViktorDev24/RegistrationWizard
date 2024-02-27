using RegistrationWizard.Interfaces.IService;
using RegistrationWizard.Interfaces;
using RegistrationWizard.Models.DTOs;

namespace RegistrationWizard.Services
{
    public class CountryService(IUnitOfWork unitOfWork) : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<CountryDto>> GetAllCountriesAsync()
        {
            var countries = await _unitOfWork.Countries.GetAllCountriesAsync();
            return countries.Select(c => new CountryDto
            {
                Id = c.Id,
                Name = c.Name
            });
        }
    }
}
