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
        public DbSet<RoleModel> RoleModels { get; set; }

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
                     UserId = 1,
                 },
                new RoleDetailModel
                {
                    Id = 3,
                    RoleId = 2,
                    UserId = 2,
                },
                new RoleDetailModel
                {
                    Id = 4,
                    RoleId = 3,
                    UserId = 3,
                }
            );

            modelBuilder.Entity<CategoryModel>().HasData(
                new CategoryModel
                {
                    Category = "Comic",
                    CategoryId = 1,
                },
                new CategoryModel
                {
                    Category = "Programinh",
                    CategoryId = 2,
                },
                new CategoryModel
                {
                    Category = "Fantasy",
                    CategoryId = 3,
                },
                new CategoryModel
                {
                    Category = "Romance",
                    CategoryId = 4,
                }
            );

            modelBuilder.Entity<BookModel>().HasData(
                new BookModel
                {
                    BookId = 1,
                    Author = "author 1",
                    BookName = "Book 1",
                    CategoryId = 1,
                    Description = "Description 1",
                    thumbnail = "https://randomuser.me/api/portraits/men/4.jpg"
                },
                new BookModel
                {
                    BookId = 2,
                    Author = "author 2",
                    BookName = "Book 2",
                    CategoryId = 1,
                    Description = "Description 2",
                    thumbnail = "https://randomuser.me/api/portraits/men/5.jpg"
                },
                new BookModel
                {
                    BookId = 3,
                    Author = "author 3",
                    BookName = "Book 3",
                    CategoryId = 1,
                    Description = "Description 3",
                    thumbnail = "https://randomuser.me/api/portraits/men/6.jpg"
                },
                new BookModel
                {
                    BookId = 4,
                    Author = "author 4",
                    BookName = "Book 4",
                    CategoryId = 1,
                    Description = "Description 4",
                    thumbnail = "https://randomuser.me/api/portraits/men/7.jpg"
                },
                new BookModel
                {
                    BookId = 5,
                    Author = "author 5",
                    BookName = "Book 5",
                    CategoryId = 2,
                    Description = "Description 5",
                    thumbnail = "https://randomuser.me/api/portraits/men/8.jpg"
                },
                new BookModel
                {
                    BookId = 6,
                    Author = "author 6",
                    BookName = "Book 6",
                    CategoryId = 2,
                    Description = "Description 6",
                    thumbnail = "https://randomuser.me/api/portraits/men/9.jpg"
                },
                new BookModel
                {
                    BookId = 7,
                    Author = "author 7",
                    BookName = "Book 7",
                    CategoryId = 1,
                    Description = "Description 7",
                    thumbnail = "https://randomuser.me/api/portraits/men/10.jpg"
                },
                new BookModel
                {
                    BookId = 8,
                    Author = "author 8",
                    BookName = "Book 8",
                    CategoryId = 2,
                    Description = "Description 8",
                    thumbnail = "https://randomuser.me/api/portraits/men/11.jpg"
                },
                new BookModel
                {
                    BookId = 9,
                    Author = "author 9",
                    BookName = "Book 9",
                    CategoryId = 3,
                    Description = "Description 1",
                    thumbnail = "https://randomuser.me/api/portraits/men/12.jpg"
                },
                new BookModel
                {
                    BookId = 10,
                    Author = "author 10",
                    BookName = "Book 10",
                    CategoryId = 3,
                    Description = "Description 10",
                    thumbnail = "https://randomuser.me/api/portraits/men/13.jpg"
                },
                new BookModel
                {
                    BookId = 11,
                    Author = "author 11",
                    BookName = "Book 11",
                    CategoryId = 3,
                    Description = "Description 11",
                    thumbnail = "https://randomuser.me/api/portraits/men/14.jpg"
                }
            );
            base.OnModelCreating(modelBuilder);
        }

    }
}