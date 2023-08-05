using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThinkElectric.Data.Migrations
{
    public partial class SeedProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("1f884fce-c41c-4489-b249-46c69fe929a2"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("553d4978-b893-4670-8808-3c91d6165c82"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7a912009-36c0-4e44-9135-37b9f16fcb0a", "AQAAAAEAACcQAAAAEECMjd0byedN+mCeJFMawRkU9Hh4hYK2Ij8PBWhBk57N9kIrzEpzcDXjYIeVvgvRcw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("95f65567-3392-47fd-ae6a-95d16dfdbfe2"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8e3ccd79-0f37-4d41-846d-26049ab5ef9e", "AQAAAAEAACcQAAAAEI/IUOmYtlzc1coyLwU6fjHYm9qT1ncBResrAxWMd/qyEZy+wT60q+bQcMcrD5x0Ig==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a44dad0e-bd5f-4394-9f8d-ad5a31e5c24b"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "76d3245f-92b2-4416-86b0-96d80cce8b24", "AQAAAAEAACcQAAAAEDfgds79U7lB7y85vTB//9LFjmqHA6tEVD6fhHWfcLQEhG1lbKX0lXbAz2pSCWvG/g==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ec09bd2a-4c64-476d-9997-e732562b0ab1"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a46e132d-4ee3-49ac-834c-51844173bc98", "AQAAAAEAACcQAAAAEKb7Qx4YK6ELQXnjB1/s3BAZDQJGqFpGdoJEndm1rXZmZ0Nsi06A7UmJKpvN5YNODQ==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1728dc0c-96d8-4869-886e-4829db33a103"),
                column: "CreatedOn",
                value: new DateTime(2023, 7, 24, 8, 45, 25, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("bd96c711-32e7-483b-b1fe-9c19c1e0a936"),
                column: "CreatedOn",
                value: new DateTime(2023, 7, 25, 8, 45, 25, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CompanyId", "CreatedOn", "ImageId", "IsDeleted", "Name", "Price", "ProductType", "Quantity" },
                values: new object[,]
                {
                    { new Guid("0d233dd0-bd97-4bee-bc9e-dd96ccef5d12"), new Guid("c0781351-133e-4383-81e7-c95e20fa1273"), new DateTime(2023, 3, 15, 8, 45, 25, 0, DateTimeKind.Unspecified), "64ce6ac5332b4fcd3cfbf213", false, "ADO A16XE", 1100.99m, 2, 15 },
                    { new Guid("253a48e4-63c2-4564-b911-b098f37d8370"), new Guid("5d652ebf-7b4d-430c-9af8-0c02b0256575"), new DateTime(2023, 5, 25, 8, 45, 25, 0, DateTimeKind.Unspecified), "64ce66572b18cc1f74d4f76b", false, "Xiaomi Mi Pro 2", 500.99m, 1, 15 },
                    { new Guid("3e044b98-8123-4273-b51f-9cf0dfc13760"), new Guid("5d652ebf-7b4d-430c-9af8-0c02b0256575"), new DateTime(2023, 3, 3, 8, 45, 25, 0, DateTimeKind.Unspecified), "64ce7219abc83a6ff8f1da33", false, "Kaabo Bag", 50.99m, 3, 35 },
                    { new Guid("485b227b-69d8-4a3c-bf73-7f25007225c5"), new Guid("5d652ebf-7b4d-430c-9af8-0c02b0256575"), new DateTime(2023, 7, 17, 8, 25, 25, 0, DateTimeKind.Unspecified), "64ce6fc7a61cf5992be7bf4f", false, "ADO 20 F BEAST", 2000.99m, 2, 5 },
                    { new Guid("67245662-bc0a-4f41-b53e-a9fb4bbdaa9f"), new Guid("c0781351-133e-4383-81e7-c95e20fa1273"), new DateTime(2023, 7, 20, 7, 45, 25, 0, DateTimeKind.Unspecified), "64ce64743037f396fd344914", false, "Kaabo Mantis 10 Pro", 1500.99m, 1, 20 },
                    { new Guid("8fe19082-5089-4ac5-88d9-51fdfa48fe10"), new Guid("c0781351-133e-4383-81e7-c95e20fa1273"), new DateTime(2023, 6, 16, 6, 45, 25, 0, DateTimeKind.Unspecified), "64ce74f819d865ac7cfd73e6", false, "Xiaomi Side Mirrors", 40.99m, 3, 15 },
                    { new Guid("d2ab0180-a9b8-470e-8a0f-9acaccb0c9bc"), new Guid("c0781351-133e-4383-81e7-c95e20fa1273"), new DateTime(2023, 7, 8, 8, 19, 25, 0, DateTimeKind.Unspecified), "64ce737583efd7dc2360de20", false, "Kaabo Disc Lock", 70.99m, 3, 10 },
                    { new Guid("d3b10c99-12e3-4e69-9771-70dafac10bb3"), new Guid("5d652ebf-7b4d-430c-9af8-0c02b0256575"), new DateTime(2023, 5, 25, 8, 25, 25, 0, DateTimeKind.Unspecified), "64ce68eaffdd5b60bd0f0da6", false, "Xiaomi 365 Amg", 799.99m, 1, 5 },
                    { new Guid("d96bb9ea-2cea-4d39-bb90-e8a94d58a819"), new Guid("5d652ebf-7b4d-430c-9af8-0c02b0256575"), new DateTime(2023, 2, 26, 8, 45, 25, 0, DateTimeKind.Unspecified), "64ce67d918f465b345e8e244", false, "Xiaomi Mi 365", 450.99m, 1, 30 },
                    { new Guid("f6b1216e-e1a6-4b83-a2b4-dc58f30d0e8e"), new Guid("c0781351-133e-4383-81e7-c95e20fa1273"), new DateTime(2023, 7, 11, 4, 45, 25, 0, DateTimeKind.Unspecified), "64ce6e7274be46075f7f39ef", false, "ADO A20 AIR", 1000.99m, 2, 10 }
                });

            migrationBuilder.InsertData(
                table: "Scooters",
                columns: new[] { "Id", "Battery", "BrakesType", "Brand", "ChargingTime", "Color", "EnginePower", "EngineType", "MaxLoad", "Model", "ProductId", "Range", "TireSize", "TopSpeed", "Type", "Weight" },
                values: new object[] { new Guid("b0163142-364a-4b15-9bfc-65e381180ad9"), "Li-Ion 60V 24.5Ah", 3, "Kaabo", 10, "Black", 2000, 2, 120, "Mantis King", new Guid("1728dc0c-96d8-4869-886e-4829db33a103"), 100, 10, 70, 2, 40 });

            migrationBuilder.InsertData(
                table: "Accessories",
                columns: new[] { "Id", "Brand", "CompatibleBrand", "CompatibleModel", "Description", "ProductId" },
                values: new object[,]
                {
                    { new Guid("58605343-b867-4905-bfa3-364a8d3940d3"), "Kaabo", "Kaabo", "Mantis", "Original Kaabo bag for the your Kaabo Mantis scooter!", new Guid("3e044b98-8123-4273-b51f-9cf0dfc13760") },
                    { new Guid("9318d0ea-30db-4431-9c30-ae1993f17728"), "Kaabo", "Kaabo", "Wolf Warrior", "Original Kaabo lock for your kaabo wolf warrior scooter!", new Guid("d2ab0180-a9b8-470e-8a0f-9acaccb0c9bc") },
                    { new Guid("ae2bbad3-a635-4860-b2c0-3260f87ce97b"), "SideTech", "Xiaomi", "Pro 2", "High quality side mirrors for Xiaomi Pro2 scooters!", new Guid("8fe19082-5089-4ac5-88d9-51fdfa48fe10") }
                });

            migrationBuilder.InsertData(
                table: "Bikes",
                columns: new[] { "Id", "Battery", "BrakesType", "Brand", "ChargingTime", "Color", "EnginePower", "EngineType", "FrameMaterial", "FrameSize", "FrameType", "GearsCount", "MaxLoad", "Model", "ProductId", "Range", "SuspensionType", "TopSpeed", "Type", "Weight", "WheelSize" },
                values: new object[,]
                {
                    { new Guid("2c2a92ec-55d2-46e0-841b-9024c1659cb1"), "48V 13Ah", 3, "ADO", 6, "Black", 250, 1, "Aluminum", 20, 1, 7, 120, "A16XE", new Guid("0d233dd0-bd97-4bee-bc9e-dd96ccef5d12"), 60, 2, 25, 1, 25, 15 },
                    { new Guid("5d9ec56d-d23a-49cb-b7e8-39ebd8f5c302"), "48V 20Ah", 3, "ADO", 9, "Black", 600, 1, "Carbon", 22, 2, 10, 150, "20 F Beast", new Guid("485b227b-69d8-4a3c-bf73-7f25007225c5"), 70, 4, 40, 2, 35, 18 },
                    { new Guid("e56a719f-ad51-4bfa-b24b-ead6ef17192c"), "48V 15Ah", 2, "ADO", 7, "Gray", 300, 2, "Aluminum", 18, 1, 6, 100, "A20 AIR", new Guid("f6b1216e-e1a6-4b83-a2b4-dc58f30d0e8e"), 50, 2, 35, 1, 20, 14 }
                });

            migrationBuilder.InsertData(
                table: "Scooters",
                columns: new[] { "Id", "Battery", "BrakesType", "Brand", "ChargingTime", "Color", "EnginePower", "EngineType", "MaxLoad", "Model", "ProductId", "Range", "TireSize", "TopSpeed", "Type", "Weight" },
                values: new object[,]
                {
                    { new Guid("2d93c875-2fda-43b9-b90a-9dc238b8b00f"), "Li-Ion 60V 18.5Ah", 2, "Kaabo", 9, "Red", 1000, 2, 120, "Mantis 10 Pro", new Guid("67245662-bc0a-4f41-b53e-a9fb4bbdaa9f"), 70, 10, 60, 5, 30 },
                    { new Guid("6ed9a802-c17a-41ce-938d-767c8b9e4ef5"), "Li-Ion 47V 12.5Ah", 1, "Xiaomi", 6, "Black", 350, 1, 100, "Mi Pro 2", new Guid("253a48e4-63c2-4564-b911-b098f37d8370"), 45, 8, 25, 1, 18 },
                    { new Guid("dd309c19-77ea-487f-bf2c-10df6deb4706"), "Li-Ion 47V 13.5Ah", 1, "Xiaomi", 7, "Gray", 400, 1, 120, "Mi Amg", new Guid("d3b10c99-12e3-4e69-9771-70dafac10bb3"), 40, 8, 25, 1, 18 },
                    { new Guid("e199e336-52be-41b3-807e-d705ebc6108b"), "Li-Ion 47V 10.5Ah", 1, "Xiaomi", 6, "White", 300, 1, 100, "Mi 365", new Guid("d96bb9ea-2cea-4d39-bb90-e8a94d58a819"), 35, 8, 25, 1, 17 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accessories",
                keyColumn: "Id",
                keyValue: new Guid("58605343-b867-4905-bfa3-364a8d3940d3"));

            migrationBuilder.DeleteData(
                table: "Accessories",
                keyColumn: "Id",
                keyValue: new Guid("9318d0ea-30db-4431-9c30-ae1993f17728"));

            migrationBuilder.DeleteData(
                table: "Accessories",
                keyColumn: "Id",
                keyValue: new Guid("ae2bbad3-a635-4860-b2c0-3260f87ce97b"));

            migrationBuilder.DeleteData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: new Guid("2c2a92ec-55d2-46e0-841b-9024c1659cb1"));

            migrationBuilder.DeleteData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: new Guid("5d9ec56d-d23a-49cb-b7e8-39ebd8f5c302"));

            migrationBuilder.DeleteData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: new Guid("e56a719f-ad51-4bfa-b24b-ead6ef17192c"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("2d93c875-2fda-43b9-b90a-9dc238b8b00f"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("6ed9a802-c17a-41ce-938d-767c8b9e4ef5"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("b0163142-364a-4b15-9bfc-65e381180ad9"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("dd309c19-77ea-487f-bf2c-10df6deb4706"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("e199e336-52be-41b3-807e-d705ebc6108b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("0d233dd0-bd97-4bee-bc9e-dd96ccef5d12"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("253a48e4-63c2-4564-b911-b098f37d8370"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3e044b98-8123-4273-b51f-9cf0dfc13760"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("485b227b-69d8-4a3c-bf73-7f25007225c5"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("67245662-bc0a-4f41-b53e-a9fb4bbdaa9f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8fe19082-5089-4ac5-88d9-51fdfa48fe10"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d2ab0180-a9b8-470e-8a0f-9acaccb0c9bc"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d3b10c99-12e3-4e69-9771-70dafac10bb3"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d96bb9ea-2cea-4d39-bb90-e8a94d58a819"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f6b1216e-e1a6-4b83-a2b4-dc58f30d0e8e"));

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

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1728dc0c-96d8-4869-886e-4829db33a103"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 5, 14, 49, 58, 960, DateTimeKind.Utc).AddTicks(8029));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("bd96c711-32e7-483b-b1fe-9c19c1e0a936"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 5, 14, 49, 58, 960, DateTimeKind.Utc).AddTicks(8021));

            migrationBuilder.InsertData(
                table: "Scooters",
                columns: new[] { "Id", "Battery", "BrakesType", "Brand", "ChargingTime", "Color", "EnginePower", "EngineType", "MaxLoad", "Model", "ProductId", "Range", "TireSize", "TopSpeed", "Type", "Weight" },
                values: new object[] { new Guid("1f884fce-c41c-4489-b249-46c69fe929a2"), "Li-Ion 60V 24.5Ah", 3, "Kaabo", 10, "Black", 2000, 2, 120, "Mantis King", new Guid("1728dc0c-96d8-4869-886e-4829db33a103"), 100, 10, 70, 2, 40 });
        }
    }
}
