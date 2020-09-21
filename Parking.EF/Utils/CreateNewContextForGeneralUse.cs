using ApplicationSettings;
using Parking.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.EF
{
    public static class CreateNewContextForGeneralUse
    {
        public static ApplicationDbContext ReturnANewContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(ConsumeAppSettingElements.GetConnectionStringFromAppSetting());

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
