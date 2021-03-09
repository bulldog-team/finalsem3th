using System;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<RoleDetailModel> RoleDetailModels;
    public DbSet<UserModel> UserModels;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<RoleDetailModel>().HasData(
      new RoleDetailModel
      {
        RoleId = Guid.Parse("a8ab1dd4-8010-11eb-9439-0242ac130002"),
        RoleName = "Admin"
      },
      new RoleDetailModel
      {
        RoleId = Guid.Parse("b90d1426-8014-11eb-9439-0242ac130002"),
        RoleName = "Admin"
      }
      );

      modelBuilder.Entity<UserModel>().HasData(
      new UserModel
      {
        UserId = Guid.Parse("ebf20dba-8014-11eb-9439-0242ac130002"),
        Username = "admin001",

        Password = new PasswordHasher<object>().HashPassword(null, "admin001"),
        RoleId = Guid.Parse("a8ab1dd4-8010-11eb-9439-0242ac130002")
      },
      new UserModel
      {
        UserId = Guid.Parse("3b25db86-8016-11eb-9439-0242ac130002"),
        Username = "admin002",
        Password = new PasswordHasher<object>().HashPassword(null, "admin002"),
        RoleId = Guid.Parse("a8ab1dd4-8010-11eb-9439-0242ac130002")
      },
      new UserModel
      {
        UserId = Guid.Parse("4cf42c32-8016-11eb-9439-0242ac130002"),
        Username = "user001",
        Password = new PasswordHasher<object>().HashPassword(null, "user001"),
        RoleId = Guid.Parse("b90d1426-8014-11eb-9439-0242ac130002")
      }
      );
      base.OnModelCreating(modelBuilder);
    }

  }
}