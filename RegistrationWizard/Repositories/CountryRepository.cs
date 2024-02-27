using Microsoft.EntityFrameworkCore;
using RegistrationWizard.Data;
using RegistrationWizard.Interfaces.IRepository;
using RegistrationWizard.Models;

namespace RegistrationWizard.Repositories
{
    public class CountryRepository(ApplicationDbContext context) : ICountryRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IEnumerable<Country>> GetAllCountriesAsync()
        {
            return await _context.Countries.ToListAsync();
        }

    }
}
