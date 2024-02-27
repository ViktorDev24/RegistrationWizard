using Microsoft.EntityFrameworkCore;
using RegistrationWizard.Data;
using RegistrationWizard.Interfaces.IRepository;
using RegistrationWizard.Models;

namespace RegistrationWizard.Repositories
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<bool> UserExists(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
        }
    }

}
