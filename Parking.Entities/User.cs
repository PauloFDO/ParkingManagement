using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Entities
{
    public class User : IdentityUser
    {
        public ParkingSpot UserParking { get; set; }

        public ICollection<ParkingAndDateRelationship> AssignedToUserInSpecificDate { get; set; }
    }
}
