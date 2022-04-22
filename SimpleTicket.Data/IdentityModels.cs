using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SimpleTicket.Data
{
    // .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  .----------------. 
    //| .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. |
    //| |   ______     | || |     ______   | || |    _______   | || |  _________   | || |  _______     | || | ____ ____    | || |     _____    | || |     ______   | || |  _________   | |
    //| |  |_ __   \   | || |   .' ___  |  | || |   /  ___  |  | || | |_   ___  |  | || | |_   __ \    | || ||_  _| |_  _| | || |    |_   _|   | || |   .' ___  |  | || | |_ ___  |  | |
    //| |    | |__) |  | || |  / .'   \_|  | || |  |  (__ \_|  | || |   | |_  \_|  | || |   | |__) |   | || |  \ \   / /   | || |      | |     | || |  / .'   \_|  | || |   | |_  \_|  | |
    //| |    |  ___/   | || |  | |         | || |   '.___`-.   | || |   |  _|  _   | || |   |  __ /    | || |   \ \ / /    | || |      | |     | || |  | |         | || |   |  _|  _   | |
    //| |   _| |_      | || |  \ `.___.'\  | || |  |`\____) |  | || |  _| |___/ |  | || |  _| |  \ \_  | || |    \ ' /     | || |     _| |_    | || |  \ `.___.'\  | || |  _| |___/ |  | |
    //| |  |_____|     | || |   `._____.'  | || |  |_______.'  | || | |_________|  | || | |____| |___| | || |     \_/      | || |    |_____|   | || |   `._____.'  | || | |_________|  | |
    //| |              | || |              | || |              | || |              | || |              | || |              | || |              | || |              | || |              | |
    //| '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' |
    // '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------' 

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
          
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            
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
        

        public DbSet<Ticket> Tickets { get; set; } 
        public DbSet<Customer> Customers { get; set; } 
        public DbSet<Note> Notes { get; set; } 
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Conventions
                .Remove<PluralizingTableNameConvention>();

            modelBuilder
                .Configurations
                .Add(new IdentityUserLoginConfiguration())
                .Add(new IdentityUserRoleConfiguration());
        }

    }
    public class IdentityUserLoginConfiguration : EntityTypeConfiguration<IdentityUserLogin>
    {
        public IdentityUserLoginConfiguration()
        {
            HasKey(iul => iul.UserId);
        }
    }
    public class IdentityUserRoleConfiguration : EntityTypeConfiguration<IdentityUserRole>
    {
        public IdentityUserRoleConfiguration()
        {
            HasKey(iur => iur.UserId);
        }
    }
}