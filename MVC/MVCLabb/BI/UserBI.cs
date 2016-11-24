using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using MVCLabb.Models;

namespace MVCLabb.BI
{
    public static class UserBI
    {
        
        internal static tbl_User ValidateLogin(string email, string password)
        {
            using (var dbCtx = new MVCLabbEntities())
            {
                foreach (var user in dbCtx.tbl_User)
                {
                    if (email == user.Email && UserBI.GenerateSHA256Hash(password, user.Salt) == user.Password)
                    {
                        return user;
                    }
                }
            }

            return null;
        }

        internal static List<tbl_User> GetAllUsers()
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.tbl_User.Include("tbl_Photo").Include("tbl_Album").Include("tbl_Comment").ToList();
            }
        }

        internal static tbl_User GetUser(string id)
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.tbl_User.Include("tbl_Photo").Include("tbl_Album").Include("tbl_Comment").FirstOrDefault(x => x.Id.ToString() == id);
            }
        }

        public static string CreateSalt(int size) //Metod för att skapa salt, tar en inparameter som bestämmer längden på saltet.
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider(); //Skapar upp en ny "Random generator" från security namespase.
            var buff = new byte[size]; //Skapar upp en array med längden från inparametern.
            rng.GetBytes(buff); //Random genererar bytes i arrayen
            return Convert.ToBase64String(buff); //Konverterar och returnerar det nu färdiga saltet.
        }

        public static string GenerateSHA256Hash(string input, string salt) //Metod för att hasha lösenordet tillsammans med ett salt
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt); //Kodar om input tillsammans med saltet och lägger i en byte array
            var sha256hashstring = new System.Security.Cryptography.SHA256Managed(); //Skapar upp en SHA256 krypterare från namespacet security
            byte[] hash = sha256hashstring.ComputeHash(bytes); //Skickar in vår byte variabel till vår SHA256 krypterare och tilldear hashen till en variabel

            return Convert.ToBase64String(hash); //Konverterar hashen till en string och returnerar.
        }

        public static void AddUser(tbl_User user)
        {
            user.Salt = CreateSalt(10);
            user.Password = GenerateSHA256Hash(user.Password, user.Salt);

            using (var ctx = new MVCLabbEntities())
            {
                ctx.tbl_User.Add(user);
                ctx.SaveChanges();
            }
        }

        internal static void UpdateUser(tbl_User user)
        {
            using (var ctx = new MVCLabbEntities())
            {
                tbl_User userFromDB = ctx.tbl_User.Where(x => x.Id == user.Id).FirstOrDefault();
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
    }
}