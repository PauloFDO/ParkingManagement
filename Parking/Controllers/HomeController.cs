using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Parking.Models;
using Parking.EF;
using Parking.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Parking.Interfaces;
using ApplicationSettings;
using Parking.Code;

namespace Parking.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        ManageAutomaticParkingDates CheckCurrentDays = new ManageAutomaticParkingDates();
        private readonly UserManager<User> _userManager;
        IParkingManagement _parkingManagementManager;
        private readonly ApplicationDbContext _context;
        IParking _parkingManager;
        IParkingDetailsOnDate _parkingDetailsManager;

        public HomeController(ApplicationDbContext context, UserManager<User> userManager, SignInManager<User> signInManager, IParkingManagement parkingManagementManager, IParking parkingManager, IParkingDetailsOnDate parkingDetailsManager)
        {
            _userManager = userManager;
            _parkingManagementManager = parkingManagementManager;
            _parkingManager = parkingManager;
            _parkingDetailsManager = parkingDetailsManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allParkingData = CreateDataIfNeccesary().Result;
            var model = await PopulateModelWithDatabaseElements(allParkingData.ToList());

            return View(model);
        }

        private async Task<IEnumerable<ParkingAndDateRelationship>> CreateDataIfNeccesary()
        {
            var allPArkingData = _parkingManagementManager.GetTodayRecordsAsync().Result.ToList();

            if (!allPArkingData.Any())
            {
                int daysToAddInAdvance = 7;
                await CheckCurrentDays.CreateNextParkingDaysIfNeccesary(daysToAddInAdvance, _parkingManager, _parkingDetailsManager, _parkingManagementManager);
                allPArkingData = _parkingManagementManager.GetTodayRecordsAsync().Result.ToList();
            }

            return await _parkingManagementManager.GetTodayRecordsAsync();
        }

        private async Task<List<SpaceAndUserAssignmentIndex>> PopulateModelWithDatabaseElements(List<ParkingAndDateRelationship> allParkingData)
        {
            var model = new List<SpaceAndUserAssignmentIndex>();

            foreach (var item in allParkingData)
            {
                var userId = GetCurrentUser(_userManager).Id;

                var modelToAdd = new SpaceAndUserAssignmentIndex()
                {
                    ID = item.ID,
                    AssignedDate = item.ParkingDetailsOnDate.Date.ToShortDateString(),
                    AssignSpaceNumber = item.ParkingSpot.SpaceNumber,
                    AssignedUser = item.AssignedToUser?.Email,
                    CanThisUserfreeThisSpace = User.Identity.IsAuthenticated && item.AssignedToUser?.Id == userId,
                    Description = item.ParkingDetailsOnDate.Description,
                    DoesThisSpaceBelongToUser = item.ParkingSpot.PermanentlyAssignedToUserId == userId,
                    isThisSpacePArtiallybookedToday = item.ParkingDetailsOnDate.ApplyPartialBooking,
                    StartTimeIfPartial = item.ParkingDetailsOnDate.StartTimeIfPartial,
                    EndTimeIfPartial = item.ParkingDetailsOnDate.EndTimeIfPartial,
                };

                model.Add(modelToAdd);
            }

            return model;
        }
    }
}
