using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fakeAccounts : Migration
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
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber", "ProfilePicUrl" },
                values: new object[,]
                {
                    { "10", "fake@fake.com", "Fake", "Fake", "10998877665", "https://images.freeimages.com/images/premium/previews/1670/16703169-disgusted-lounge-singer.jpg" },
                    { "11", "fake@fake.com", "Fake", "Fake", "11998877665", "https://images.freeimages.com/images/premium/previews/1670/16703169-disgusted-lounge-singer.jpg" },
                    { "12", "fake@fake.com", "Fake", "Fake", "12998877665", "https://images.freeimages.com/images/premium/previews/1670/16703169-disgusted-lounge-singer.jpg" },
                    { "13", "fake@fake.com", "Fake", "Fake", "13998877665", "https://images.freeimages.com/images/premium/previews/1670/16703169-disgusted-lounge-singer.jpg" },
                    { "14", "fake@fake.com", "Fake", "Fake", "14998877665", "https://images.freeimages.com/images/premium/previews/1670/16703169-disgusted-lounge-singer.jpg" },
                    { "15", "fake@fake.com", "Fake", "Fake", "15998877665", "https://images.freeimages.com/images/premium/previews/1670/16703169-disgusted-lounge-singer.jpg" },
                    { "16", "fake@fake.com", "Fake", "Fake", "16998877665", "https://images.freeimages.com/images/premium/previews/1670/16703169-disgusted-lounge-singer.jpg" },
                    { "17", "fake@fake.com", "Fake", "Fake", "17998877665", "https://images.freeimages.com/images/premium/previews/1670/16703169-disgusted-lounge-singer.jpg" },
                    { "18", "fake@fake.com", "Fake", "Fake", "18998877665", "https://images.freeimages.com/images/premium/previews/1670/16703169-disgusted-lounge-singer.jpg" },
                    { "19", "fake@fake.com", "Fake", "Fake", "19998877665", "https://images.freeimages.com/images/premium/previews/1670/16703169-disgusted-lounge-singer.jpg" },
                    { "20", "fake@fake.com", "Fake", "Fake", "20998877665", "https://images.freeimages.com/images/premium/previews/1670/16703169-disgusted-lounge-singer.jpg" },
                    { "21", "fake@fake.com", "Fake", "Fake", "21998877665", "https://images.freeimages.com/images/premium/previews/1670/16703169-disgusted-lounge-singer.jpg" },
                    { "22", "fake@fake.com", "Fake", "Fake", "22998877665", "https://images.freeimages.com/images/premium/previews/1670/16703169-disgusted-lounge-singer.jpg" },
                    { "3", "fake@fake.com", "Fake", "Fake", "3998877665", "https://images.freeimages.com/images/premium/previews/1670/16703169-disgusted-lounge-singer.jpg" },
                    { "4", "fake@fake.com", "Fake", "Fake", "4998877665", "https://images.freeimages.com/images/premium/previews/1670/16703169-disgusted-lounge-singer.jpg" },
                    { "5", "fake@fake.com", "Fake", "Fake", "5998877665", "https://images.freeimages.com/images/premium/previews/1670/16703169-disgusted-lounge-singer.jpg" },
                    { "52694168-6706-4595-bdea-bae0da5923f0", "john.doe.ddd@outlook.com", "John", "Doe", null, "https://res.cloudinary.com/demo/image/upload/w_400,h_400,c_crop,g_face,r_max/w_200/lady.jpg" },
                    { "5b1aa188-636f-436a-a2da-ae742ddadedf", "admin@patternstech.com", "Patterns", "Tech", null, "https://res.cloudinary.com/demo/image/upload/w_400,h_400,c_crop,g_face,r_max/w_200/lady.jpg" },
                    { "6", "fake@fake.com", "Fake", "Fake", "6998877665", "https://images.freeimages.com/images/premium/previews/1670/16703169-disgusted-lounge-singer.jpg" },
                    { "7", "fake@fake.com", "Fake", "Fake", "7998877665", "https://images.freeimages.com/images/premium/previews/1670/16703169-disgusted-lounge-singer.jpg" },
                    { "8", "fake@fake.com", "Fake", "Fake", "8998877665", "https://images.freeimages.com/images/premium/previews/1670/16703169-disgusted-lounge-singer.jpg" },
                    { "9", "fake@fake.com", "Fake", "Fake", "9998877665", "https://images.freeimages.com/images/premium/previews/1670/16703169-disgusted-lounge-singer.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountNumber", "AccountStatus", "AccountTitle", "CurrentBalance", "UserId" },
                values: new object[,]
                {
                    { "08838ac3-add9-4392-bd40-03dab85db89a", "8-xxx-xxx", 1, "Fake Account 8", 800m, "8" },
                    { "235b840a-5adb-4bb0-b772-68e36b63692f", "21-xxx-xxx", 1, "Fake Account 21", 2100m, "21" },
                    { "2b55ef13-6c92-415b-851e-d91ce819210b", "14-xxx-xxx", 1, "Fake Account 14", 1400m, "14" },
                    { "2f115781-c0d2-4f98-a70b-0bc4ed01d780", "0002-2002", 0, "John Doe", 545m, "52694168-6706-4595-bdea-bae0da5923f0" },
                    { "37688472-c1ab-4ec4-b52f-3a8527d1a771", "7-xxx-xxx", 1, "Fake Account 7", 700m, "7" },
                    { "37846734-172e-4149-8cec-6f43d1eb3f60", "0001-1001", 0, "Patterns Tech", 3500m, "5b1aa188-636f-436a-a2da-ae742ddadedf" },
                    { "447764ed-69b9-4c44-8262-e9575cafccd1", "3-xxx-xxx", 1, "Fake Account 3", 300m, "3" },
                    { "471b9643-4801-4b43-a50b-487b621d9b3c", "17-xxx-xxx", 1, "Fake Account 17", 1700m, "17" },
                    { "4c6982e1-fb17-4cc1-a9ff-921d39ae3228", "10-xxx-xxx", 1, "Fake Account 10", 1000m, "10" },
                    { "610356ec-2bba-462e-8fa9-2984a004fdc2", "19-xxx-xxx", 1, "Fake Account 19", 1900m, "19" },
                    { "69c8ab81-f5e9-4598-aecc-0948c17f3601", "15-xxx-xxx", 1, "Fake Account 15", 1500m, "15" },
                    { "6bc79aed-08fb-4bdf-a2be-88b26c3a4429", "11-xxx-xxx", 1, "Fake Account 11", 1100m, "11" },
                    { "9339a982-d23c-4728-b883-4a3a7b733611", "6-xxx-xxx", 1, "Fake Account 6", 600m, "6" },
                    { "9fd1b484-456c-46b6-8ca3-f70e6717f31b", "22-xxx-xxx", 1, "Fake Account 22", 2200m, "22" },
                    { "c14816d9-2238-44ea-a6a3-7f826895d601", "12-xxx-xxx", 1, "Fake Account 12", 1200m, "12" },
                    { "c74f4cb8-966c-4d38-9076-d3c933b6b0cd", "4-xxx-xxx", 1, "Fake Account 4", 400m, "4" },
                    { "cec07431-c29c-4d7b-9855-4b81f4956b78", "5-xxx-xxx", 1, "Fake Account 5", 500m, "5" },
                    { "d26ffa0f-95c3-45bc-8907-4429ff68451d", "16-xxx-xxx", 1, "Fake Account 16", 1600m, "16" },
                    { "f311a7f9-8467-41eb-a237-db7473e7b3fb", "18-xxx-xxx", 1, "Fake Account 18", 1800m, "18" },
                    { "fa32b50a-5cc2-4fb3-a75e-a03db682db12", "9-xxx-xxx", 1, "Fake Account 9", 900m, "9" },
                    { "ff6af9be-9145-4165-ab7e-6a8b3066b6cf", "20-xxx-xxx", 1, "Fake Account 20", 2000m, "20" },
                    { "ffb7d932-e1af-4bfc-9dd2-2037ac668822", "13-xxx-xxx", 1, "Fake Account 13", 1300m, "13" }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "AccountId", "TransactionAmount", "TransactionDate", "TransactionType" },
                values: new object[,]
                {
                    { "03387228-a5b1-4915-9af6-d273d62f5eed", "37846734-172e-4149-8cec-6f43d1eb3f60", 900m, new DateTime(2024, 3, 21, 2, 47, 50, 65, DateTimeKind.Local).AddTicks(7236), 0 },
                    { "036267d0-6f41-46b7-9dce-bd2f56b97dc5", "2f115781-c0d2-4f98-a70b-0bc4ed01d780", -400m, new DateTime(2024, 2, 21, 2, 47, 50, 65, DateTimeKind.Local).AddTicks(7257), 1 },
                    { "15fae58c-7507-4b34-989c-79ab7cbb4e78", "37846734-172e-4149-8cec-6f43d1eb3f60", 500m, new DateTime(2024, 9, 21, 2, 47, 50, 65, DateTimeKind.Local).AddTicks(7110), 0 },
                    { "17f6797a-f486-4414-87fe-8ba8b9aef4b2", "37846734-172e-4149-8cec-6f43d1eb3f60", -100m, new DateTime(2024, 6, 21, 2, 47, 50, 65, DateTimeKind.Local).AddTicks(7155), 1 },
                    { "1bfb3a57-33fd-4724-96e8-066530ee9976", "2f115781-c0d2-4f98-a70b-0bc4ed01d780", 200m, new DateTime(2024, 11, 21, 2, 47, 50, 65, DateTimeKind.Local).AddTicks(7292), 0 },
                    { "256a4940-f2d6-42c0-9306-d2c7975a2aad", "2f115781-c0d2-4f98-a70b-0bc4ed01d780", 2000m, new DateTime(2025, 2, 20, 2, 47, 50, 65, DateTimeKind.Local).AddTicks(7249), 0 },
                    { "2d90c76e-1912-4ef2-80a5-d610831a5b05", "37846734-172e-4149-8cec-6f43d1eb3f60", -500m, new DateTime(2024, 4, 21, 2, 47, 50, 65, DateTimeKind.Local).AddTicks(7229), 1 },
                    { "3f3e7eca-2bce-4272-982c-b2d3640d98c8", "2f115781-c0d2-4f98-a70b-0bc4ed01d780", -200m, new DateTime(2024, 6, 21, 2, 47, 50, 65, DateTimeKind.Local).AddTicks(7330), 1 },
                    { "4faa9d52-eec3-4eab-b631-d05a39be331f", "37846734-172e-4149-8cec-6f43d1eb3f60", -300m, new DateTime(2024, 7, 21, 2, 47, 50, 65, DateTimeKind.Local).AddTicks(7141), 1 },
                    { "6f836b03-c618-4785-bdd3-123688b83ec7", "2f115781-c0d2-4f98-a70b-0bc4ed01d780", 300m, new DateTime(2024, 5, 21, 2, 47, 50, 65, DateTimeKind.Local).AddTicks(7338), 0 },
                    { "865c10a7-68fc-4a8d-a55f-5101bf1be801", "37846734-172e-4149-8cec-6f43d1eb3f60", 500m, new DateTime(2024, 11, 21, 2, 47, 50, 65, DateTimeKind.Local).AddTicks(7040), 0 },
                    { "94e179db-784b-4188-bc88-4252e85a132d", "37846734-172e-4149-8cec-6f43d1eb3f60", 200m, new DateTime(2024, 8, 21, 2, 47, 50, 65, DateTimeKind.Local).AddTicks(7134), 0 },
                    { "958db5f3-0f02-4c7d-bc31-e2296e25344c", "37846734-172e-4149-8cec-6f43d1eb3f60", 3000m, new DateTime(2025, 2, 20, 2, 47, 50, 65, DateTimeKind.Local).AddTicks(5561), 0 },
                    { "9a4e5783-fc28-4ee4-8ce7-a903ded088d2", "2f115781-c0d2-4f98-a70b-0bc4ed01d780", -200m, new DateTime(2024, 7, 21, 2, 47, 50, 65, DateTimeKind.Local).AddTicks(7320), 1 },
                    { "a0d86a11-ef32-44b7-86b1-402ad7463fb1", "37846734-172e-4149-8cec-6f43d1eb3f60", -200m, new DateTime(2024, 10, 21, 2, 47, 50, 65, DateTimeKind.Local).AddTicks(7102), 1 },
                    { "a200069f-336a-42ca-b564-617c9553681c", "2f115781-c0d2-4f98-a70b-0bc4ed01d780", -100m, new DateTime(2024, 10, 21, 2, 47, 50, 65, DateTimeKind.Local).AddTicks(7298), 1 },
                    { "af29038e-fa1d-4436-8df9-ba7bd4c9ea30", "2f115781-c0d2-4f98-a70b-0bc4ed01d780", -200m, new DateTime(2024, 4, 21, 2, 47, 50, 65, DateTimeKind.Local).AddTicks(7345), 1 },
                    { "be46c1e2-75a2-41e5-82f3-5bc5f5407a88", "37846734-172e-4149-8cec-6f43d1eb3f60", -500m, new DateTime(2024, 2, 21, 2, 47, 50, 65, DateTimeKind.Local).AddTicks(6955), 1 },
                    { "c0f382fb-d94d-4eed-bdbf-80fa296622d2", "37846734-172e-4149-8cec-6f43d1eb3f60", 1000m, new DateTime(2023, 2, 21, 2, 47, 50, 65, DateTimeKind.Local).AddTicks(7032), 0 },
                    { "c5702440-8aa7-4b80-b1cf-f60e0ccb52d1", "2f115781-c0d2-4f98-a70b-0bc4ed01d780", 800m, new DateTime(2024, 3, 21, 2, 47, 50, 65, DateTimeKind.Local).AddTicks(7351), 0 },
                    { "e21aced0-d75c-4aec-9a2d-4b6c6a67e302", "2f115781-c0d2-4f98-a70b-0bc4ed01d780", 300m, new DateTime(2024, 9, 21, 2, 47, 50, 65, DateTimeKind.Local).AddTicks(7306), 0 },
                    { "e53b04a4-df36-4209-9285-8db630992f20", "37846734-172e-4149-8cec-6f43d1eb3f60", 200m, new DateTime(2024, 5, 21, 2, 47, 50, 65, DateTimeKind.Local).AddTicks(7212), 0 },
                    { "e7611ca6-389f-4d2d-b010-11572a585d4e", "2f115781-c0d2-4f98-a70b-0bc4ed01d780", 100m, new DateTime(2024, 8, 21, 2, 47, 50, 65, DateTimeKind.Local).AddTicks(7313), 0 },
                    { "fca4b8f0-1d5a-43da-8536-d9f5bbcc8a24", "2f115781-c0d2-4f98-a70b-0bc4ed01d780", 9000m, new DateTime(2023, 2, 21, 2, 47, 50, 65, DateTimeKind.Local).AddTicks(7271), 0 }
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
