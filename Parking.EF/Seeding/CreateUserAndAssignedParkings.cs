using Parking.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.EF
{
    public static class CreateUserAndAssignedParkings
    {
        public static void SeedAssignedParkings(this ModelBuilder modelBuilder)
        {
            CreateNewUserAndAssignParking(modelBuilder, "userWithAssignedspot@usertest.co.uk", 345);
        }

        private static void CreateNewUserAndAssignParking(ModelBuilder modelBuilder, string userEmail, int parkingNumber)
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

            var userParking = new ParkingSpot()
            {
                ID = parkingNumber,
                SpaceNumber = parkingNumber,
                PermanentlyAssignedToUserId = user.Id
            };

            modelBuilder.Entity<User>().HasData(user);
            modelBuilder.Entity<ParkingSpot>().HasData(userParking);
        }
    }
}