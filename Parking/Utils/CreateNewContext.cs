using Parking.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Utils
{
    public static class CreateNewContext
    {
        public static ApplicationDbContext GetNewContext()
        {
            var connectionstring = "Connection string";

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionstring);


            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
