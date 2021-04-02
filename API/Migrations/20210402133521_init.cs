using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categoryModels",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categoryModels", x => x.CategoryId);
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
                name: "BookModels",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThumnailId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookModels", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_BookModels_categoryModels_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "categoryModels",
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
                    { 1, "admin001@mail.com", "AQAAAAEAACcQAAAAELfT7CRM59XUd9e8oJGZMdeRJEiEqDFXUQ+iXvPSli08f1nNXGNCEpx/jq5tNNd/cQ==", "admin001" },
                    { 2, "user001@mail.com", "AQAAAAEAACcQAAAAEG1zJT8DCR/Kg88mohP62oTQZct4m71Ez68YYqbYpXUr9H6vFLz/umLePbZMXLiKRQ==", "user001" },
                    { 3, "cus001@mail.com", "AQAAAAEAACcQAAAAEGmO99cYAn33DKDytugtxo1YBtDtXz1E4AsMVHyK9CDzmQ5pPK7m6buGETld29RTwg==", "cus001" }
                });

            migrationBuilder.InsertData(
                table: "categoryModels",
                columns: new[] { "CategoryId", "Category" },
                values: new object[,]
                {
                    { 1, "Comic" },
                    { 2, "Programing" },
                    { 3, "Fantasy" },
                    { 4, "Romance" }
                });

            migrationBuilder.InsertData(
                table: "BookModels",
                columns: new[] { "BookId", "Author", "BookName", "CategoryId", "Description", "Thumbnail", "ThumnailId" },
                values: new object[,]
                {
                    { 1, "author 1", "Book 1", 1, "Description 1", "https://randomuser.me/api/portraits/men/4.jpg", null },
                    { 2, "author 2", "Book 2", 1, "Description 2", "https://randomuser.me/api/portraits/men/5.jpg", null },
                    { 3, "author 3", "Book 3", 1, "Description 3", "https://randomuser.me/api/portraits/men/6.jpg", null },
                    { 4, "author 4", "Book 4", 1, "Description 4", "https://randomuser.me/api/portraits/men/7.jpg", null },
                    { 7, "author 7", "Book 7", 1, "Description 7", "https://randomuser.me/api/portraits/men/10.jpg", null },
                    { 5, "author 5", "Book 5", 2, "Description 5", "https://randomuser.me/api/portraits/men/8.jpg", null },
                    { 6, "author 6", "Book 6", 2, "Description 6", "https://randomuser.me/api/portraits/men/9.jpg", null },
                    { 8, "author 8", "Book 8", 2, "Description 8", "https://randomuser.me/api/portraits/men/11.jpg", null },
                    { 9, "author 9", "Book 9", 3, "Description 1", "https://randomuser.me/api/portraits/men/12.jpg", null },
                    { 10, "author 10", "Book 10", 3, "Description 10", "https://randomuser.me/api/portraits/men/13.jpg", null },
                    { 11, "author 11", "Book 11", 3, "Description 11", "https://randomuser.me/api/portraits/men/14.jpg", null }
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
                name: "IX_BookModels_CategoryId",
                table: "BookModels",
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
                name: "BookModels");

            migrationBuilder.DropTable(
                name: "RoleDetailModels");

            migrationBuilder.DropTable(
                name: "categoryModels");

            migrationBuilder.DropTable(
                name: "RoleModels");

            migrationBuilder.DropTable(
                name: "UserModels");
        }
    }
}
