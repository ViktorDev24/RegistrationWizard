using Microsoft.EntityFrameworkCore;
using RegistrationWizard.Data;
using RegistrationWizard.Interfaces.IRepository;
using RegistrationWizard.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationWizard.Repositories
{
    public class ProvinceRepository(ApplicationDbContext context) : IProvinceRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IEnumerable<Province>> GetAllProvincesAsync()
        {
            return await _context.Provinces.ToListAsync();
        }

        public async Task<Province> GetProvinceByIdAsync(int id)
        {
            var province = await _context.Provinces.FindAsync(id);
            return province ?? throw new KeyNotFoundException($"A province with ID {id} was not found.");
        }

        public async Task<IEnumerable<Province>> GetProvincesByCountryIdAsync(int countryId)
        {
            return await _context.Provinces
                                 .Where(p => p.CountryId == countryId)
                                 .ToListAsync();
        }

    }
}
