using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CampSiteC3.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Hometown { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<CampSiteC3.Models.ForumModels> ForumModels { get; set; }
        public System.Data.Entity.DbSet<CampSiteC3.Models.Accomodation> Accomodations { get; set; }
        public System.Data.Entity.DbSet<CampSiteC3.Models.Reservation> Reservations { get; set; }
        public System.Data.Entity.DbSet<CampSiteC3.Models.AnimatieSchedule> AnimatieSchedules { get; set; }
        public System.Data.Entity.DbSet<CampSiteC3.Models.ActivitiesSchedule> ActivitiesSchedules { get; set; }
    }

    public class SeedRolesAndUsers
    {
        public static void Seed(ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists("Mod"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Mod"));
            }
            if (!roleManager.RoleExists("Admin"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Admin"));
            }

            string userName = "admin@admin.nl";
            string password = "PassWord0-";
            ApplicationUser user = userManager.FindByName(userName);
            if (user == null)
            {
                user = new ApplicationUser()
                {
                    UserName = userName
                };

                IdentityResult userResult = userManager.Create(user, password); 
                if (userResult.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, "Admin");
                }
            }
        }
    }
}