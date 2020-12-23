using System.Threading.Tasks;
using News_API.Models;

namespace News_API.Data
{
    public interface IAuthRepo
    {
       Task <bool> UserNameExists(string userName);
       Task <bool> EmailExists(string email);
        Task <User> GetUser(int id);
    }
}