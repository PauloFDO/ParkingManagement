using Parking.EF;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Models
{
    public class CreateParkingUserAssignment : IValidatableObject
    {
        ApplicationDbContext _context;

        public CreateParkingUserAssignment(ApplicationDbContext context)
        {
            _context = context;
        }

        public CreateParkingUserAssignment()
        {
        }

        public string AssignToUserID { get; set; }

        public SelectList AssignToUser { get; set; }

        public int ParkingNumberID { get; set; }

        public SelectList ParkingNumberToSelect { get; set; }

        public DateTime AssingOnDate { get; set; }

        public string Description { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(DoesthisUserAlreadyHaveParking()) yield return new ValidationResult("This contact already exists with this role.");
        }

        private bool DoesthisUserAlreadyHaveParking()
        {

           return _context.ParkingAndDateRelationship.Any(x => x.ParkingDetailsOnDate.Date.Date == AssingOnDate.Date && x.AssignedToUserID == AssignToUserID);

        }
    }
}