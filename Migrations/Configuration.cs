using System.Web.WebPages;
using CampSiteC3.Models;
 using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
namespace CampSiteC3.Migrations
{


    internal sealed class Configuration : DbMigrationsConfiguration<CampSiteC3.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
            ContextKey = "CampSiteC3.Models.ApplicationDbContext";
        }
        protected override void Seed(CampSiteC3.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //create admin user
            var passwordHash = new PasswordHasher();
            string password = passwordHash.HashPassword("PassWord0-");
            context.Users.AddOrUpdate(u => u.UserName,
                new ApplicationUser
                {
                    Id = "1",
                    UserName = "admin@admin.nl",
                    Hometown = "Amsterdam",
                    PasswordHash = password,
                    Email = "admin@admin.nl",
                    SecurityStamp = "222"
                });


            //seed accomodations and reservations
            for (int i = 1; i <= 38; i++)
            {
                string nr;
                if (i < 10)
                {
                    nr = "0" + i;
                }
                else
                {
                    nr = i + "";
                }

                context.Reservations.Add(new Reservation()
                {
                    StartDate = DateTime.Now.AddDays(5),
                    EndDate = new DateTime(2021, 6, 15, 00, 00, 00),
                    Accomodation_id = i,
                    UserId = "1",
                    NumberOfAdults = 1,
                    NumberOfKids = i % 3,
                    NumberOfAnimals = i / 9,
                    DescriptionAnimals = "succes"
                });

                context.Reservations.AddOrUpdate(new Reservation()
                {
                    StartDate = new DateTime(2021, 1, 1, 00, 00, 00),
                    EndDate = new DateTime(2021, 4 * i % 12 + 1, 15, 00, 00, 00),
                    Accomodation_id = i,
                    UserId = "1",
                    NumberOfAdults = 2,
                    NumberOfKids = 0,
                    NumberOfAnimals = 0,
                    DescriptionAnimals = "fd"
                });

                context.Reservations.AddOrUpdate(new Reservation()
                {
                    StartDate = new DateTime(2021, 2, 2, 00, 00, 00),
                    EndDate = new DateTime(2021, ((i + 2) * 10) % 12 + 1, (i / 3) + 1, 00, 00, 00),
                    Accomodation_id = 39 - i,
                    UserId = "1",
                    NumberOfAdults = i / 5 + 1,
                    NumberOfKids = 2,
                    NumberOfAnimals = 0,
                    DescriptionAnimals = "fad"
                });

                context.Reservations.Add(new Reservation()
                {
                    StartDate = new DateTime(2021, 6, 2, 00, 00, 00),
                    EndDate = new DateTime(2021, 6, 3, 00, 00, 00),
                    Accomodation_id = i,
                    UserId = "1",
                    NumberOfAdults = i / 5 + 1,
                    NumberOfKids = 2,
                    NumberOfAnimals = 0,
                    DescriptionAnimals = "test"
                });

                context.Reservations.Add(new Reservation()
                {
                    StartDate = new DateTime(2021, 8, 2, 00, 00, 00),
                    EndDate = new DateTime(2021, 8, 14, 00, 00, 00),
                    Accomodation_id = i,
                    UserId = "1",
                    NumberOfAdults = i / 5 + 1,
                    NumberOfKids = 2,
                    NumberOfAnimals = 0,
                    DescriptionAnimals = "test"
                });

                //context.Accomodations.AddOrUpdate(new Accomodation()
                //{
                //    id = Guid.NewGuid(),
                //    Number = nr
                //});
            }
            //    context.Roles.AddOrUpdate(
            //    new IdentityRole { Id = "1", Name = "Admin" });
        }


        }
        
    }


