using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinesLayer
{
    class BIUser
    {
        internal static bool LoginUser(string email, string password)
        {
            using (var dbCtx = new MVCLabbEntities())
            {

                foreach (var user in dbCtx.tbl_User)
                {
                    if (email == user.Email && UserHelper.GenerateSHA256Hash(password, user.Salt) == user.Password)
                    {
                        var identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Admin ? "Admin" : "User")
                        },
                            "ApplicationCookie");

                        var ctx = HttpContext.Current.Request.GetOwinContext();
                        var authManager = ctx.Authentication;

                        authManager.SignIn(identity);

                        return true;
                    }
                }
            }

            return false;
        }
    }
}
