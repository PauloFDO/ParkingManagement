using Parking.EF;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Models
{
    public class EditParkingViewModel : IValidatableObject
    {
        ApplicationDbContext _context;

        public EditParkingViewModel()
        {
            _context = CreateNewContextForGeneralUse.ReturnANewContext();
        }

        public int ID { get; set; }

        public SelectList AssignToUser { get; set; }

        public string PermanentlyAssignedToUserId { get; set; }

        public int SpaceNumber { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PermanentlyAssignedToUserId != null && DoesthisUserAlreadyHaveParking()) yield return new ValidationResult("This user is already assigned to a parking space.");


            if (_context.Parkings.Any(x => x.ID != ID && x.SpaceNumber == SpaceNumber))
            {
                yield return new ValidationResult("This parking space number already exists.");
            }
        }

        private bool DoesthisUserAlreadyHaveParking()
        {
            return _context.Parkings.Any(x => x.ID != ID  && x.PermanentlyAssignedToUserId == PermanentlyAssignedToUserId);
        }

    }
}
