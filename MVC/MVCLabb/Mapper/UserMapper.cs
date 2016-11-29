using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCLabb.Models;
using MVCLabb.Data;
using MVCLabb.Data.Repository;

namespace MVCLabb.Mapper
{
    public class UserMapper
    {
        internal static User MapRegistrationViewModel(RegistrationViewModel model)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Email = model.Email,
                Country = "nn",
                Admin = false,
                Name = model.Name
            };
        }

        internal static ManageViewModel MapManageViewModel(User user, IUserRepository UserBI)
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

        internal static User MapManageViewModel(ManageViewModel model)
        {
            return new User
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                Country = model.Country
            };
        }
    }
}