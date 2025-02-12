using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class name_and_id_change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePicUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccountStatus = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "ProfilePicUrl" },
                values: new object[,]
                {
                    { "52694168-6706-4595-bdea-bae0da5923f0", "john.doe.ddd@outlook.com", "John", "Doe", "https://res.cloudinary.com/demo/image/upload/w_400,h_400,c_crop,g_face,r_max/w_200/lady.jpg" },
                    { "5b1aa188-636f-436a-a2da-ae742ddadedf", "admin@patternstech.com", "Patterns", "Tech", "https://res.cloudinary.com/demo/image/upload/w_400,h_400,c_crop,g_face,r_max/w_200/lady.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountNumber", "AccountStatus", "AccountTitle", "CurrentBalance", "UserId" },
                values: new object[,]
                {
                    { "2f115781-c0d2-4f98-a70b-0bc4ed01d780", "0002-2002", 0, "John Doe", 545m, "52694168-6706-4595-bdea-bae0da5923f0" },
                    { "37846734-172e-4149-8cec-6f43d1eb3f60", "0001-1001", 0, "Patterns Tech", 3500m, "5b1aa188-636f-436a-a2da-ae742ddadedf" }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "AccountId", "TransactionAmount", "TransactionDate", "TransactionType" },
                values: new object[,]
                {
                    { "159d454c-e3c5-46a6-81bd-7eae9a4c75b4", "37846734-172e-4149-8cec-6f43d1eb3f60", -100m, new DateTime(2024, 6, 12, 15, 41, 11, 815, DateTimeKind.Local).AddTicks(6299), 1 },
                    { "1898f00c-4ca3-4d2b-b5d6-f8e9a57259c2", "37846734-172e-4149-8cec-6f43d1eb3f60", 900m, new DateTime(2024, 3, 12, 15, 41, 11, 815, DateTimeKind.Local).AddTicks(6323), 0 },
                    { "2aa19b0a-dd32-4f49-a4e3-0107d1ca6099", "37846734-172e-4149-8cec-6f43d1eb3f60", -200m, new DateTime(2024, 10, 12, 15, 41, 11, 815, DateTimeKind.Local).AddTicks(6270), 1 },
                    { "51406b14-67df-4c3d-b0c2-86dc8ec73841", "37846734-172e-4149-8cec-6f43d1eb3f60", -300m, new DateTime(2024, 7, 12, 15, 41, 11, 815, DateTimeKind.Local).AddTicks(6287), 1 },
                    { "9e0cacc6-3fa2-4b88-9549-102bfe4dd960", "37846734-172e-4149-8cec-6f43d1eb3f60", -500m, new DateTime(2024, 4, 12, 15, 41, 11, 815, DateTimeKind.Local).AddTicks(6320), 1 },
                    { "a9090b73-c531-4c71-952a-41b871ad58fd", "37846734-172e-4149-8cec-6f43d1eb3f60", 3000m, new DateTime(2025, 2, 11, 15, 41, 11, 815, DateTimeKind.Local).AddTicks(5565), 0 },
                    { "b3214210-bd11-423e-8ca5-6c6d19cc95d6", "37846734-172e-4149-8cec-6f43d1eb3f60", -500m, new DateTime(2024, 2, 12, 15, 41, 11, 815, DateTimeKind.Local).AddTicks(6204), 1 },
                    { "cfcdd90e-3756-4470-b349-4208e6c535a6", "37846734-172e-4149-8cec-6f43d1eb3f60", 500m, new DateTime(2024, 11, 12, 15, 41, 11, 815, DateTimeKind.Local).AddTicks(6237), 0 },
                    { "dfc9a5a8-0751-4cc2-82c8-ff332def73c7", "37846734-172e-4149-8cec-6f43d1eb3f60", 200m, new DateTime(2024, 8, 12, 15, 41, 11, 815, DateTimeKind.Local).AddTicks(6284), 0 },
                    { "e442cf3e-a110-436c-93a4-7a3849d0429d", "37846734-172e-4149-8cec-6f43d1eb3f60", 1000m, new DateTime(2023, 2, 12, 15, 41, 11, 815, DateTimeKind.Local).AddTicks(6233), 0 },
                    { "e81b121f-4897-4fcd-947f-222e77cc0e6d", "37846734-172e-4149-8cec-6f43d1eb3f60", 500m, new DateTime(2024, 9, 12, 15, 41, 11, 815, DateTimeKind.Local).AddTicks(6274), 0 },
                    { "f57594c9-7e5c-48c6-be1c-42ccaa3aee81", "37846734-172e-4149-8cec-6f43d1eb3f60", 200m, new DateTime(2024, 5, 12, 15, 41, 11, 815, DateTimeKind.Local).AddTicks(6303), 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions",
                column: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
