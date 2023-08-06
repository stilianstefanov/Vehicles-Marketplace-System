using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThinkElectric.Data.Migrations
{
    public partial class SeedAdminCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("553d4978-b893-4670-8808-3c91d6165c82"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1ff44686-9096-475e-a07d-7ef1df892326", "AQAAAAEAACcQAAAAEG3w7xQ3gX5RdfEV8DtLCfEZ+xmi2OYMpmkcgmHkbpatf9l7HSnHOsCQze/WS2nteA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("95f65567-3392-47fd-ae6a-95d16dfdbfe2"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a62e9847-6af3-41ef-bc6c-0b6242039a84", "AQAAAAEAACcQAAAAEAxmL5iaONPZc94InrI5O0XQk+g7IrgM/3FUaj7eP0oQwKBATZJUylI620XGHfojSQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a44dad0e-bd5f-4394-9f8d-ad5a31e5c24b"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b2fac17b-cbae-4c21-a1b1-55e9ca03b74a", "AQAAAAEAACcQAAAAEO5B5WoPz4HJca1eQT4iYY55bQqF2FP7389cRDn36+BRofGP7K7sdhmXoBxGELfJKQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ec09bd2a-4c64-476d-9997-e732562b0ab1"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e9ed10a9-b088-49af-812f-013d73283438", "AQAAAAEAACcQAAAAEE+iOxjH5Clt7n/JzZMqxNh9uS1wgJGMMDwFw0p8zZDlQ4cq1ZeWu7FAQvLPpvNOhQ==" });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "UserId" },
                values: new object[] { new Guid("3226ee7e-6e28-4c7c-b338-9ea6df852957"), new Guid("ec09bd2a-4c64-476d-9997-e732562b0ab1") });

            migrationBuilder.InsertData(
                table: "Scooters",
                columns: new[] { "Id", "Battery", "BrakesType", "Brand", "ChargingTime", "Color", "EnginePower", "EngineType", "MaxLoad", "Model", "ProductId", "Range", "TireSize", "TopSpeed", "Type", "Weight" },
                values: new object[,]
                {
                    { new Guid("48334f42-e1a2-4f15-8cae-af34a4921d24"), "Li-Ion 47V 13.5Ah", 1, "Xiaomi", 7, "Gray", 400, 1, 120, "Mi Amg", new Guid("d3b10c99-12e3-4e69-9771-70dafac10bb3"), 40, 8, 25, 1, 18 },
                    { new Guid("6ed89d48-5868-4939-bded-039f5a44960e"), "Li-Ion 60V 24.5Ah", 3, "Kaabo", 10, "Black", 2000, 2, 120, "Mantis King", new Guid("1728dc0c-96d8-4869-886e-4829db33a103"), 100, 10, 70, 2, 40 },
                    { new Guid("7c215b7a-0c3e-49ab-86c8-c8d7d84f1866"), "Li-Ion 47V 12.5Ah", 1, "Xiaomi", 6, "Black", 350, 1, 100, "Mi Pro 2", new Guid("253a48e4-63c2-4564-b911-b098f37d8370"), 45, 8, 25, 1, 18 },
                    { new Guid("cb924236-c377-4fa6-b337-eeabd776bed8"), "Li-Ion 60V 18.5Ah", 2, "Kaabo", 9, "Red", 1000, 2, 120, "Mantis 10 Pro", new Guid("67245662-bc0a-4f41-b53e-a9fb4bbdaa9f"), 70, 10, 60, 5, 30 },
                    { new Guid("e0acf67e-7717-4b50-8cd8-807434b82a1d"), "Li-Ion 47V 10.5Ah", 1, "Xiaomi", 6, "White", 300, 1, 100, "Mi 365", new Guid("d96bb9ea-2cea-4d39-bb90-e8a94d58a819"), 35, 8, 25, 1, 17 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: new Guid("3226ee7e-6e28-4c7c-b338-9ea6df852957"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("48334f42-e1a2-4f15-8cae-af34a4921d24"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("6ed89d48-5868-4939-bded-039f5a44960e"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("7c215b7a-0c3e-49ab-86c8-c8d7d84f1866"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("cb924236-c377-4fa6-b337-eeabd776bed8"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("e0acf67e-7717-4b50-8cd8-807434b82a1d"));

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

            migrationBuilder.InsertData(
                table: "Scooters",
                columns: new[] { "Id", "Battery", "BrakesType", "Brand", "ChargingTime", "Color", "EnginePower", "EngineType", "MaxLoad", "Model", "ProductId", "Range", "TireSize", "TopSpeed", "Type", "Weight" },
                values: new object[,]
                {
                    { new Guid("2d93c875-2fda-43b9-b90a-9dc238b8b00f"), "Li-Ion 60V 18.5Ah", 2, "Kaabo", 9, "Red", 1000, 2, 120, "Mantis 10 Pro", new Guid("67245662-bc0a-4f41-b53e-a9fb4bbdaa9f"), 70, 10, 60, 5, 30 },
                    { new Guid("6ed9a802-c17a-41ce-938d-767c8b9e4ef5"), "Li-Ion 47V 12.5Ah", 1, "Xiaomi", 6, "Black", 350, 1, 100, "Mi Pro 2", new Guid("253a48e4-63c2-4564-b911-b098f37d8370"), 45, 8, 25, 1, 18 },
                    { new Guid("b0163142-364a-4b15-9bfc-65e381180ad9"), "Li-Ion 60V 24.5Ah", 3, "Kaabo", 10, "Black", 2000, 2, 120, "Mantis King", new Guid("1728dc0c-96d8-4869-886e-4829db33a103"), 100, 10, 70, 2, 40 },
                    { new Guid("dd309c19-77ea-487f-bf2c-10df6deb4706"), "Li-Ion 47V 13.5Ah", 1, "Xiaomi", 7, "Gray", 400, 1, 120, "Mi Amg", new Guid("d3b10c99-12e3-4e69-9771-70dafac10bb3"), 40, 8, 25, 1, 18 },
                    { new Guid("e199e336-52be-41b3-807e-d705ebc6108b"), "Li-Ion 47V 10.5Ah", 1, "Xiaomi", 6, "White", 300, 1, 100, "Mi 365", new Guid("d96bb9ea-2cea-4d39-bb90-e8a94d58a819"), 35, 8, 25, 1, 17 }
                });
        }
    }
}
