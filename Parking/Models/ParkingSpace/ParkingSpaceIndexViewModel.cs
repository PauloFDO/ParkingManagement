using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Models
{
    public class ParkingIndexViewModel
    {
        public int ParkingID { get; set; }

        public int ParkingNumber { get; set; }

        public string PermanentlyAssignToUser { get; set; }
    }
}
