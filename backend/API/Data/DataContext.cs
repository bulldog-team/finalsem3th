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
        public DbSet<PackageModel> PackageModels { get; set; }
        public DbSet<PackageStatusModel> PackageStatusModels { get; set; }

        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<InvoiceModel> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BranchModel>().HasData(
                new BranchModel
                {
                    BranchId = 1,
                    Address = "114 Đường 9A Khu Dân cư Trung Sơn, Huyện Bình Chánh, Hồ Chí Minh, Việt Nam",
                    BranchName = "HCM"
                },
                new BranchModel
                {
                    BranchId = 2,
                    Address = "Hikari Bình Dương, Thành phố Thủ Dầu Một, Bình Dương, Việt Nam",
                    BranchName = " Binh Duong"
                },
                new BranchModel
                {
                    BranchId = 3,
                    Address = "170 Nguyễn Văn Cừ, Ninh Kiều, Cần Thơ, Việt Nam",
                    BranchName = "Can Tho"
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
                    Gender = GenderType.Male,
                    UserId = 1,
                    Dob = new DateTime(1963, 10, 14),
                    ImgName = "default_img.png"
                },
                new UserInfo
                {
                    Id = 2,
                    Address = "user001 address",
                    BranchId = 1,
                    Gender = GenderType.Male,
                    UserId = 2,
                    Dob = new DateTime(1989, 1, 4),
                    ImgName = "default_img.png"
                },
                new UserInfo
                {
                    Id = 3,
                    Address = "user002 address",
                    BranchId = 2,
                    Gender = GenderType.Female,
                    UserId = 3,
                    Dob = new DateTime(1998, 6, 20),
                    ImgName = "default_img.png"
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
                    Status = "Picked up",
                },
                new PackageStatusModel
                {
                    StatusId = 2,
                    Status = "Sending",
                },
                new PackageStatusModel
                {
                    StatusId = 3,
                    Status = "Received",
                }
            );

            modelBuilder.Entity<PackageModel>().HasData(
                new PackageModel
                {
                    PackageId = 1,
                    DateSent = DateTime.Now,
                    SenderName = "Nguyen Thi Minh Khai",
                    SenderAddress = "249 Lý Thường Kiệt, Phường 15, Quận 11, Thành phố Hồ Chí Minh",
                    ReceiveName = "Nguyen Thi Mai Nam",
                    ReceiveAddress = "175B Cao Thắng, Phường 12, Quận 10, Thành phố Hồ Chí Minh",
                    Distance = 2019,
                    TotalPrice = 20120,
                    Weight = 1,
                    UserId = 2,
                    TypeId = 1,
                    StatusId = 1,
                    IsPaid = true,
                    Pincode = 700000,
                }
            );

            modelBuilder.Entity<InvoiceModel>().HasData(
                new InvoiceModel
                {
                    CreateAt = DateTime.Now,
                    InvoiceId = 1,
                    PackageId = 1,
                    TotalPrice = 20120
                }
            );
            base.OnModelCreating(modelBuilder);
        }

    }
}