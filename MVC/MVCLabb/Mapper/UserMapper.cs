using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCLabb.Models;
using MVCLabb.BI;

namespace MVCLabb.Mapper
{
    public class UserMapper
    {
        internal static tbl_User MapRegistrationViewModel(RegistrationViewModel model)
        {
            return new tbl_User
            {
                Id = Guid.NewGuid(),
                Email = model.Email,
                Country = "nn",
                Admin = false,
                Name = model.Name
            };
        }

        internal static ManageViewModel MapManageViewModel(tbl_User user)
        {
            user = UserBI.GetUser(user.Id.ToString());

            return new ManageViewModel
            {
                Name = user.Name,
                Email = user.Email,
                Country = user.Country,
                Password = user.Password,
                ConfirmPassword = user.Password
            };

        }

        internal static tbl_User MapManageViewModel(ManageViewModel model)
        {
            return new tbl_User
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                Country = model.Country
            };
        }
    }
}