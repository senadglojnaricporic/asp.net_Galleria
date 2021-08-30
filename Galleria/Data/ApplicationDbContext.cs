using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Galleria.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public IConfiguration Configuration { get; }

        public ApplicationDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            var adminRoleName = "Admin";
            var adminId = Configuration["Galleria:AdminId"];
            var adminEmail = Configuration["Galleria:AdminEmail"];
            var adminPassword = Configuration["Galleria:AdminPassword"];

            var adminRole = new IdentityRole { Name = adminRoleName, NormalizedName = adminRoleName.ToUpper() };
            modelBuilder.Entity<IdentityRole>().HasData(adminRole);
            var hasher = new PasswordHasher<IdentityUser>();
            var adminUser = new IdentityUser {
				                    Id = adminId,
                                    UserName = adminEmail, 
                                    Email = adminEmail,
                                    NormalizedEmail = adminEmail.ToUpper(),
                                    NormalizedUserName = adminEmail.ToUpper(),
                                    EmailConfirmed = true
                                    };
            adminUser.PasswordHash = hasher.HashPassword(adminUser, adminPassword);
            modelBuilder.Entity<IdentityUser>().HasData(adminUser);
            var adminUserRole = new IdentityUserRole<string>{
                                            RoleId = adminRole.Id, // for admin username
                                            UserId = adminUser.Id  // for admin role
                                            };
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(adminUserRole);                    
        }
    }
}
