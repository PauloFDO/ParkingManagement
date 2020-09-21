using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Parking.Entities
{
    public class ParkingAndDateRelationship
    {
        [Key]
        public int ID { get; set; }

        public int ParkingID { get; set; }

        public int ParkingDetailsOnDateID { get; set; }

        public string AssignedToUserID { get; set; }

        public User AssignedToUser { get; set; }

        public ParkingSpot ParkingSpot { get; set; }

        public ParkingDetailsOnDate ParkingDetailsOnDate { get; set; }
    }
}
