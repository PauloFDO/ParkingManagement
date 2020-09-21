using Parking.Entities;
using Parking.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Managers
{
    public class ParkingManager : Repository<ParkingSpot,int>, IParking
    {
        public async Task<ParkingSpot> GetParkingByID(int parkingID)
        {
            return await this.GetByIdAsync(parkingID);
        }

        public async Task<ParkingSpot> GetSingleParkingWithUserID(string parkingAssignedToUserId, bool loadallData = true)
        {
            return await this.GetSingleItemOnConditionAsync(x => x.PermanentlyAssignedToUserId == parkingAssignedToUserId, loadallData);
        }

        public async Task<IEnumerable<ParkingSpot>> GetAllParkingSpaces()
        {
            return await this.GetAllItemsOfGivenEntityWithIncludesAsync(x => x.PermanentlyAssignedToUser);
        }

        public async Task<IEnumerable<ParkingSpot>> GetAllParkingSpacesWithMissingDate(DateTime date, bool loadRelatedData = true)
        {
           return await this.GetAllItemsOfGivenEntityWithConditionAsync(x=> x.ParkingAndDateRelationship.Any(i=>i.ParkingDetailsOnDate.Date.Date == date.Date), loadRelatedData);
        }

        public async Task<ParkingSpot> CreateAndSaveParkingSpaceAsync(string userId,int parkingNumber)
        {
            var parking = new ParkingSpot()
            {
                PermanentlyAssignedToUserId = userId,
                SpaceNumber = parkingNumber
            };

            await this.CreateAsync(parking);
            await this.SaveAsync();

            return parking;
        }

        public async Task UpdateAndSaveAsync(ParkingSpot parking)
        {
            await this.UpdateAsync(parking);
            await this.SaveAsync();
        }
    }
}
