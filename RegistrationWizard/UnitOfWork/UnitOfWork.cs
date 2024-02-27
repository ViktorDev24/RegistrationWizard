using RegistrationWizard.Data;
using RegistrationWizard.Interfaces;
using RegistrationWizard.Interfaces.IRepository;
using RegistrationWizard.Repositories;

namespace RegistrationWizard
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed = false;

        public ICountryRepository Countries { get; private set; }
        public IProvinceRepository Provinces { get; private set; }
        public IUserRepository Users { get; private set; }


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Countries = new CountryRepository(_context);
            Provinces = new ProvinceRepository(_context);
            Users = new UserRepository(context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _disposed = true;
            }
        }
    }
}
