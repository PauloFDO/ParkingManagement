using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Parking.EF;
using Parking.Entities;
using Parking.Models;
using Parking.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Parking.Code;
using ApplicationSettings;

namespace Parking.Controllers
{
    [Authorize]
    public class ManageParkingSpaceController : Controller
    {
        ManageAutomaticParkingDates CheckCurrentDays = new ManageAutomaticParkingDates();
        private readonly ApplicationDbContext _context;
        IParking _parkingManager;
        IParkingManagement _parkingManagementManager;
        IParkingDetailsOnDate _parkingDetailsManager;


        public ManageParkingSpaceController(ApplicationDbContext context, IParkingManagement parkingManagementManager, IParking parkingManager, IParkingDetailsOnDate parkingDetailsManager)
        {
            _context = context;
            _parkingManagementManager = parkingManagementManager;
            _parkingManager = parkingManager;
            _parkingDetailsManager = parkingDetailsManager;
        }

        public async Task<IActionResult> Index()
        {
            var databaseRecords = await _parkingManager.GetAllParkingSpaces();
            var model = PopulateIndexModelWithDatabaseRecods(databaseRecords);

            return View(model);
        }

        private List<ParkingIndexViewModel> PopulateIndexModelWithDatabaseRecods(IEnumerable<ParkingSpot> records)
        {
            var model = new List<ParkingIndexViewModel>();

            foreach (var item in records)
            {
                var toAddToModel = new ParkingIndexViewModel()
                {
                    ParkingID = item.ID,
                    ParkingNumber = item.SpaceNumber,
                    PermanentlyAssignToUser = item.PermanentlyAssignedToUser?.Email
                };

                model.Add(toAddToModel);
            }

            return model;
        }

        public IActionResult Create()
        {
            var model = new CreateParkingViewModel()
            {
                AssignToUser = new SelectList(_context.Set<User>(), "Id", "Email")
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateParkingViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _parkingManager.CreateAndSaveParkingSpaceAsync(model.PermanentlyAssignedToUserId, model.SpaceNumber);
                await CheckCurrentDays.CreateNextParkingDaysIfNeccesary(0, _parkingManager, _parkingDetailsManager, _parkingManagementManager);

                return RedirectToAction(nameof(Index));
            }

            model.AssignToUser = new SelectList(_context.Set<User>(), "Id", "Email");
            return View(model);
        }

        public async Task<EditParkingViewModel> CreateParkingModelWithDBRecords(ParkingSpot faParking)
        {
            var model = new EditParkingViewModel()
            {
                AssignToUser = new SelectList(_context.Set<User>(), "Id", "Email"),
                ID = faParking.ID,
                PermanentlyAssignedToUserId = faParking.PermanentlyAssignedToUserId,
                SpaceNumber = faParking.SpaceNumber
            };

            return model;
        }

        public async Task<IActionResult> Edit(int id)
        {
            var faParking = await _parkingManager.GetParkingByID(id);
            var model = await CreateParkingModelWithDBRecords(faParking);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditParkingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var fAParking = await _parkingManager.GetParkingByID(model.ID);

                await FreeParkingSpaceFromPreviousOwner(fAParking, model);
                await AddNewOwnerToParkingSpace(fAParking, model);

                return RedirectToAction(nameof(Index));
            }

            model.AssignToUser = new SelectList(_context.Set<User>(), "Id", "Email");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ConstantRoles.AdministratorRole)]
        public async Task<IActionResult> CreateMoreParkingDaysInAdvance()
        {
            int daysToAddInAdvance = 7;
            await CheckCurrentDays.CreateNextParkingDaysIfNeccesary(daysToAddInAdvance, _parkingManager, _parkingDetailsManager, _parkingManagementManager);
            return RedirectToAction("Index", "ManageUserAssignmentsToParking");
        }

        private async Task FreeParkingSpaceFromPreviousOwner(ParkingSpot fAParking, EditParkingViewModel model)
        {
            foreach (var item in fAParking.ParkingAndDateRelationship.ToList())
            {
                if (item.AssignedToUserID == item.ParkingSpot.PermanentlyAssignedToUserId || model.PermanentlyAssignedToUserId == null || item.AssignedToUserID == null)
                {
                    item.AssignedToUserID = model.PermanentlyAssignedToUserId;
                    await _parkingManagementManager.UpdateAndSave(item);
                }
            }
        }

        private async Task AddNewOwnerToParkingSpace(ParkingSpot fAParking, EditParkingViewModel model)
        {
            fAParking.SpaceNumber = model.SpaceNumber;
            fAParking.PermanentlyAssignedToUserId = model.PermanentlyAssignedToUserId;
            await _parkingManager.UpdateAndSaveAsync(fAParking);
        }

    }
}
