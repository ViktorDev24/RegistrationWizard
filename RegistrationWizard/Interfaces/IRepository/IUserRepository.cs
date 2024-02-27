using RegistrationWizard.Models;

namespace RegistrationWizard.Interfaces.IRepository
{
    public interface IUserRepository
    {
        Task<bool> UserExists(string email);
        void Add(User user);
    }
}
