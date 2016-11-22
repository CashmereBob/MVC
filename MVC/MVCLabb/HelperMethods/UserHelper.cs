using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCLabb.BI;
using System.Security.Claims;

namespace MVCLabb.HelperMethods
{
    public static class UserHelper
    {
        public static tbl_User GetLogedInUser()
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