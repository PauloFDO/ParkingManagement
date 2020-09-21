﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Parking.EF;

namespace Parking.EF.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200921160056_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "9740acb2-22e6-4b6d-9ae6-fd6914cbaea2",
                            ConcurrencyStamp = "854985bf-e5c5-45fd-ae34-1b3b72bd3007",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Parking.Entities.ParkingAndDateRelationship", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AssignedToUserID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ParkingDetailsOnDateID")
                        .HasColumnType("int");

                    b.Property<int>("ParkingID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("AssignedToUserID");

                    b.HasIndex("ParkingDetailsOnDateID");

                    b.HasIndex("ParkingID");

                    b.ToTable("ParkingAndDateRelationship");
                });

            modelBuilder.Entity("Parking.Entities.ParkingDetailsOnDate", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("ApplyPartialBooking")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan?>("EndTimeIfPartial")
                        .HasColumnType("time");

                    b.Property<TimeSpan?>("StartTimeIfPartial")
                        .HasColumnType("time");

                    b.HasKey("ID");

                    b.ToTable("ParkingDetailsOnDate");
                });

            modelBuilder.Entity("Parking.Entities.ParkingSpot", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PermanentlyAssignedToUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("SpaceNumber")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("PermanentlyAssignedToUserId")
                        .IsUnique()
                        .HasFilter("[PermanentlyAssignedToUserId] IS NOT NULL");

                    b.ToTable("Parkings");

                    b.HasData(
                        new
                        {
                            ID = 345,
                            PermanentlyAssignedToUserId = "ab449405-701a-4a4e-9d9a-aa7abd4f5d00",
                            SpaceNumber = 345
                        },
                        new
                        {
                            ID = 337,
                            SpaceNumber = 337
                        },
                        new
                        {
                            ID = 336,
                            SpaceNumber = 336
                        },
                        new
                        {
                            ID = 335,
                            SpaceNumber = 335
                        },
                        new
                        {
                            ID = 334,
                            SpaceNumber = 334
                        });
                });

            modelBuilder.Entity("Parking.Entities.User", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.HasDiscriminator().HasValue("User");

                    b.HasData(
                        new
                        {
                            Id = "ab449405-701a-4a4e-9d9a-aa7abd4f5d00",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "67157a1b-f7d9-4eb0-92d8-390f8ab2a089",
                            Email = "userWithAssignedspot@usertest.co.uk",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "USERWITHASSIGNEDSPOT@USERTEST.CO.UK",
                            NormalizedUserName = "USERWITHASSIGNEDSPOT@USERTEST.CO.UK",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "337edac7-0364-4ef1-890a-65814b42d0c6",
                            TwoFactorEnabled = false,
                            UserName = "userWithAssignedspot@usertest.co.uk"
                        },
                        new
                        {
                            Id = "173b44f0-587a-4794-beea-e03a38e252ec",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "1645f3c0-f9ea-41ad-b100-81aa153eec4f",
                            Email = "NormalUserTest@admintest.co.uk",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "NORMALUSERTEST@ADMINTEST.CO.UK",
                            NormalizedUserName = "NORMALUSERTEST@ADMINTEST.CO.UK",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "74d6f7db-5d8d-4316-a38f-f7dcedb1ff8b",
                            TwoFactorEnabled = false,
                            UserName = "NormalUserTest@admintest.co.uk"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Parking.Entities.ParkingAndDateRelationship", b =>
                {
                    b.HasOne("Parking.Entities.User", "AssignedToUser")
                        .WithMany("AssignedToUserInSpecificDate")
                        .HasForeignKey("AssignedToUserID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Parking.Entities.ParkingDetailsOnDate", "ParkingDetailsOnDate")
                        .WithMany("ParkingAndDateRelationship")
                        .HasForeignKey("ParkingDetailsOnDateID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Parking.Entities.ParkingSpot", "ParkingSpot")
                        .WithMany("ParkingAndDateRelationship")
                        .HasForeignKey("ParkingID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Parking.Entities.ParkingSpot", b =>
                {
                    b.HasOne("Parking.Entities.User", "PermanentlyAssignedToUser")
                        .WithOne("UserParking")
                        .HasForeignKey("Parking.Entities.ParkingSpot", "PermanentlyAssignedToUserId");
                });
#pragma warning restore 612, 618
        }
    }
}
