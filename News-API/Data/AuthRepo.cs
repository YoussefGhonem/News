using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using News_API.Models;

namespace News_API.Data
{
    public class AuthRepo : IAuthRepo
    {
        private readonly DataContext _context;

        public AuthRepo(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> EmailExists(string email)
        {
            if (await _context.Users.AnyAsync(u => u.Email == email)) return true;
            return false;
        }

        public async Task<User> GetUser(int id)
        {
            var user=await _context.Users.FirstOrDefaultAsync(a=>a.Id==id);
            return user;
        }

        public async Task<bool> UserNameExists(string userName)
        {
            if (await _context.Users.AnyAsync(u => u.UserName == userName)) return true;
            return false;

        }

    }
}