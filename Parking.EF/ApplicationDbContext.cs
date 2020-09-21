using System.Linq;
using Parking.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Parking.EF
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Mapping(modelBuilder);
            Seeding(modelBuilder);
        }

        private void Mapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ParkingSpot>().HasMany(x => x.ParkingAndDateRelationship).WithOne(x => x.ParkingSpot).HasForeignKey(y => y.ParkingID).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ParkingAndDateRelationship>().HasOne(x => x.ParkingSpot).WithMany(x => x.ParkingAndDateRelationship).HasForeignKey(y => y.ParkingID).OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<ParkingDetailsOnDate>().HasMany(x => x.ParkingAndDateRelationship).WithOne(x => x.ParkingDetailsOnDate).HasForeignKey(y => y.ParkingDetailsOnDateID).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ParkingAndDateRelationship>().HasOne(x => x.ParkingDetailsOnDate).WithMany(x => x.ParkingAndDateRelationship).HasForeignKey(y => y.ParkingDetailsOnDateID).OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<ParkingAndDateRelationship>().HasOne(x => x.AssignedToUser).WithMany(x => x.AssignedToUserInSpecificDate).HasForeignKey(y => y.AssignedToUserID).OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<ParkingSpot>().HasMany(x => x.ParkingAndDateRelationship).WithOne(x => x.ParkingSpot).HasForeignKey(y => y.ParkingID).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>().HasMany(x => x.AssignedToUserInSpecificDate).WithOne(x => x.AssignedToUser).HasForeignKey(y => y.AssignedToUserID).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ParkingAndDateRelationship>().HasOne(x => x.AssignedToUser).WithMany(x => x.AssignedToUserInSpecificDate).HasForeignKey(y => y.AssignedToUserID).OnDelete(DeleteBehavior.Restrict);

        }

        private void Seeding(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedAssignedParkings();
            modelBuilder.SeedParkings();
            modelBuilder.SeedUserWithNoParking();
            modelBuilder.SeedRoles();
        }

        public DbSet<ParkingDetailsOnDate> ParkingDetailsOnDate { get; set; }

        public DbSet<ParkingSpot> Parkings { get; set; }

        public DbSet<ParkingAndDateRelationship> ParkingAndDateRelationship { get; set; }
    }
}


