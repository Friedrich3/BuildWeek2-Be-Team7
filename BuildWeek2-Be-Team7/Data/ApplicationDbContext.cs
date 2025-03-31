using System;
using BuildWeek2_Be_Team7.Models;
using BuildWeek2_Be_Team7.Models.Animali;
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
        public DbSet<Order> Orders { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Drawer> Drawers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProd> OrderProducts { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Hospitalization> Hospitalizations { get; set; }
        public DbSet<MedicalExam> MedicalExams { get; set; }
        public DbSet<Client> Clients { get; set; }

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

            modelBuilder.Entity<Race>().HasData(
                new Race() { RaceId = 1, Name = "Cane" },
                new Race() { RaceId = 2, Name = "Gatto" },
                new Race() { RaceId = 3, Name = "Coniglio" },
                new Race() { RaceId = 4, Name = "Animali esotici" }
                );

            modelBuilder.Entity<Company>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Category>().HasData(
                new Category() { Id = 1, Name = "Cibo secco" },
                new Category() { Id = 2, Name = "Cibo umido" },
                new Category() { Id = 3, Name = "Integratori" },
                new Category() { Id = 4, Name = "Antibiotici" },
                new Category() { Id = 5, Name = "Analgesici" },
                new Category() { Id = 6, Name = "Antiparassitari" },
                new Category() { Id = 7, Name = "Vaccini" },
                new Category() { Id = 8, Name = "Sistema Cardiovascolare" },
                new Category() { Id = 9, Name = "Sistema Respiratorio" },
                new Category() { Id = 10, Name = "Sistema Gastrointestinale" },
                new Category() { Id = 11, Name = "Oftalmici" },
                new Category() { Id = 12, Name = "Dermatologici" }
                );

            modelBuilder.Entity<Drawer>().HasData(
                new Drawer() { Id = 1, Name = "Alta rotazione", Position = "Center left" },
                new Drawer() { Id = 2, Name = "Emergenza", Position = "Top left" },
                new Drawer() { Id = 3, Name = "Uso Comune", Position = "Center right" },
                new Drawer() { Id = 4, Name = "Specialistici", Position = "Bottom left" },
                new Drawer() { Id = 5, Name = "Refrigerati", Position = "Bottom right" },
                new Drawer() { Id = 6, Name = "Nutraceutici", Position = "Top right" }
                );
            modelBuilder.Entity<MedicalExam>().HasOne(m => m.Pet).WithMany(me => me.MedicalExams).HasForeignKey(me => me.PetId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Client>().HasIndex(p => p.CodiceFiscale).IsUnique();
            modelBuilder.Entity<Pet>().HasIndex(p => p.Microchip).IsUnique();
            
        }
    }
}
