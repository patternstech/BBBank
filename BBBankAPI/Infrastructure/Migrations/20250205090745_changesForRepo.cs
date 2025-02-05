using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changesForRepo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "1e3731db-c89a-4d27-8efb-09e60b72edf6");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "28c51fa0-f0ec-4b74-b4f6-da8d5cbeeed6");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "4400c28a-a005-4786-87a5-3c7fa64c2b6d");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "46d0e450-6301-4d49-bf3b-d09f59e7b158");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "51792daf-628f-4304-8f0d-016f011cc391");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "5b1bf3b8-f700-4974-bd84-c0a133ff6435");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "61cf7d1b-5e5c-412e-b7d7-ca68fc04140d");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "88090472-7798-4231-b4cf-fa64047a8dc6");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "891ab004-d8ce-400f-8fcd-142e30ecb140");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "bf3d41ae-a020-4ac6-a309-970b76854014");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "d28816e1-bb92-4ed4-b8d9-4b24aaf0eef5");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "f778816e-bc02-4b62-9c61-7b76e1e5ca33");

            migrationBuilder.AlterColumn<string>(
                name: "ProfilePicUrl",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "AccountId", "TransactionAmount", "TransactionDate", "TransactionType" },
                values: new object[,]
                {
                    { "357300c6-e658-4a6e-98a3-ce5590d2be4d", "37846734-172e-4149-8cec-6f43d1eb3f60", -500m, new DateTime(2024, 4, 5, 3, 7, 44, 693, DateTimeKind.Local).AddTicks(4625), 1 },
                    { "60f52cf1-5fcc-4111-b4da-0daf04fdc827", "37846734-172e-4149-8cec-6f43d1eb3f60", -100m, new DateTime(2024, 6, 5, 3, 7, 44, 693, DateTimeKind.Local).AddTicks(4618), 1 },
                    { "615eaaf8-4acb-4d4a-a6b1-ecf54236bf80", "37846734-172e-4149-8cec-6f43d1eb3f60", 1000m, new DateTime(2023, 2, 5, 3, 7, 44, 693, DateTimeKind.Local).AddTicks(4465), 0 },
                    { "65e568f5-005f-43c2-a22b-6049c28c30e0", "37846734-172e-4149-8cec-6f43d1eb3f60", 3000m, new DateTime(2025, 2, 4, 3, 7, 44, 691, DateTimeKind.Local).AddTicks(7852), 0 },
                    { "87bda3b2-832f-4ad0-b299-f11e1e5cba89", "37846734-172e-4149-8cec-6f43d1eb3f60", 500m, new DateTime(2024, 11, 5, 3, 7, 44, 693, DateTimeKind.Local).AddTicks(4469), 0 },
                    { "9b7a36f5-218a-4a18-a1fb-b468aa70666b", "37846734-172e-4149-8cec-6f43d1eb3f60", 200m, new DateTime(2024, 8, 5, 3, 7, 44, 693, DateTimeKind.Local).AddTicks(4613), 0 },
                    { "a1594ec9-dd50-495f-99d0-fe662fbf000d", "37846734-172e-4149-8cec-6f43d1eb3f60", 900m, new DateTime(2024, 3, 5, 3, 7, 44, 693, DateTimeKind.Local).AddTicks(4628), 0 },
                    { "bda769d8-e8bb-46d8-8f29-bc38f92fc27f", "37846734-172e-4149-8cec-6f43d1eb3f60", -300m, new DateTime(2024, 7, 5, 3, 7, 44, 693, DateTimeKind.Local).AddTicks(4616), 1 },
                    { "d30e2e33-cd14-457d-923a-bf1a18430a15", "37846734-172e-4149-8cec-6f43d1eb3f60", -200m, new DateTime(2024, 10, 5, 3, 7, 44, 693, DateTimeKind.Local).AddTicks(4607), 1 },
                    { "d3d2e4aa-cd73-470b-980d-b9dd816b9548", "37846734-172e-4149-8cec-6f43d1eb3f60", 200m, new DateTime(2024, 5, 5, 3, 7, 44, 693, DateTimeKind.Local).AddTicks(4621), 0 },
                    { "ddbbeea3-b931-4910-b3d1-e9a6f3a4310c", "37846734-172e-4149-8cec-6f43d1eb3f60", 500m, new DateTime(2024, 9, 5, 3, 7, 44, 693, DateTimeKind.Local).AddTicks(4611), 0 },
                    { "f145b7c7-45e7-47f9-9cf6-919431988336", "37846734-172e-4149-8cec-6f43d1eb3f60", -500m, new DateTime(2024, 2, 5, 3, 7, 44, 693, DateTimeKind.Local).AddTicks(4429), 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "357300c6-e658-4a6e-98a3-ce5590d2be4d");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "60f52cf1-5fcc-4111-b4da-0daf04fdc827");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "615eaaf8-4acb-4d4a-a6b1-ecf54236bf80");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "65e568f5-005f-43c2-a22b-6049c28c30e0");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "87bda3b2-832f-4ad0-b299-f11e1e5cba89");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "9b7a36f5-218a-4a18-a1fb-b468aa70666b");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "a1594ec9-dd50-495f-99d0-fe662fbf000d");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "bda769d8-e8bb-46d8-8f29-bc38f92fc27f");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "d30e2e33-cd14-457d-923a-bf1a18430a15");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "d3d2e4aa-cd73-470b-980d-b9dd816b9548");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "ddbbeea3-b931-4910-b3d1-e9a6f3a4310c");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "f145b7c7-45e7-47f9-9cf6-919431988336");

            migrationBuilder.AlterColumn<string>(
                name: "ProfilePicUrl",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
        }
    }
}
