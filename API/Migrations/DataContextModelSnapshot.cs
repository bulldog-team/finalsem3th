﻿// <auto-generated />
using BackEnd.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("API.Models.BookModel", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BookName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Thumbnail")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BookId");

                    b.HasIndex("CategoryId");

                    b.ToTable("BookModels");

                    b.HasData(
                        new
                        {
                            BookId = 1,
                            Author = "author 1",
                            BookName = "Book 1",
                            CategoryId = 1,
                            Description = "Description 1",
                            Thumbnail = "https://randomuser.me/api/portraits/men/4.jpg"
                        },
                        new
                        {
                            BookId = 2,
                            Author = "author 2",
                            BookName = "Book 2",
                            CategoryId = 1,
                            Description = "Description 2",
                            Thumbnail = "https://randomuser.me/api/portraits/men/5.jpg"
                        },
                        new
                        {
                            BookId = 3,
                            Author = "author 3",
                            BookName = "Book 3",
                            CategoryId = 1,
                            Description = "Description 3",
                            Thumbnail = "https://randomuser.me/api/portraits/men/6.jpg"
                        },
                        new
                        {
                            BookId = 4,
                            Author = "author 4",
                            BookName = "Book 4",
                            CategoryId = 1,
                            Description = "Description 4",
                            Thumbnail = "https://randomuser.me/api/portraits/men/7.jpg"
                        },
                        new
                        {
                            BookId = 5,
                            Author = "author 5",
                            BookName = "Book 5",
                            CategoryId = 2,
                            Description = "Description 5",
                            Thumbnail = "https://randomuser.me/api/portraits/men/8.jpg"
                        },
                        new
                        {
                            BookId = 6,
                            Author = "author 6",
                            BookName = "Book 6",
                            CategoryId = 2,
                            Description = "Description 6",
                            Thumbnail = "https://randomuser.me/api/portraits/men/9.jpg"
                        },
                        new
                        {
                            BookId = 7,
                            Author = "author 7",
                            BookName = "Book 7",
                            CategoryId = 1,
                            Description = "Description 7",
                            Thumbnail = "https://randomuser.me/api/portraits/men/10.jpg"
                        },
                        new
                        {
                            BookId = 8,
                            Author = "author 8",
                            BookName = "Book 8",
                            CategoryId = 2,
                            Description = "Description 8",
                            Thumbnail = "https://randomuser.me/api/portraits/men/11.jpg"
                        },
                        new
                        {
                            BookId = 9,
                            Author = "author 9",
                            BookName = "Book 9",
                            CategoryId = 3,
                            Description = "Description 1",
                            Thumbnail = "https://randomuser.me/api/portraits/men/12.jpg"
                        },
                        new
                        {
                            BookId = 10,
                            Author = "author 10",
                            BookName = "Book 10",
                            CategoryId = 3,
                            Description = "Description 10",
                            Thumbnail = "https://randomuser.me/api/portraits/men/13.jpg"
                        },
                        new
                        {
                            BookId = 11,
                            Author = "author 11",
                            BookName = "Book 11",
                            CategoryId = 3,
                            Description = "Description 11",
                            Thumbnail = "https://randomuser.me/api/portraits/men/14.jpg"
                        });
                });

            modelBuilder.Entity("API.Models.CategoryModel", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("categoryModels");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            Category = "Comic"
                        },
                        new
                        {
                            CategoryId = 2,
                            Category = "Programing"
                        },
                        new
                        {
                            CategoryId = 3,
                            Category = "Fantasy"
                        },
                        new
                        {
                            CategoryId = 4,
                            Category = "Romance"
                        });
                });

            modelBuilder.Entity("API.Models.RoleDetailModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("RoleDetailModels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoleId = 1,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            RoleId = 2,
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            RoleId = 2,
                            UserId = 2
                        },
                        new
                        {
                            Id = 4,
                            RoleId = 3,
                            UserId = 3
                        });
                });

            modelBuilder.Entity("API.Models.RoleModel", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("RoleModels");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            RoleName = "Admin"
                        },
                        new
                        {
                            RoleId = 2,
                            RoleName = "User"
                        },
                        new
                        {
                            RoleId = 3,
                            RoleName = "Customer"
                        });
                });

            modelBuilder.Entity("API.Models.UserModel", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("UserModels");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Email = "admin001@mail.com",
                            Password = "AQAAAAEAACcQAAAAEGooytMiCP0GRTY4ykCKHcdLNyP9U9+cyp2bdj3rkaWOAkJ864oLukBmpjcPagACYQ==",
                            Username = "admin001"
                        },
                        new
                        {
                            UserId = 2,
                            Email = "user001@mail.com",
                            Password = "AQAAAAEAACcQAAAAENB9EmI7/bwV7m6afI2FZJmsXYiHaIoP1IOm7tT1BFRtQ5eKKsIj6PqFkeQHOqtHhg==",
                            Username = "user001"
                        },
                        new
                        {
                            UserId = 3,
                            Email = "cus001@mail.com",
                            Password = "AQAAAAEAACcQAAAAENYT1yvczxOhiAl4DEkMDmg2CVyuv/LfELcmdXmpumkzRfV16pyqLwwyUAlsO2zqkQ==",
                            Username = "cus001"
                        });
                });

            modelBuilder.Entity("API.Models.BookModel", b =>
                {
                    b.HasOne("API.Models.CategoryModel", "categoryModel")
                        .WithMany("BookModels")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("categoryModel");
                });

            modelBuilder.Entity("API.Models.RoleDetailModel", b =>
                {
                    b.HasOne("API.Models.RoleModel", "RoleModel")
                        .WithMany("RoleDetailModels")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.UserModel", "User")
                        .WithMany("RoleDetailModels")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RoleModel");

                    b.Navigation("User");
                });

            modelBuilder.Entity("API.Models.CategoryModel", b =>
                {
                    b.Navigation("BookModels");
                });

            modelBuilder.Entity("API.Models.RoleModel", b =>
                {
                    b.Navigation("RoleDetailModels");
                });

            modelBuilder.Entity("API.Models.UserModel", b =>
                {
                    b.Navigation("RoleDetailModels");
                });
#pragma warning restore 612, 618
        }
    }
}
