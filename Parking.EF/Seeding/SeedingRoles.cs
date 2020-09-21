using ApplicationSettings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.EF
{
    public static class SeedingRoles
    {
        public static void SeedRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = ConstantRoles.AdministratorRole, NormalizedName = "Administrator".ToUpper() });
        }
    }
}
