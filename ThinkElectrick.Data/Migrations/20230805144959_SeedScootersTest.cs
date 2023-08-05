using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThinkElectric.Data.Migrations
{
    public partial class SeedScootersTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("553d4978-b893-4670-8808-3c91d6165c82"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8b239f01-944c-4f37-afa6-3e66540addc9", "AQAAAAEAACcQAAAAEDssV6acLaX2FlgMdqCNhL25UrpeZdT6TAr8d1eOzgSUmv/3GQd5uscBeWNNRtNehQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("95f65567-3392-47fd-ae6a-95d16dfdbfe2"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "29951fed-0e6d-4266-a0af-236d4709db94", "AQAAAAEAACcQAAAAEMueCRFVIjSmaASIRATSRI77Juq+81qXZTqCs/Xez1tn+Ts43wcFjWCxyMRKKq2Vnw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a44dad0e-bd5f-4394-9f8d-ad5a31e5c24b"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "53b91ec1-def8-4bfe-97d9-c3c393b7726a", "AQAAAAEAACcQAAAAENizNsJ/HDZXH08TyjvtP4DgD1ZvR0pbYfuA0DiclgDcD4W7GIdDavcmd8Ycm1CRUg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ec09bd2a-4c64-476d-9997-e732562b0ab1"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c8ca88e0-7e3b-415f-b3ee-00d3718f5f2d", "AQAAAAEAACcQAAAAEHWWQCeGkdgpph4dTBBtshhSVh35a4dMdYdMNdl9DpmlBRwl40fmxy/Z2o8jrEUdew==" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CompanyId", "CreatedOn", "ImageId", "IsDeleted", "Name", "Price", "ProductType", "Quantity" },
                values: new object[,]
                {
                    { new Guid("1728dc0c-96d8-4869-886e-4829db33a103"), new Guid("c0781351-133e-4383-81e7-c95e20fa1273"), new DateTime(2023, 8, 5, 14, 49, 58, 960, DateTimeKind.Utc).AddTicks(8029), "64ce5f7abd83b3123fc9ab71", false, "Kaabo Mantis King", 2999.99m, 1, 15 },
                    { new Guid("bd96c711-32e7-483b-b1fe-9c19c1e0a936"), new Guid("c0781351-133e-4383-81e7-c95e20fa1273"), new DateTime(2023, 8, 5, 14, 49, 58, 960, DateTimeKind.Utc).AddTicks(8021), "64ce5c39ad51218262de3f60", false, "Kaabo Wolf Warrior 10", 4999.99m, 1, 10 }
                });

            migrationBuilder.InsertData(
                table: "Scooters",
                columns: new[] { "Id", "Battery", "BrakesType", "Brand", "ChargingTime", "Color", "EnginePower", "EngineType", "MaxLoad", "Model", "ProductId", "Range", "TireSize", "TopSpeed", "Type", "Weight" },
                values: new object[] { new Guid("1f884fce-c41c-4489-b249-46c69fe929a2"), "Li-Ion 60V 24.5Ah", 3, "Kaabo", 10, "Black", 2000, 2, 120, "Mantis King", new Guid("1728dc0c-96d8-4869-886e-4829db33a103"), 100, 10, 70, 2, 40 });

            migrationBuilder.InsertData(
                table: "Scooters",
                columns: new[] { "Id", "Battery", "BrakesType", "Brand", "ChargingTime", "Color", "EnginePower", "EngineType", "MaxLoad", "Model", "ProductId", "Range", "TireSize", "TopSpeed", "Type", "Weight" },
                values: new object[] { new Guid("3108d779-0c6e-4178-b309-5a9d4ee11d29"), "Li-Ion 70V 35Ah", 3, "Kaabo", 12, "Black", 2400, 2, 150, "Wolf Warrior 10", new Guid("bd96c711-32e7-483b-b1fe-9c19c1e0a936"), 110, 11, 80, 2, 46 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("1f884fce-c41c-4489-b249-46c69fe929a2"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("3108d779-0c6e-4178-b309-5a9d4ee11d29"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1728dc0c-96d8-4869-886e-4829db33a103"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("bd96c711-32e7-483b-b1fe-9c19c1e0a936"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("553d4978-b893-4670-8808-3c91d6165c82"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "919dac88-f602-483f-8a35-dfafd64c13f2", "AQAAAAEAACcQAAAAELq1kQj/NZSWxxfUXSRgr6dGPkOFCLuJxLipP33L4f5u/Echl78khaB6tMyaXcmohw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("95f65567-3392-47fd-ae6a-95d16dfdbfe2"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cd55ca1f-14af-4ce4-8ff4-8d319000cee8", "AQAAAAEAACcQAAAAEE2YDL7TQff2cHpH1yhDLEs8CxSaan3jjxzOGuJ1K9P0+AR3kzUmmgKhnGPU1nrOiw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a44dad0e-bd5f-4394-9f8d-ad5a31e5c24b"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "13325908-7ee5-4e45-943b-cc8e37483ca4", "AQAAAAEAACcQAAAAEHHqTp6MH3//N4t6UGv6ODC1crqZ3g46OijHtX/7FG8d2nl+B07wDlCllFMtIX3v2Q==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ec09bd2a-4c64-476d-9997-e732562b0ab1"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7fd1850d-b493-42de-8094-692f72b248d6", "AQAAAAEAACcQAAAAEN6Q4EVezKOThkBzqhHFRX93gOdYYs5nH0zGVSPoapKTz42VoSimUYtbi2Mj67qGFQ==" });
        }
    }
}
