using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Parking.Entities
{
    public class ParkingDetailsOnDate
    {
        [Key]
        public int ID { get; set; }

        public DateTime Date { get; set; }

        public bool ApplyPartialBooking { get; set; }

        public TimeSpan? StartTimeIfPartial { get; set; }

        public TimeSpan? EndTimeIfPartial { get; set; }

        public string Description { get; set; }

        public ICollection<ParkingAndDateRelationship> ParkingAndDateRelationship { get; set; }
    }
}