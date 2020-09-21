using Parking.Entities;
using Parking.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Interfaces
{
    public interface IParkingDetailsOnDate : IRepository<ParkingDetailsOnDate, int>
    {
        Task<ParkingDetailsOnDate> CreateNewDateDetailsAndSaveAsync(DateTime date, string description = "");

        Task<DateTime> GetLastExistingDate();

        Task UpdateAndSave(ParkingDetailsOnDate entity);
    }
}
