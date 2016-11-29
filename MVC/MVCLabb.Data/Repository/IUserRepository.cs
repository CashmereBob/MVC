using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCLabb.Data.Repository
{
    public interface IUserRepository
    {
        User ValidateLogin(string email, string password);

        List<User> GetAllUsers();

        User GetUser(string id);

        string CreateSalt(int size);

        string GenerateSHA256Hash(string input, string salt);

        void AddUser(User user);

        void UpdateUser(User user);
    }
}
