using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThinkElectric.Data.Migrations
{
    public partial class AddedIsBlockedProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("553d4978-b893-4670-8808-3c91d6165c82"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a4bfe76e-de09-4385-bca9-989ee6ee745c", "AQAAAAEAACcQAAAAEOx5D58X7N48HEJyPAjpVw5nA+On14nOsVl3h7RPzlCRehQY0eXwRCuvm5UaHOJILA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("95f65567-3392-47fd-ae6a-95d16dfdbfe2"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "72af9962-241d-4a2d-ab08-221ed202ca89", "AQAAAAEAACcQAAAAELUWSCGdD5JRPG3pgq6iVkOy+OW2T2CsXboRJ49Y/R0UdWeJTodDHyWr8OKBNFbfhQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a44dad0e-bd5f-4394-9f8d-ad5a31e5c24b"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4452d379-9773-4604-95b4-df83dcdc9aac", "AQAAAAEAACcQAAAAEOPx7nc1ARqF6RnJYJ3T2XB7nFiA2NJba8s2XwZgCVrVFPq1TKfSgKEXyN2iIdei2w==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ec09bd2a-4c64-476d-9997-e732562b0ab1"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1dbcd94b-ac3b-4c07-91b7-8865908b6b49", "AQAAAAEAACcQAAAAEGJiZc3WWBRu5gMpIcGtqFea4+72cL3vrGDvyaJSZyCJ/ugo9vuHix/G4jfTvEzt+w==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "AspNetUsers");

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
        }
    }
}
