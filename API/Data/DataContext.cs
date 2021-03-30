using System;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<RoleDetailModel> RoleDetailModels { get; set; }
        public DbSet<UserModel> UserModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserModel>().HasData(
            new UserModel
            {
                UserId = 1,
                Username = "admin001",
                Email = "admin001@mail.com",
                Password = new PasswordHasher<object>().HashPassword(null, "admin001"),
            },
            new UserModel
            {
                UserId = 2,
                Username = "user001",
                Email = "user001@mail.com",
                Password = new PasswordHasher<object>().HashPassword(null, "user001"),
            },
            new UserModel
            {
                UserId = 3,
                Username = "cus001",
                Email = "cus001@mail.com",
                Password = new PasswordHasher<object>().HashPassword(null, "cus001"),
            }
            );

            modelBuilder.Entity<RoleModel>().HasData(
                new RoleModel
                {
                    RoleId = 1,
                    RoleName = "Admin"
                },
                 new RoleModel
                 {
                     RoleId = 2,
                     RoleName = "User"
                 },
                 new RoleModel
                 {
                     RoleId = 3,
                     RoleName = "Customer"
                 }
            );

            modelBuilder.Entity<RoleDetailModel>().HasData(
            new RoleDetailModel
            {
                Id = 1,
                RoleId = 1,
                UserId = 1,
            },
            new RoleDetailModel
            {
                Id = 2,
                RoleId = 2,
                UserId = 2,
            },
            new RoleDetailModel
            {
                Id = 3,
                RoleId = 3,
                UserId = 3,
            }
            );
            base.OnModelCreating(modelBuilder);
        }

    }
}