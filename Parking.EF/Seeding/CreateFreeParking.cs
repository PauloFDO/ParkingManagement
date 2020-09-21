using Parking.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.EF
{
    public static class CreateFreeParking
    {
        public static void SeedParkings(this ModelBuilder modelBuilder)
        {
            CreateNewParking(modelBuilder, 337);
            CreateNewParking(modelBuilder, 336);
            CreateNewParking(modelBuilder, 335);
            CreateNewParking(modelBuilder, 334);
        }

        private static void CreateNewParking(ModelBuilder modelBuilder, int parkingNumber)
        {
            var userParking = new ParkingSpot()
            {
                ID = parkingNumber,
                SpaceNumber = parkingNumber
            };

            modelBuilder.Entity<ParkingSpot>().HasData(userParking);
        }
    }
}
