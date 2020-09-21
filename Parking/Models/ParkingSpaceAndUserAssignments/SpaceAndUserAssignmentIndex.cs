using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Models
{
    public class SpaceAndUserAssignmentIndex
    {
        public int ID { get; set; }

        public string AssignedDate { get; set; }

        public int AssignSpaceNumber { get; set; }

        public string AssignedUser { get; set; }

        public string Description { get; set; }

        public bool CanThisUserfreeThisSpace { get; set; }

        public bool DoesThisSpaceBelongToUser { get; set; }

        public bool isThisSpacePArtiallybookedToday { get; set; }

        public TimeSpan? StartTimeIfPartial { get; set; }

        public TimeSpan? EndTimeIfPartial { get; set; }
    }
}
