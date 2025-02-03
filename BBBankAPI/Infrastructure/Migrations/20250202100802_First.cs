using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePicUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    { "aa45e3c9-261d-41fe-a1b0-5b4dcf79cfd3", "rassmasood@hotmail.com", "Raas", "Masood", "https://res.cloudinary.com/demo/image/upload/w_400,h_400,c_crop,g_face,r_max/w_200/lady.jpg" },
                    { "c651e237-102a-4de1-8c5a-d41c94079ff0", "salman-dev@outlook.com", "Salman", "Taj", "https://res.cloudinary.com/demo/image/upload/w_400,h_400,c_crop,g_face,r_max/w_200/lady.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountNumber", "AccountStatus", "AccountTitle", "CurrentBalance", "UserId" },
                values: new object[,]
                {
                    { "2f115781-c0d2-4f98-a70b-0bc4ed01d780", "0002-2002", 0, "Salman Taj", 545m, "c651e237-102a-4de1-8c5a-d41c94079ff0" },
                    { "37846734-172e-4149-8cec-6f43d1eb3f60", "0001-1001", 0, "Raas Masood", 3500m, "aa45e3c9-261d-41fe-a1b0-5b4dcf79cfd3" }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "AccountId", "TransactionAmount", "TransactionDate", "TransactionType" },
                values: new object[,]
                {
                    { "1e3731db-c89a-4d27-8efb-09e60b72edf6", "37846734-172e-4149-8cec-6f43d1eb3f60", -300m, new DateTime(2024, 7, 2, 4, 8, 1, 794, DateTimeKind.Local).AddTicks(5940), 1 },
                    { "28c51fa0-f0ec-4b74-b4f6-da8d5cbeeed6", "37846734-172e-4149-8cec-6f43d1eb3f60", 3000m, new DateTime(2025, 2, 1, 4, 8, 1, 793, DateTimeKind.Local).AddTicks(50), 0 },
                    { "4400c28a-a005-4786-87a5-3c7fa64c2b6d", "37846734-172e-4149-8cec-6f43d1eb3f60", -500m, new DateTime(2024, 4, 2, 4, 8, 1, 794, DateTimeKind.Local).AddTicks(5950), 1 },
                    { "46d0e450-6301-4d49-bf3b-d09f59e7b158", "37846734-172e-4149-8cec-6f43d1eb3f60", 500m, new DateTime(2024, 9, 2, 4, 8, 1, 794, DateTimeKind.Local).AddTicks(5935), 0 },
                    { "51792daf-628f-4304-8f0d-016f011cc391", "37846734-172e-4149-8cec-6f43d1eb3f60", 1000m, new DateTime(2023, 2, 2, 4, 8, 1, 794, DateTimeKind.Local).AddTicks(5913), 0 },
                    { "5b1bf3b8-f700-4974-bd84-c0a133ff6435", "37846734-172e-4149-8cec-6f43d1eb3f60", -200m, new DateTime(2024, 10, 2, 4, 8, 1, 794, DateTimeKind.Local).AddTicks(5933), 1 },
                    { "61cf7d1b-5e5c-412e-b7d7-ca68fc04140d", "37846734-172e-4149-8cec-6f43d1eb3f60", -100m, new DateTime(2024, 6, 2, 4, 8, 1, 794, DateTimeKind.Local).AddTicks(5942), 1 },
                    { "88090472-7798-4231-b4cf-fa64047a8dc6", "37846734-172e-4149-8cec-6f43d1eb3f60", -500m, new DateTime(2024, 2, 2, 4, 8, 1, 794, DateTimeKind.Local).AddTicks(5877), 1 },
                    { "891ab004-d8ce-400f-8fcd-142e30ecb140", "37846734-172e-4149-8cec-6f43d1eb3f60", 900m, new DateTime(2024, 3, 2, 4, 8, 1, 794, DateTimeKind.Local).AddTicks(5952), 0 },
                    { "bf3d41ae-a020-4ac6-a309-970b76854014", "37846734-172e-4149-8cec-6f43d1eb3f60", 500m, new DateTime(2024, 11, 2, 4, 8, 1, 794, DateTimeKind.Local).AddTicks(5917), 0 },
                    { "d28816e1-bb92-4ed4-b8d9-4b24aaf0eef5", "37846734-172e-4149-8cec-6f43d1eb3f60", 200m, new DateTime(2024, 8, 2, 4, 8, 1, 794, DateTimeKind.Local).AddTicks(5938), 0 },
                    { "f778816e-bc02-4b62-9c61-7b76e1e5ca33", "37846734-172e-4149-8cec-6f43d1eb3f60", 200m, new DateTime(2024, 5, 2, 4, 8, 1, 794, DateTimeKind.Local).AddTicks(5945), 0 }
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
