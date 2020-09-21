using Parking.EF;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Models
{
    public class CreateParkingViewModel : IValidatableObject
    {
        ApplicationDbContext _context;

        public CreateParkingViewModel()
        {
           _context = CreateNewContextForGeneralUse.ReturnANewContext();
        }

        public SelectList AssignToUser { get; set; }

        public string PermanentlyAssignedToUserId { get; set; }

        public int SpaceNumber { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PermanentlyAssignedToUserId != null && _context.Parkings.Any(x=>x.PermanentlyAssignedToUserId == PermanentlyAssignedToUserId))
            {
                yield return new ValidationResult("This contact already have a parking space assigned.");
            }

            if (_context.Parkings.Any(x => x.SpaceNumber == SpaceNumber))
            {
                yield return new ValidationResult("This parking space number already exists.");
            }
        }
    }
}
