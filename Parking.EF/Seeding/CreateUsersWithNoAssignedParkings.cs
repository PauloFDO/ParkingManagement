using Parking.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Parking.EF
{
    public static class CreateUsersWithNoAssignedParkings
    {
        public static void SeedUserWithNoParking(this ModelBuilder modelBuilder)
        {
            CreateNewUser(modelBuilder, "NormalUserTest@admintest.co.uk");
        }

        private static void CreateNewUser(ModelBuilder modelBuilder, string userEmail)
        {
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = userEmail,
                EmailConfirmed = true,
                UserName = userEmail,
                NormalizedEmail = userEmail.ToUpper(),
                NormalizedUserName = userEmail.ToUpper(),
            };

            modelBuilder.Entity<User>().HasData(user);
        }
    }
}