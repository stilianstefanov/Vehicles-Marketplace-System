using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThinkElectric.Data.Migrations
{
    public partial class SeedPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("553d4978-b893-4670-8808-3c91d6165c82"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a3285745-9e41-4458-adf6-d244b56cc3ea", "AQAAAAEAACcQAAAAEJ/FwfAvundRI8qm7+pKytWDSTQsn50NLuNiAHData7+Vc0Kqng0QAdUgM3l/+zH1g==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("95f65567-3392-47fd-ae6a-95d16dfdbfe2"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cb191c9a-f51c-4883-9462-4d304e31bf50", "AQAAAAEAACcQAAAAEFERDw87pIKGS8xcpnLVnBK8tP34mNq90AnDW+UM46AetOW553M1BVfeYOkSKIepHA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a44dad0e-bd5f-4394-9f8d-ad5a31e5c24b"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ce5ecb8a-b67c-459d-a10d-f8595a6abea8", "AQAAAAEAACcQAAAAEBjgPbaVhF4Ruzajx12jkH5+A7Gh6kLto7WsFEMkqCE/ChX30CQO7vTZ065VFd2aFA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ec09bd2a-4c64-476d-9997-e732562b0ab1"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8bf64a82-873c-4820-b363-dc99cfff0465", "AQAAAAEAACcQAAAAEHvFVh+FyezRlF6RSppPhhxk5CVmI2lq8rMgCgCJ7Q4PlDUMMI5z4ra5rE9ocddtRg==" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "CreatedOn", "IsDeleted", "Title", "UserId" },
                values: new object[,]
                {
                    { new Guid("bd96c711-32e7-483b-b1fe-9c19c1e0a936"), "The Wolf Warrior 11 is the most powerful, all terrain electric scooter from Kaabo. It has dual 1200W brushless motors, hydraulic brakes, 11 inch off road tires and a massive 35Ah battery for a maximum range of 150 km. The Wolf Warrior 11 is the ultimate electric scooter for extreme riding.", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Kaabo Wolf Warrior 11 opinions and thoughts", new Guid("553d4978-b893-4670-8808-3c91d6165c82") },
                    { new Guid("c202cb91-30e9-49e2-8c15-d12eec51b3f7"), "Vsett 8 is the most powerful, all terrain electric scooter from Vsett. It has dual 1200W brushless motors, hydraulic brakes, 11 inch off road tires and a massive 35Ah battery for a maximum range of 150 km. The vsett 8 is the ultimate electric scooter for extreme riding.", new DateTime(2021, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Vsett 8", new Guid("95f65567-3392-47fd-ae6a-95d16dfdbfe2") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("bd96c711-32e7-483b-b1fe-9c19c1e0a936"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("c202cb91-30e9-49e2-8c15-d12eec51b3f7"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("553d4978-b893-4670-8808-3c91d6165c82"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "51a9bb64-96ad-4dda-8673-49926d5d1bd3", "AQAAAAEAACcQAAAAEMK5EciZ8Nze/a2UX/ne5NXKop9mFRTZXbMwFfLk9MTGVrhxlYtgD4jhLtsgVcw6oA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("95f65567-3392-47fd-ae6a-95d16dfdbfe2"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "eb78312c-07f8-4644-abcd-c89f0b76581b", "AQAAAAEAACcQAAAAEO2wwlv3nySYzr2oGpgaLJMaUn1ruZ4DooHTqAbppMZ7CzBGYsk+GtEoB4JMZxWCsw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a44dad0e-bd5f-4394-9f8d-ad5a31e5c24b"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "99d1c953-ba0e-472f-8011-3cd300c3450f", "AQAAAAEAACcQAAAAEAhFvgfrHQRDQ0bMEgu71CtGAetRySzObGU8LZdOiiD+n/dorO46fV3paITi+/tgiw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ec09bd2a-4c64-476d-9997-e732562b0ab1"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "507f35b2-e34d-481b-9f6f-f48988a4989f", "AQAAAAEAACcQAAAAENozsEeMhr575etHveu9mlMLaXeOjcYqPbT4tAjUkBSzTSrzYZ/WEmSTgsvvHB/MBA==" });
        }
    }
}
