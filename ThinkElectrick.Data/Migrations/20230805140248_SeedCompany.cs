using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThinkElectric.Data.Migrations
{
    public partial class SeedCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "Street", "ZipCode" },
                values: new object[] { new Guid("283a2377-59ae-491e-b70f-ce5f2643e563"), "Varna", "Bulgaria", "Levski", "9000" });

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

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "AddressId", "Description", "Email", "FoundedDate", "ImageId", "Name", "PhoneNumber", "UserId", "Website" },
                values: new object[] { new Guid("5d652ebf-7b4d-430c-9af8-0c02b0256575"), new Guid("283a2377-59ae-491e-b70f-ce5f2643e563"), "Green Company is a company that sells high quality electric scooters bikes and accessories. Enjoy our products!", "GreenCompany@abv.bg", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "64ce53d31dee708ccae9319d", "Green Company", "0777777777", new Guid("553d4978-b893-4670-8808-3c91d6165c82"), "https://GreenCompanyExample.bg/" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5d652ebf-7b4d-430c-9af8-0c02b0256575"));

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("283a2377-59ae-491e-b70f-ce5f2643e563"));

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
        }
    }
}
