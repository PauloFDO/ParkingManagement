using Parking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Interfaces
{
    public interface IParkingManagement : IRepository<ParkingAndDateRelationship, int>
    {
        Task<ParkingAndDateRelationship> GetByID(int id, bool loadAllRelatedData = true);

        Task<IEnumerable<ParkingAndDateRelationship>> GetAllTheRecordsThatBelongToUserAsync(string userId, int parkingID);

        Task<IEnumerable<ParkingAndDateRelationship>> GetAllTheRecordsFromTodayToAsync(DateTime StopGettingRecordsAt);

        Task<IEnumerable<ParkingAndDateRelationship>> GetAllTheRecordsFromSpecificDatesAsync(List<DateTime> dates);

        Task<IEnumerable<ParkingAndDateRelationship>> GetTodayRecordsAsync();

        Task<ParkingAndDateRelationship> CreateNewParkingRelationshipAndSave(int parkingId, int dateDetailsId, string userId);

        Task UpdateAndSave(ParkingAndDateRelationship entity);
    }
}
