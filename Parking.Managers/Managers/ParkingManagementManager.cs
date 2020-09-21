using Parking.Entities;
using Parking.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Managers
{
    public class ParkingManagementManager : Repository<ParkingAndDateRelationship, int>, IParkingManagement
    {
        public async Task<ParkingAndDateRelationship> GetByID(int id, bool loadAllRelatedData = true)
        {
            return await this.GetByIdAsync(id, loadAllRelatedData);
        }

        public async Task<IEnumerable<ParkingAndDateRelationship>> GetAllTheRecordsThatBelongToUserAsync(string userId, int parkingID)
        {
            return await this.GetAllItemsOfGivenEntityWithConditionAsync(x => x.ParkingID == parkingID && x.AssignedToUserID == userId);
        }

        public async Task<IEnumerable<ParkingAndDateRelationship>> GetAllTheRecordsFromTodayToAsync(DateTime StopGettingRecordsAt)
        {
            return await this.GetAllItemsWithConditionAndIncludesAsync(x => x.ParkingDetailsOnDate.Date.Date >= DateTime.Now.Date && x.ParkingDetailsOnDate.Date.Date <=  StopGettingRecordsAt,x=>x.ParkingSpot,x=>x.ParkingDetailsOnDate,x=>x.AssignedToUser);
        }

        public async Task<IEnumerable<ParkingAndDateRelationship>> GetTodayRecordsAsync()
        {
            return await this.GetAllItemsWithConditionAndIncludesAsync(x => x.ParkingDetailsOnDate.Date.Date == DateTime.Now.Date, x => x.ParkingDetailsOnDate, x => x.ParkingSpot, x=>x.AssignedToUser);
        }

        public async Task<IEnumerable<ParkingAndDateRelationship>> GetAllTheRecordsFromSpecificDatesAsync(List<DateTime> dates)
        {
            return await this.GetAllItemsOfGivenEntityWithConditionAsync(x => dates.Contains(x.ParkingDetailsOnDate.Date.Date));
        }

        public async Task<ParkingAndDateRelationship> CreateNewParkingRelationshipAndSave(int parkingId, int dateDetailsId, string userId)
        {
            var dateWithSpaceNumber = new ParkingAndDateRelationship()
            {
                ParkingID = parkingId,
                ParkingDetailsOnDateID = dateDetailsId,
                AssignedToUserID = userId,
            };

            await this.CreateAsync(dateWithSpaceNumber);
            await this.SaveAsync();

            return dateWithSpaceNumber;
        }

        public async Task UpdateAndSave(ParkingAndDateRelationship entity)
        {
            await this.UpdateAsync(entity);
            await this.SaveAsync();
        }
    }
}
