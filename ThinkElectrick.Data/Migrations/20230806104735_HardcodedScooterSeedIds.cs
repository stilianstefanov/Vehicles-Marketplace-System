using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThinkElectric.Data.Migrations
{
    public partial class HardcodedScooterSeedIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                values: new object[] { "4f75fbf5-0c72-4255-bfe1-a8d264ea7635", "AQAAAAEAACcQAAAAEE2XDA/3LhEvslpelN9wIgplIiqf2iV6WlkdJDFcxffuVwV9Thf49i76Db7M2iSrcg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("95f65567-3392-47fd-ae6a-95d16dfdbfe2"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ed8b4047-46bd-421c-be59-1269c4a4983b", "AQAAAAEAACcQAAAAEGoG4IAYGlDkSMAufFkNAFurwiCmDsB/ysqpQcPU96ADKFiH5zHLdrUOD0wHtx0q4Q==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a44dad0e-bd5f-4394-9f8d-ad5a31e5c24b"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "873cf8cd-ace7-4e58-8557-ca8277f7741e", "AQAAAAEAACcQAAAAEGNouWLZAywVqcIa8NqiBpUN496er82aiVXk0WRyLzQVrDGDTV6vRsF+5Z1w98MXqA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ec09bd2a-4c64-476d-9997-e732562b0ab1"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2c10c135-0ed2-4aba-9f8d-0c48c495b3dd", "AQAAAAEAACcQAAAAENI/69zN7D7TmZXjnlERXesSJCN2zV2ArWxPTfiQ457kAy/bHVXCPQbkxtZYtpufXQ==" });

            migrationBuilder.InsertData(
                table: "Scooters",
                columns: new[] { "Id", "Battery", "BrakesType", "Brand", "ChargingTime", "Color", "EnginePower", "EngineType", "MaxLoad", "Model", "ProductId", "Range", "TireSize", "TopSpeed", "Type", "Weight" },
                values: new object[,]
                {
                    { new Guid("22cc4ae0-2d70-45ee-8302-956fae8bbf0d"), "Li-Ion 47V 13.5Ah", 1, "Xiaomi", 7, "Gray", 400, 1, 120, "Mi Amg", new Guid("d3b10c99-12e3-4e69-9771-70dafac10bb3"), 40, 8, 25, 1, 18 },
                    { new Guid("a4ac8b34-4a66-4022-a03f-ce8ca411a38f"), "Li-Ion 60V 18.5Ah", 2, "Kaabo", 9, "Red", 1000, 2, 120, "Mantis 10 Pro", new Guid("67245662-bc0a-4f41-b53e-a9fb4bbdaa9f"), 70, 10, 60, 5, 30 },
                    { new Guid("c1007106-b91c-4bba-9517-d91eec70cc91"), "Li-Ion 60V 24.5Ah", 3, "Kaabo", 10, "Black", 2000, 2, 120, "Mantis King", new Guid("1728dc0c-96d8-4869-886e-4829db33a103"), 100, 10, 70, 2, 40 },
                    { new Guid("d29b190b-82f0-4b9a-bed8-002b3e2a464e"), "Li-Ion 47V 10.5Ah", 1, "Xiaomi", 6, "White", 300, 1, 100, "Mi 365", new Guid("d96bb9ea-2cea-4d39-bb90-e8a94d58a819"), 35, 8, 25, 1, 17 },
                    { new Guid("f474d048-fd43-4795-a86b-1c77f57e2535"), "Li-Ion 47V 12.5Ah", 1, "Xiaomi", 6, "Black", 350, 1, 100, "Mi Pro 2", new Guid("253a48e4-63c2-4564-b911-b098f37d8370"), 45, 8, 25, 1, 18 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("22cc4ae0-2d70-45ee-8302-956fae8bbf0d"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("a4ac8b34-4a66-4022-a03f-ce8ca411a38f"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("c1007106-b91c-4bba-9517-d91eec70cc91"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("d29b190b-82f0-4b9a-bed8-002b3e2a464e"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("f474d048-fd43-4795-a86b-1c77f57e2535"));

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
    }
}
