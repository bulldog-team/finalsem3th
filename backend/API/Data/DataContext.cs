using System;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<BranchModel> BranchModels { get; set; }
        public DbSet<RoleDetailModel> RoleDetailModels { get; set; }
        public DbSet<UserModel> UserModels { get; set; }
        public DbSet<RoleModel> RoleModels { get; set; }
        public DbSet<DeliveryTypeModel> DeliveryTypeModels { get; set; }
        public DbSet<PackageModel> packageModels { get; set; }
        public DbSet<PackageStatusModel> PackageStatusModels { get; set; }
        public DbSet<UserInfoTemp> UserInfoTemps { get; set; }

        public DbSet<UserInfo> UserInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BranchModel>().HasData(
                new BranchModel
                {
                    BranchId = 1,
                    Address = "HCM",
                },
                new BranchModel
                {
                    BranchId = 2,
                    Address = "HN",
                },
                new BranchModel
                {
                    BranchId = 3,
                    Address = "HP",
                }
            );

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
                Password = new PasswordHasher<object>().HashPassword(null, "user001")
            },
            new UserModel
            {
                UserId = 3,
                Username = "user002",
                Email = "user002@mail.com",
                Password = new PasswordHasher<object>().HashPassword(null, "user002")
            }
            );

            modelBuilder.Entity<UserInfo>().HasData(
                new UserInfo
                {
                    Id = 1,
                    Address = "admin address",
                    BranchId = 1,
                    Gender = true,
                    UserId = 1,
                },
                new UserInfo
                {
                    Id = 2,
                    Address = "user001 address",
                    BranchId = 1,
                    Gender = false,
                    UserId = 2,
                },
                new UserInfo
                {
                    Id = 3,
                    Address = "user002 address",
                    BranchId = 2,
                    Gender = true,
                    UserId = 3
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
                    RoleId = 2,
                    UserId = 3,
                }
            );

            modelBuilder.Entity<DeliveryTypeModel>().HasData(
                new DeliveryTypeModel
                {
                    TypeId = 1,
                    TypeName = "Courier",
                    UnitPrice = 10,
                },
                new DeliveryTypeModel
                {
                    TypeId = 2,
                    TypeName = "Speed Post",
                    UnitPrice = 20,
                },
                new DeliveryTypeModel
                {
                    TypeId = 3,
                    TypeName = "Normal Post",
                    UnitPrice = 30,
                },
                new DeliveryTypeModel
                {
                    TypeId = 4,
                    TypeName = "VPP",
                    UnitPrice = 40,
                },
                new DeliveryTypeModel
                {
                    TypeId = 5,
                    TypeName = "Money Order",
                    UnitPrice = 50,
                }
            );

            modelBuilder.Entity<PackageStatusModel>().HasData(
                new PackageStatusModel
                {
                    StatusId = 1,
                    Status = "Waiting to send",
                },
                new PackageStatusModel
                {
                    StatusId = 2,
                    Status = "Sending",
                },
                new PackageStatusModel
                {
                    StatusId = 3,
                    Status = "Receive",
                }
            );
            base.OnModelCreating(modelBuilder);
        }

    }
}