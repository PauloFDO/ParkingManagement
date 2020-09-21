using Parking.Interfaces;
using FluentDateTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Code
{
    public class ManageAutomaticParkingDates
    {
        int daysToAddInAdvance = 0;
        int daysInAdvanceToCreate = 7;
        IParking _parkingManager;
        IParkingDetailsOnDate _parkingDetailsManager;
        IParkingManagement _parkingManagementManager;

        private async Task AssignClassVariables(int daysToAddInAdvance,IParking parkingManager, IParkingDetailsOnDate parkingDetailsManager, IParkingManagement parkingManagementManager)
        {
            _parkingManager = parkingManager;
            _parkingDetailsManager = parkingDetailsManager;
            _parkingManagementManager = parkingManagementManager;
            this.daysToAddInAdvance = daysToAddInAdvance;
        }

        public async Task CreateNextParkingDaysIfNeccesary(int daysToAddInAdvance, IParking parkingManager, IParkingDetailsOnDate parkingDetailsManager, IParkingManagement parkingManagementManager)
        {
            await AssignClassVariables(daysToAddInAdvance,parkingManager, parkingDetailsManager, parkingManagementManager);
            var LastExistingDateInTheDB = await _parkingDetailsManager.GetLastExistingDate();

            var allNeededDaysExist = await CheckIfAllNeededDaysExist(LastExistingDateInTheDB);
            if (allNeededDaysExist) return;

            var isThereAnyMissingDate = await _parkingManager.GetAllParkingSpacesWithMissingDate(LastExistingDateInTheDB.AddBusinessDays(this.daysToAddInAdvance).Date, false);
            var numberOfPArkingSpaces = await _parkingManager.GetAllParkingSpaces();

            if (isThereAnyMissingDate.Count() == numberOfPArkingSpaces.Count()) return;

            var numberOfDaysToCheck = await GetTotalNumberOfDaysToCheck(LastExistingDateInTheDB);
            await CheckIfRecordExistIOnDateAndCreateIfNot(numberOfDaysToCheck);
        }

        private async Task<int> GetTotalNumberOfDaysToCheck(DateTime LastExistingDateInTheDB)
        {
            var result = (int)(LastExistingDateInTheDB.AddDays(daysToAddInAdvance).Date - DateTime.Now.Date).TotalDays;
            return result < 0 ? daysInAdvanceToCreate : result;
        }

        private async Task<bool> CheckIfAllNeededDaysExist(DateTime LastExistingDateInTheDB)
        {
            var isThereAnyMissingDate = await _parkingManager.GetAllParkingSpacesWithMissingDate(LastExistingDateInTheDB.AddBusinessDays(daysToAddInAdvance).Date, false);
            var numberOfPArkingSpaces = await _parkingManager.GetAllParkingSpaces();

            return isThereAnyMissingDate.Count() == numberOfPArkingSpaces.Count();
        }

        private async Task CheckIfRecordExistIOnDateAndCreateIfNot(int totalDaysToAdd)
        {
            for (int i = 0; i < totalDaysToAdd + 1; i++)
            {
                var allSpaceNumbers = await _parkingManager.GetAllAsync();
                var parkingsWithThisMissingDate = await _parkingManager.GetAllParkingSpacesWithMissingDate(DateTime.Now.AddBusinessDays(i).Date, false);

                if (allSpaceNumbers.Any())
                {
                    foreach (var item in allSpaceNumbers)
                    {
                        var doesThisSpaceHaveANumber = parkingsWithThisMissingDate.Contains(item);

                        if (!doesThisSpaceHaveANumber)
                        {
                            var savedParkingDetailsRecord = await _parkingDetailsManager.CreateNewDateDetailsAndSaveAsync(DateTime.Now.AddBusinessDays(i));
                            await _parkingManagementManager.CreateNewParkingRelationshipAndSave(item.ID, savedParkingDetailsRecord.ID, item.PermanentlyAssignedToUserId);
                        }
                    }
                }
            }
        }
    }
}
