using Parking.Entities;
using Parking.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Managers
{
    public class ParkingDetailsOnDateManager : Repository<ParkingDetailsOnDate, int>, IParkingDetailsOnDate
    {
        public async Task<ParkingDetailsOnDate> CreateNewDateDetailsAndSaveAsync(DateTime date,string description = "")
        {
            var ParkingDetailsOnDate = new ParkingDetailsOnDate()
            {
                Date = date,
                Description = description,
                ApplyPartialBooking = false,
                StartTimeIfPartial = TimeSpan.FromHours(00),
                EndTimeIfPartial = TimeSpan.FromHours(23)
            };

            await this.CreateAsync(ParkingDetailsOnDate);
            await this.SaveAsync();

            return ParkingDetailsOnDate;
        }

        public async Task<DateTime> GetLastExistingDate()
        {
            var getExistentValues = await this.GetAllAsync();
            return getExistentValues.Any() ? getExistentValues.Max(x => x.Date) : DateTime.Now;
        }

        public async Task UpdateAndSave(ParkingDetailsOnDate entity)
        {
            await this.UpdateAsync(entity);
            await this.SaveAsync();
        }
    }
}
