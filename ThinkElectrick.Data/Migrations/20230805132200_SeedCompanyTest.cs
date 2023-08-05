using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThinkElectric.Data.Migrations
{
    public partial class SeedCompanyTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "Street", "ZipCode" },
                values: new object[] { new Guid("b5c6e6d3-1423-4f03-821e-d4438909f0dd"), "Sofia", "Bulgaria", "Vitosha", "1000" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("553d4978-b893-4670-8808-3c91d6165c82"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "adfc921c-7c6c-4a22-a942-2941b0f64fbc", "AQAAAAEAACcQAAAAELNxhZvrY0wRjfE6tUdDFSngAcC3CsdW0CrUeIzvAX9j7TUVpa5/Ro3Dsd7mjI0FIA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("95f65567-3392-47fd-ae6a-95d16dfdbfe2"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7c6d8a6f-44be-4605-ac6a-de175de66b97", "AQAAAAEAACcQAAAAEI3J+SDY0f76MTbhi6M6Kw1Dn3gmQU4lZF/KC3ZzKNziRH+pyu6/4UWwQcMn2LJbMA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a44dad0e-bd5f-4394-9f8d-ad5a31e5c24b"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fb0534e7-210d-429d-a0f6-4fad422c5882", "AQAAAAEAACcQAAAAEOAYuhHB8o2xqiaLumy2WPf/y5jdppdK1JbTlDpmeSG115ovlnO50Y3JbXgVE+rRuw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ec09bd2a-4c64-476d-9997-e732562b0ab1"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3fbe4385-7dc6-4e34-a752-3a3cefb43a7b", "AQAAAAEAACcQAAAAEMHRjUh7R3XFFHDVCEIb5+Up8R2aViWI3AELY6xe3JfgqC3FIrMi84wDdc4v1qbNVQ==" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "AddressId", "Description", "Email", "FoundedDate", "ImageId", "Name", "PhoneNumber", "UserId", "Website" },
                values: new object[] { new Guid("c0781351-133e-4383-81e7-c95e20fa1273"), new Guid("b5c6e6d3-1423-4f03-821e-d4438909f0dd"), "Vistaz is a company that sells electric scooters and bikes. Really good quality!", "vistaz@abv.bg", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "64ce4237f1bda0c4b930c421", "Vistaz", "0888888888", new Guid("95f65567-3392-47fd-ae6a-95d16dfdbfe2"), "https://vistazExample.bg/" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("c0781351-133e-4383-81e7-c95e20fa1273"));

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("b5c6e6d3-1423-4f03-821e-d4438909f0dd"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("553d4978-b893-4670-8808-3c91d6165c82"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d7af8a10-f8ac-4beb-a8a4-6a43869eedf9", "AQAAAAEAACcQAAAAECku7VJoc5+nxe6pd1t4F5LEvqXDsYi2Qn14ugbmu3gWmVUoEVmMXH4WCIOlEQ74VA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("95f65567-3392-47fd-ae6a-95d16dfdbfe2"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "90365dfd-0493-431f-8ba2-13d9b873b743", "AQAAAAEAACcQAAAAEIcg68pJQmcDmSRNJApVCe3nC7baRpsegQqbJVHv4Sc8Tt01AM+BBBf4It57Ltcf4w==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a44dad0e-bd5f-4394-9f8d-ad5a31e5c24b"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8e383082-c90b-45b9-83b9-1f6d30189aec", "AQAAAAEAACcQAAAAEJhrJpIcwVlsyJHFbZBSAWBTl3jNqfveb/YowAXWCL/LChIA2EtseQDNijY5irl8Zg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ec09bd2a-4c64-476d-9997-e732562b0ab1"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b1c7f0eb-b819-4b3e-89ca-b1481d0a098f", "AQAAAAEAACcQAAAAEB25VUW9jGqqbRzYa8RcWRQhu7vKweDhOJit/upOhfprd06vGRzL/w4z7dYJb27MPw==" });
        }
    }
}
