using Parking.EF;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Parking.Models
{
    public class EditParkingUserAssignment : IValidatableObject
    {
        ApplicationDbContext _context;

        public EditParkingUserAssignment()
        {
            _context = CreateNewContextForGeneralUse.ReturnANewContext();
        }

        public int ID { get; set; }

        public string AssignToUserID { get; set; }

        public SelectList AssignToUser { get; set; }

        public int ParkingNumberID { get; set; }

        public SelectList ParkingNumberToSelect { get; set; }

        [DataType(DataType.Date)]
        public DateTime AssingOnDate { get; set; }

        public string Description { get; set; }

        public bool ApplyPartialBooking { get; set; }

        public TimeSpan? StartTimeIfPartial { get; set; }

        public TimeSpan? EndTimeIfPartial { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (AssignToUserID != null && DoesthisUserAlreadyHaveParking()) yield return new ValidationResult("This user is already assigned to a parking place on this day.");

            if(StartTimeIfPartial > EndTimeIfPartial) yield return new ValidationResult("Start time must be before end up.");

            if (ApplyPartialBooking)
            {
                var regexTimeCheck = @"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]$";
                if (StartTimeIfPartial == null || IsValidTimeFormat(StartTimeIfPartial.Value, regexTimeCheck)) yield return new ValidationResult("Start time format is not valid, format e.g.: 13:30.");
                if (EndTimeIfPartial == null || IsValidTimeFormat(EndTimeIfPartial.Value, regexTimeCheck)) yield return new ValidationResult("End time format is not valid, format e.g.: 14:30.");
            }
        }

        private bool IsValidTimeFormat(TimeSpan time, string regexTimeCheck)
        {
            return !Regex.IsMatch(time.ToString(), regexTimeCheck);
        }

        private bool DoesthisUserAlreadyHaveParking()
        {
            return _context.ParkingAndDateRelationship.Any(x => x.ParkingID != ParkingNumberID && x.ParkingDetailsOnDate.Date.Date == AssingOnDate.Date && x.AssignedToUserID == AssignToUserID);
        }
    }
}
