using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class second_user_transactions_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "159d454c-e3c5-46a6-81bd-7eae9a4c75b4");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "1898f00c-4ca3-4d2b-b5d6-f8e9a57259c2");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "2aa19b0a-dd32-4f49-a4e3-0107d1ca6099");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "51406b14-67df-4c3d-b0c2-86dc8ec73841");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "9e0cacc6-3fa2-4b88-9549-102bfe4dd960");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "a9090b73-c531-4c71-952a-41b871ad58fd");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "b3214210-bd11-423e-8ca5-6c6d19cc95d6");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "cfcdd90e-3756-4470-b349-4208e6c535a6");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "dfc9a5a8-0751-4cc2-82c8-ff332def73c7");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "e442cf3e-a110-436c-93a4-7a3849d0429d");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "e81b121f-4897-4fcd-947f-222e77cc0e6d");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "f57594c9-7e5c-48c6-be1c-42ccaa3aee81");

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "AccountId", "TransactionAmount", "TransactionDate", "TransactionType" },
                values: new object[,]
                {
                    { "06db49d7-d068-4aa2-83f3-3a512aee5a4a", "37846734-172e-4149-8cec-6f43d1eb3f60", 200m, new DateTime(2024, 8, 12, 15, 58, 20, 521, DateTimeKind.Local).AddTicks(8782), 0 },
                    { "1ae030d0-7456-45e2-bd28-ccf8aed1df89", "37846734-172e-4149-8cec-6f43d1eb3f60", 3000m, new DateTime(2025, 2, 11, 15, 58, 20, 521, DateTimeKind.Local).AddTicks(8045), 0 },
                    { "2d37e74c-d148-435b-a187-3ef9ff11533b", "2f115781-c0d2-4f98-a70b-0bc4ed01d780", -200m, new DateTime(2024, 4, 12, 15, 58, 20, 521, DateTimeKind.Local).AddTicks(8883), 1 },
                    { "31bb56d7-cc28-411c-8b23-d472771f2999", "37846734-172e-4149-8cec-6f43d1eb3f60", 200m, new DateTime(2024, 5, 12, 15, 58, 20, 521, DateTimeKind.Local).AddTicks(8814), 0 },
                    { "37803ae3-88c4-48ee-bb63-05cc94ff63a5", "2f115781-c0d2-4f98-a70b-0bc4ed01d780", -200m, new DateTime(2024, 7, 12, 15, 58, 20, 521, DateTimeKind.Local).AddTicks(8870), 1 },
                    { "3cdb3d89-729c-4193-a70f-d449e4312f79", "2f115781-c0d2-4f98-a70b-0bc4ed01d780", -200m, new DateTime(2024, 6, 12, 15, 58, 20, 521, DateTimeKind.Local).AddTicks(8873), 1 },
                    { "42d29091-f1c2-4e18-b5b2-1c4f70a7a2a1", "2f115781-c0d2-4f98-a70b-0bc4ed01d780", -100m, new DateTime(2024, 10, 12, 15, 58, 20, 521, DateTimeKind.Local).AddTicks(8856), 1 },
                    { "45b982a1-3e81-4a43-a0b8-564bbe457e31", "37846734-172e-4149-8cec-6f43d1eb3f60", 500m, new DateTime(2024, 11, 12, 15, 58, 20, 521, DateTimeKind.Local).AddTicks(8700), 0 },
                    { "657e6b13-6289-4ae6-b620-85e57feb73a7", "37846734-172e-4149-8cec-6f43d1eb3f60", -100m, new DateTime(2024, 6, 12, 15, 58, 20, 521, DateTimeKind.Local).AddTicks(8796), 1 },
                    { "7552a7fe-f909-4ab5-8755-c110af85b3b2", "37846734-172e-4149-8cec-6f43d1eb3f60", 900m, new DateTime(2024, 3, 12, 15, 58, 20, 521, DateTimeKind.Local).AddTicks(8824), 0 },
                    { "76f1dfc0-04b1-4805-81d6-dee23720be28", "2f115781-c0d2-4f98-a70b-0bc4ed01d780", 800m, new DateTime(2024, 3, 12, 15, 58, 20, 521, DateTimeKind.Local).AddTicks(8887), 0 },
                    { "7af18e39-4183-4e63-ab01-43fa8be08cdb", "37846734-172e-4149-8cec-6f43d1eb3f60", -300m, new DateTime(2024, 7, 12, 15, 58, 20, 521, DateTimeKind.Local).AddTicks(8786), 1 },
                    { "8064a490-1156-4133-8f50-1ec261f9dceb", "2f115781-c0d2-4f98-a70b-0bc4ed01d780", 100m, new DateTime(2024, 8, 12, 15, 58, 20, 521, DateTimeKind.Local).AddTicks(8866), 0 },
                    { "88fdf9d6-10c7-4979-8727-b2ece3664544", "37846734-172e-4149-8cec-6f43d1eb3f60", -200m, new DateTime(2024, 10, 12, 15, 58, 20, 521, DateTimeKind.Local).AddTicks(8750), 1 },
                    { "8cb7f141-e59a-4d7c-b809-3dc2044458c4", "37846734-172e-4149-8cec-6f43d1eb3f60", 500m, new DateTime(2024, 9, 12, 15, 58, 20, 521, DateTimeKind.Local).AddTicks(8760), 0 },
                    { "95692060-ac52-4ba1-8688-ac1e3433b072", "2f115781-c0d2-4f98-a70b-0bc4ed01d780", 2000m, new DateTime(2025, 2, 11, 15, 58, 20, 521, DateTimeKind.Local).AddTicks(8838), 0 },
                    { "ae520b71-d7da-4195-9035-15888ac99ea2", "2f115781-c0d2-4f98-a70b-0bc4ed01d780", 200m, new DateTime(2024, 11, 12, 15, 58, 20, 521, DateTimeKind.Local).AddTicks(8853), 0 },
                    { "b5ab2b66-8526-4349-be98-62a7d656a422", "37846734-172e-4149-8cec-6f43d1eb3f60", 1000m, new DateTime(2023, 2, 12, 15, 58, 20, 521, DateTimeKind.Local).AddTicks(8683), 0 },
                    { "bf9a3246-0043-4b9f-bee5-3aca11334dab", "37846734-172e-4149-8cec-6f43d1eb3f60", -500m, new DateTime(2024, 2, 12, 15, 58, 20, 521, DateTimeKind.Local).AddTicks(8660), 1 },
                    { "c0996c98-8ad8-4950-b9de-312e367874e2", "2f115781-c0d2-4f98-a70b-0bc4ed01d780", 9000m, new DateTime(2023, 2, 12, 15, 58, 20, 521, DateTimeKind.Local).AddTicks(8849), 0 },
                    { "cfb80e61-334f-43e4-ab0c-7dad889f993f", "37846734-172e-4149-8cec-6f43d1eb3f60", -500m, new DateTime(2024, 4, 12, 15, 58, 20, 521, DateTimeKind.Local).AddTicks(8817), 1 },
                    { "d9875372-4500-4e73-829d-7024ac062dc1", "2f115781-c0d2-4f98-a70b-0bc4ed01d780", -400m, new DateTime(2024, 2, 12, 15, 58, 20, 521, DateTimeKind.Local).AddTicks(8842), 1 },
                    { "db9581f8-1e3d-4589-b4de-c86d75362a52", "2f115781-c0d2-4f98-a70b-0bc4ed01d780", 300m, new DateTime(2024, 5, 12, 15, 58, 20, 521, DateTimeKind.Local).AddTicks(8877), 0 },
                    { "e0d1228c-758c-4a02-b3a3-4a3c74368090", "2f115781-c0d2-4f98-a70b-0bc4ed01d780", 300m, new DateTime(2024, 9, 12, 15, 58, 20, 521, DateTimeKind.Local).AddTicks(8862), 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "06db49d7-d068-4aa2-83f3-3a512aee5a4a");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "1ae030d0-7456-45e2-bd28-ccf8aed1df89");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "2d37e74c-d148-435b-a187-3ef9ff11533b");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "31bb56d7-cc28-411c-8b23-d472771f2999");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "37803ae3-88c4-48ee-bb63-05cc94ff63a5");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "3cdb3d89-729c-4193-a70f-d449e4312f79");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "42d29091-f1c2-4e18-b5b2-1c4f70a7a2a1");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "45b982a1-3e81-4a43-a0b8-564bbe457e31");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "657e6b13-6289-4ae6-b620-85e57feb73a7");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "7552a7fe-f909-4ab5-8755-c110af85b3b2");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "76f1dfc0-04b1-4805-81d6-dee23720be28");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "7af18e39-4183-4e63-ab01-43fa8be08cdb");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "8064a490-1156-4133-8f50-1ec261f9dceb");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "88fdf9d6-10c7-4979-8727-b2ece3664544");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "8cb7f141-e59a-4d7c-b809-3dc2044458c4");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "95692060-ac52-4ba1-8688-ac1e3433b072");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "ae520b71-d7da-4195-9035-15888ac99ea2");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "b5ab2b66-8526-4349-be98-62a7d656a422");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "bf9a3246-0043-4b9f-bee5-3aca11334dab");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "c0996c98-8ad8-4950-b9de-312e367874e2");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "cfb80e61-334f-43e4-ab0c-7dad889f993f");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "d9875372-4500-4e73-829d-7024ac062dc1");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "db9581f8-1e3d-4589-b4de-c86d75362a52");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "e0d1228c-758c-4a02-b3a3-4a3c74368090");

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
        }
    }
}
