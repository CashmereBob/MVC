using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCLabb.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        public void AddUser(User user)
        {
            user.Salt = CreateSalt(10);
            user.Password = GenerateSHA256Hash(user.Password, user.Salt);

            using (var ctx = new MVCLabbEntities())
            {
                ctx.Users.Add(user);
                ctx.SaveChanges();
            }
        }

        public string CreateSalt(int size)
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider(); //Skapar upp en ny "Random generator" från security namespase.
            var buff = new byte[size]; //Skapar upp en array med längden från inparametern.
            rng.GetBytes(buff); //Random genererar bytes i arrayen
            return Convert.ToBase64String(buff); //Konverterar och returnerar det nu färdiga saltet.
        }

        public string GenerateSHA256Hash(string input, string salt)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt); //Kodar om input tillsammans med saltet och lägger i en byte array
            var sha256hashstring = new System.Security.Cryptography.SHA256Managed(); //Skapar upp en SHA256 krypterare från namespacet security
            byte[] hash = sha256hashstring.ComputeHash(bytes); //Skickar in vår byte variabel till vår SHA256 krypterare och tilldear hashen till en variabel

            return Convert.ToBase64String(hash); //Konverterar hashen till en string och returnerar.
        }

        public List<User> GetAllUsers()
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.Users.Include("Photos").Include("Albums").Include("Comments").ToList();
            }
        }

        public User GetUser(string id)
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.Users.Include("Photos").Include("Albums").Include("Comments").FirstOrDefault(x => x.Id.ToString() == id);
            }
        }

        public void UpdateUser(User user)
        {
            using (var ctx = new MVCLabbEntities())
            {
                User userFromDB = ctx.Users.Where(x => x.Id == user.Id).FirstOrDefault();
                userFromDB.Name = user.Name;
                userFromDB.Country = user.Country;
                userFromDB.Email = user.Email;

                if (userFromDB.Password != user.Password)
                {
                    var salt = CreateSalt(10);
                    var password = GenerateSHA256Hash(user.Password, salt);

                    userFromDB.Salt = salt;
                    userFromDB.Password = password;

                }

                ctx.SaveChanges();
            }
        }

        public User ValidateLogin(string email, string password)
        {
            using (var dbCtx = new MVCLabbEntities())
            {
                foreach (var user in dbCtx.Users)
                {
                    if (email == user.Email && GenerateSHA256Hash(password, user.Salt) == user.Password)
                    {
                        return user;
                    }
                }
            }

            return null;
        }
    }
}
