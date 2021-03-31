using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryModel",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryModel", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "RoleModels",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleModels", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "UserModels",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModels", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "BookModel",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookModel", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_BookModel_CategoryModel_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CategoryModel",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleDetailModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleDetailModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleDetailModels_RoleModels_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleModels",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleDetailModels_UserModels_UserId",
                        column: x => x.UserId,
                        principalTable: "UserModels",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CategoryModel",
                columns: new[] { "CategoryId", "Category" },
                values: new object[,]
                {
                    { 1, "Comic" },
                    { 2, "Programinh" },
                    { 3, "Fantasy" },
                    { 4, "Romance" }
                });

            migrationBuilder.InsertData(
                table: "RoleModels",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" },
                    { 3, "Customer" }
                });

            migrationBuilder.InsertData(
                table: "UserModels",
                columns: new[] { "UserId", "Email", "Password", "Username" },
                values: new object[,]
                {
                    { 1, "admin001@mail.com", "AQAAAAEAACcQAAAAECE7RyR8szfW6QRSN2NCZ+t4imcCKeliBiF8HrBD6pAynk8R/wW5lyv4go0Rr/gsjQ==", "admin001" },
                    { 2, "user001@mail.com", "AQAAAAEAACcQAAAAED1UGiViG0ViXnvbYB78Q/ZzxhTmzKSHPrPweanFIkxQ/bvneHTdQLqpxGT+h0P3kQ==", "user001" },
                    { 3, "cus001@mail.com", "AQAAAAEAACcQAAAAEI+crJMN3vCXssNt0x3j+f9Ngvv/iBn7boZZWBxDKrCpX6AeJy37lGPatrqRFv7DQg==", "cus001" }
                });

            migrationBuilder.InsertData(
                table: "BookModel",
                columns: new[] { "BookId", "Author", "BookName", "CategoryId", "Description", "thumbnail" },
                values: new object[,]
                {
                    { 1, "author 1", "Book 1", 1, "Description 1", "https://randomuser.me/api/portraits/men/4.jpg" },
                    { 2, "author 2", "Book 2", 1, "Description 2", "https://randomuser.me/api/portraits/men/5.jpg" },
                    { 3, "author 3", "Book 3", 1, "Description 3", "https://randomuser.me/api/portraits/men/6.jpg" },
                    { 4, "author 4", "Book 4", 1, "Description 4", "https://randomuser.me/api/portraits/men/7.jpg" },
                    { 7, "author 7", "Book 7", 1, "Description 7", "https://randomuser.me/api/portraits/men/10.jpg" },
                    { 5, "author 5", "Book 5", 2, "Description 5", "https://randomuser.me/api/portraits/men/8.jpg" },
                    { 6, "author 6", "Book 6", 2, "Description 6", "https://randomuser.me/api/portraits/men/9.jpg" },
                    { 8, "author 8", "Book 8", 2, "Description 8", "https://randomuser.me/api/portraits/men/11.jpg" },
                    { 9, "author 9", "Book 9", 3, "Description 1", "https://randomuser.me/api/portraits/men/12.jpg" },
                    { 10, "author 10", "Book 10", 3, "Description 10", "https://randomuser.me/api/portraits/men/13.jpg" },
                    { 11, "author 11", "Book 11", 3, "Description 11", "https://randomuser.me/api/portraits/men/14.jpg" }
                });

            migrationBuilder.InsertData(
                table: "RoleDetailModels",
                columns: new[] { "Id", "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 2, 2 },
                    { 4, 3, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookModel_CategoryId",
                table: "BookModel",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleDetailModels_RoleId",
                table: "RoleDetailModels",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleDetailModels_UserId",
                table: "RoleDetailModels",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookModel");

            migrationBuilder.DropTable(
                name: "RoleDetailModels");

            migrationBuilder.DropTable(
                name: "CategoryModel");

            migrationBuilder.DropTable(
                name: "RoleModels");

            migrationBuilder.DropTable(
                name: "UserModels");
        }
    }
}
