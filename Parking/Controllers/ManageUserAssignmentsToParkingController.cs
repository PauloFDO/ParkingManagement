using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Parking.EF;
using Parking.Entities;
using Parking.Models;
using Microsoft.AspNetCore.Identity;
using Parking.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Parking.Controllers
{
    [Authorize]
    public class ManageUserAssignmentsToParkingController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        IParkingManagement _parkingManagementManager;
        IParkingDetailsOnDate _parkingDetailsManager;
        IParking _parkingManager;

        public ManageUserAssignmentsToParkingController(ApplicationDbContext context, UserManager<User> userManager, IParkingManagement parkingManagementManager, IParkingDetailsOnDate parkingDetailsManager, IParking parkingManager)
        {
            _context = context;
            _userManager = userManager;
            _parkingManagementManager = parkingManagementManager;
            _parkingDetailsManager = parkingDetailsManager;
            _parkingManager = parkingManager;
        }

        public async Task<IActionResult> Index()
        {
            var DBRecords = await GetDataOfAllUsers();
            var model = await BuildModelWithDBValues(DBRecords.OrderBy(x => x.ParkingDetailsOnDate.Date).ThenBy(x => x.ParkingSpot.SpaceNumber));

            return View(model);
        }

        public async Task<IEnumerable<ParkingAndDateRelationship>> GetDataOfAllUsers()
        {
            DateTime StopGettingRecordsAt = DateTime.Now.AddDays(15);
            return await _parkingManagementManager.GetAllTheRecordsFromTodayToAsync(StopGettingRecordsAt);
        }

        public async Task<List<IGrouping<string, SpaceAndUserAssignmentIndex>>> BuildModelWithDBValues(IEnumerable<ParkingAndDateRelationship> parkingRecords)
        {
            var model = new List<SpaceAndUserAssignmentIndex>();
            var currentUser = GetCurrentUser(_userManager);
            var assignedParking = await _parkingManager.GetSingleParkingWithUserID(currentUser.Id);

            foreach (var item in parkingRecords)
            {
                var modelToAdd = new SpaceAndUserAssignmentIndex()
                {
                    ID = item.ID,
                    AssignedDate = item.ParkingDetailsOnDate.Date.ToShortDateString(),
                    AssignSpaceNumber = item.ParkingSpot.SpaceNumber,
                    AssignedUser = item.AssignedToUser?.Email,
                    Description = item.ParkingDetailsOnDate.Description,
                    DoesThisSpaceBelongToUser = item.ParkingSpot.SpaceNumber == assignedParking?.SpaceNumber
                };

                model.Add(modelToAdd);
            }

            return model.GroupBy(x => x.AssignedDate).ToList();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var parkingRelationshipRecords = await _parkingManagementManager.GetByIdAsync(id);
            var model = PopulateEditModelWithDBRecords(parkingRelationshipRecords);

            return View(model);
        }

        private EditParkingUserAssignment PopulateEditModelWithDBRecords(ParkingAndDateRelationship parkingRelationshipRecords)
        {
            var applyPartialBooking = string.IsNullOrEmpty(parkingRelationshipRecords.ParkingDetailsOnDate.Description) ? false
                : parkingRelationshipRecords.ParkingDetailsOnDate.ApplyPartialBooking;

            var model = new EditParkingUserAssignment()
            {
                AssignToUserID = parkingRelationshipRecords.AssignedToUserID,
                ParkingNumberID = parkingRelationshipRecords.ParkingSpot.SpaceNumber,
                AssingOnDate = parkingRelationshipRecords.ParkingDetailsOnDate.Date,
                Description = parkingRelationshipRecords.ParkingDetailsOnDate.Description,
                ApplyPartialBooking = parkingRelationshipRecords.ParkingDetailsOnDate.ApplyPartialBooking,
                StartTimeIfPartial = parkingRelationshipRecords.ParkingDetailsOnDate.StartTimeIfPartial,
                EndTimeIfPartial = parkingRelationshipRecords.ParkingDetailsOnDate.EndTimeIfPartial
            };

            model = PopulateSelectLists(model);
            return model;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditParkingUserAssignment model)
        {
            if (ModelState.IsValid)
            {
                var ParkingAndDateRelationship = await _parkingManagementManager.GetByID(model.ID);

                ParkingAndDateRelationship.ParkingDetailsOnDate.Date = model.AssingOnDate;
                ParkingAndDateRelationship.ParkingDetailsOnDate.Description = model.Description;
                ParkingAndDateRelationship.ParkingID = model.ParkingNumberID;
                ParkingAndDateRelationship.AssignedToUser = null;
                ParkingAndDateRelationship.AssignedToUserID = model.AssignToUserID;
                ParkingAndDateRelationship.ParkingDetailsOnDate.ApplyPartialBooking = model.ApplyPartialBooking;
                ParkingAndDateRelationship.ParkingDetailsOnDate.StartTimeIfPartial = model.StartTimeIfPartial;
                ParkingAndDateRelationship.ParkingDetailsOnDate.EndTimeIfPartial = model.EndTimeIfPartial;

                await _parkingManagementManager.UpdateAndSave(ParkingAndDateRelationship);

                return RedirectToAction(nameof(Index));
            }

            model = PopulateSelectLists(model);

            return View(model);
        }

        public EditParkingUserAssignment PopulateSelectLists(EditParkingUserAssignment model)
        {
            model.AssignToUser = new SelectList(_context.Set<User>(), "Id", "Email");
            model.ParkingNumberToSelect = new SelectList(_context.Set<ParkingSpot>(), "ID", "SpaceNumber");

            return model;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAssignedUser(int ID)
        {
            var fAParkingAndDateRelationship = await _context.ParkingAndDateRelationship.FirstOrDefaultAsync(x => x.ID == ID);

            var userId = GetCurrentUser(_userManager).Id;
            fAParkingAndDateRelationship.AssignedToUserID = userId;

            _context.Update(fAParkingAndDateRelationship);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FreeParking(int ID)
        {
            var fAParkingAndDateRelationship = await _context.ParkingAndDateRelationship.FirstOrDefaultAsync(x => x.ID == ID);

            fAParkingAndDateRelationship.AssignedToUserID = null;

            _context.Update(fAParkingAndDateRelationship);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
