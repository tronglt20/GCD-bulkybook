using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Services.UserAccountMapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ReqAccess> ReqAccess { get; set; }
        public DbSet<Chapter> Chapters { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            this.SeedUsers(modelBuilder);
            this.SeedRoles(modelBuilder);
            this.SeedUserRoles(modelBuilder);
            this.SeedCategory(modelBuilder);

        }
        private void SeedCategory(ModelBuilder modelBuilder)
        {
            Category category1 = new Category()
            {
                Id = 1,
                Name = "Mystery",
            };
            Category category2 = new Category()
            {
                Id = 2,
                Name = "Romance",
            };
            Category category3 = new Category()
            {
                Id = 3,
                Name = "Classic",
            };

            modelBuilder.Entity<Category>().HasData(category1, category2, category3);
        }

        private void SeedUsers(ModelBuilder modelBuilder)
        {
            ApplicationUser admin = new ApplicationUser()
            {
                Id = "9999",
                UserName = "admin@gmail.com",
                NormalizedUserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com",
                LockoutEnabled = false,
            };

            PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
            admin.PasswordHash = ph.HashPassword(admin, "Abc@@123");

            modelBuilder.Entity<ApplicationUser>().HasData( admin);
        }

        private void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "r1", Name = SD.Role_Admin, NormalizedName = SD.Role_Admin },
                new IdentityRole() { Id = "r2", Name = SD.Role_User_Indi, NormalizedName = SD.Role_User_Indi }
                );
        }

        private void SeedUserRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = "r1", UserId = "9999" }
                );
        }
    }
}
