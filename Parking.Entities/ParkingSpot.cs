using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parking.Entities
{
    public class ParkingSpot
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }

        public string PermanentlyAssignedToUserId { get; set; }

        public int SpaceNumber { get; set; }

        public User PermanentlyAssignedToUser { get; set; }

        public ICollection<ParkingAndDateRelationship> ParkingAndDateRelationship { get; set; }
    }
}