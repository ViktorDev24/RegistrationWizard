using RegistrationWizard.Interfaces.IRepository;

namespace RegistrationWizard.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICountryRepository Countries { get; }
        IProvinceRepository Provinces { get; }
        IUserRepository Users { get; }
        Task<int> CompleteAsync();
    }
}
