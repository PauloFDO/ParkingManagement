using Parking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Interfaces
{
    public interface IParking : IRepository<ParkingSpot, int>
    {
        Task<ParkingSpot> GetParkingByID(int parkingID);

        Task<IEnumerable<ParkingSpot>> GetAllParkingSpaces();

        Task<ParkingSpot> CreateAndSaveParkingSpaceAsync(string userId, int parkingNumber);

        Task<ParkingSpot> GetSingleParkingWithUserID(string parkingAssignedToUserId, bool LoadFullData = true);

        Task<IEnumerable<ParkingSpot>> GetAllParkingSpacesWithMissingDate(DateTime date, bool loadRelatedData = true);

        Task UpdateAndSaveAsync(ParkingSpot parking);
    }
}
