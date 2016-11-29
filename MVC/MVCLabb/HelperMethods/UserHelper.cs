using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCLabb.Data;
using MVCLabb.Data.Repository;
using System.Security.Claims;

namespace MVCLabb.HelperMethods
{
    public static class UserHelper
    {
        public static User GetLogedInUser(IUserRepository UserBI)
        {
            var claims = HttpContext.Current.User.Identity as ClaimsIdentity;

            if (claims != null)
            {
                var userID = claims.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                return UserBI.GetUser(userID);
            }

            return null;
        }
    }
}