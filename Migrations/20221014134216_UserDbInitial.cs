using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactListWebService.Migrations
{
    public partial class UserDbInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Surname = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    BirthDate = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryType = table.Column<int>(type: "INTEGER", nullable: false),
                    Subcategory = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "BirthDate", "Email", "Name", "Password", "Phone", "Surname" },
                values: new object[] { 1, "1990-12-1", "smith@onet.pl", "Wilson", "CfDJ8NexdWr8pwVLtd6Bq9KrAxHnB1yxocpHLyp58_sYx6SnpDu5-h13HgDtfA4TXvO_5YesDkq1i9KvOIu6sxNgQwW4jpPjClzPKt_0ayP8-LMzmjAe9mlhP7AIZMEQo9TSWg", "504-555-555", "Smith" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "BirthDate", "Email", "Name", "Password", "Phone", "Surname" },
                values: new object[] { 2, "2001-12-1", "jk@poczta.com", "Adam", "CfDJ8NexdWr8pwVLtd6Bq9KrAxHnB1yxocpHLyp58_sYx6SnpDu5-h13HgDtfA4TXvO_5YesDkq1i9KvOIu6sxNgQwW4jpPjClzPKt_0ayP8-LMzmjAe9mlhP7AIZMEQo9TSWg", "888-334-333", "Kowalski" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "BirthDate", "Email", "Name", "Password", "Phone", "Surname" },
                values: new object[] { 3, "2020-01-1", "jn2000@gmail.com", "Jan", "CfDJ8NexdWr8pwVLtd6Bq9KrAxHnB1yxocpHLyp58_sYx6SnpDu5-h13HgDtfA4TXvO_5YesDkq1i9KvOIu6sxNgQwW4jpPjClzPKt_0ayP8-LMzmjAe9mlhP7AIZMEQo9TSWg", "111-444-333", "Niezbedny" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryType", "Subcategory", "UserId" },
                values: new object[] { 1, 0, "", 1 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryType", "Subcategory", "UserId" },
                values: new object[] { 2, 1, "", 2 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryType", "Subcategory", "UserId" },
                values: new object[] { 3, 2, "Other than other", 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UserId",
                table: "Categories",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
