using System;
using BuildWeek2_Be_Team7.Models.Auth;
using BuildWeek2_Be_Team7.Models.Pharmacy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BuildWeek2_Be_Team7.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<Drawer> Drawers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProd> orderProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUserRole>().HasOne(ur => ur.User).WithMany(u => u.ApplicationUserRole).HasForeignKey(ur => ur.UserId);
            modelBuilder.Entity<ApplicationUserRole>().HasOne(ur => ur.Role).WithMany(u => u.ApplicationUserRole).HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<ApplicationUserRole>().Property(p => p.Date).HasDefaultValueSql("GETDATE()").IsRequired(true);
            modelBuilder.Entity<ApplicationRole>().HasData(new ApplicationRole() { Id = "66D7DB2A-56A6-4E65-90A2-26A33701C613", Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = "66D7DB2A-56A6-4E65-90A2-26A33701C613" });
            modelBuilder.Entity<ApplicationRole>().HasData(new ApplicationRole() { Id = "B0B023D5-5C9A-430F-B38F-53D8781BAEA9", Name = "Veterinario", NormalizedName = "VETERINARIO", ConcurrencyStamp = "B0B023D5-5C9A-430F-B38F-53D8781BAEA9" });
            modelBuilder.Entity<ApplicationRole>().HasData(new ApplicationRole() { Id = "91745C18-A978-4202-81D9-BDAC8CB5E2F0", Name = "Farmacista", NormalizedName = "FARMACISTA", ConcurrencyStamp = "91745C18-A978-4202-81D9-BDAC8CB5E2F0" });
            modelBuilder.Entity<ApplicationRole>().HasData(new ApplicationRole() { Id = "6DD52BC5-AC79-4A54-B6CD-302475F6E068", Name = "User", NormalizedName = "USER", ConcurrencyStamp = "6DD52BC5-AC79-4A54-B6CD-302475F6E068" });

            modelBuilder.Entity<ApplicationUser>().HasIndex(p => p.CodiceFiscale).IsUnique();

        }


    }
}
