using Parking.EF;
using Parking.Entities;
using ApplicationSettings;
using Parking.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Parking.Code
{
    public class StartupCustomServiceActions
    {
        public void PostLoginActions(TicketReceivedContext context, ServiceProvider serviceProvider)
        {
            var userEmail = context.Principal.Claims.First(x => x.Type == "preferred_username").Value;

            var userManager = (UserManager<User>)serviceProvider.GetService(typeof(UserManager<User>));
            var signinManager = (SignInManager<User>)serviceProvider.GetService(typeof(SignInManager<User>));

            var userResult = CheckIfUserIsNewAndCreate(userManager, signinManager, userEmail).Result;
            MergeDatabaseRolesWithClaims(context, userManager, userResult);
        }

        private async Task<User> CheckIfUserIsNewAndCreate(UserManager<User> userManager, SignInManager<User> signinManager, string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                await CreateNewUserInDB(userManager, email);
            }

            return user;
        }

        private static async Task<User> CreateNewUserInDB(UserManager<User> userManager, string email)
        {
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = email,
                EmailConfirmed = true,
                UserName = email,
                NormalizedEmail = email.ToUpper(),
                NormalizedUserName = email.ToUpper(),

            };

            await userManager.CreateAsync(user);

            return user;
        }

        private static void MergeDatabaseRolesWithClaims(TicketReceivedContext context, UserManager<User> userManager, User user)
        {
            var isUserAnAdministrator = userManager.IsInRoleAsync(user, ConstantRoles.AdministratorRole).Result;

            if (isUserAnAdministrator)
            {
                var extraIdentity = new ClaimsIdentity(new List<Claim>() { new Claim(ClaimTypes.Role, ConstantRoles.AdministratorRole) });
                context.Principal.AddIdentity(extraIdentity);
            }
        }

        public void SeedUserRolesOnAplicationInitiation(ServiceProvider serviceProvider)
        {
                var userManager = (UserManager<User>)serviceProvider.GetService(typeof(UserManager<User>));
                AssignAdministratorRole.SeedUsersRolesFromStartupAsync(userManager);
        }

        public void AdministratorUserFromSeeding(IServiceScope scope)
        {
            var userManager = (UserManager<User>)scope.ServiceProvider.GetService(typeof(UserManager<User>));
            AssignAdministratorRole.SeedUsersRolesFromStartupAsync(userManager);
        }
    }
}
