using Parking.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ApplicationSettings;

namespace Parking.EF
{
    public static class AssignAdministratorRole
    {
        public static void SeedUsersRolesFromStartupAsync(UserManager<User> userManager)
        {
            string[] emailsToUpdate = { "admin.user@admintest.co.uk" };
            SeedData(userManager, emailsToUpdate);
        }

        private static void SeedData(UserManager<User> userManager, string[] emailsToUpdate)
        {
            foreach (var userEmail in emailsToUpdate)
            {
                User user = userManager.FindByEmailAsync(userEmail).Result;

                if(user == null)
                {
                    var newUser = new User
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = userEmail,
                        EmailConfirmed = true,
                        UserName = userEmail,
                        NormalizedEmail = userEmail.ToUpper(),
                        NormalizedUserName = userEmail.ToUpper(),
                    };

                    var ptestPassword = "TestUser@123";
                    var creatUser = userManager.CreateAsync(newUser, ptestPassword).Result;
                    var result = userManager.AddToRoleAsync(newUser, "Administrator").Result;
                }
            }
        }

        private static void CreateNewUser(UserManager<User> userManager, string email)
        {
                var newUser = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = email,
                    EmailConfirmed = true,
                    UserName = email,
                    NormalizedEmail = email.ToUpper(),
                    NormalizedUserName = email.ToUpper(),
                };

                userManager.CreateAsync(newUser, "TestUser@123");
        }
    }
}