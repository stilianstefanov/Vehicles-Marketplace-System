using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThinkElectric.Data.Migrations
{
    public partial class SeedReviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "CompanyId", "Content", "CreatedOn", "ProductId", "Rating", "UserId" },
                values: new object[,]
                {
                    { new Guid("2c2a92ec-55d2-46e0-841b-9024c1659cb1"), null, "This is really nice scooter. Really good for hanging around :)", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("253a48e4-63c2-4564-b911-b098f37d8370"), 9.0, new Guid("a44dad0e-bd5f-4394-9f8d-ad5a31e5c24b") },
                    { new Guid("e56a719f-ad51-4bfa-b24b-ead6ef17192c"), new Guid("c0781351-133e-4383-81e7-c95e20fa1273"), "So good company. I recommend their scooters to everyone!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 9.0, new Guid("a44dad0e-bd5f-4394-9f8d-ad5a31e5c24b") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("2c2a92ec-55d2-46e0-841b-9024c1659cb1"));

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("e56a719f-ad51-4bfa-b24b-ead6ef17192c"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("553d4978-b893-4670-8808-3c91d6165c82"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d51d3e2a-27e1-47ca-a592-099baade2dec", "AQAAAAEAACcQAAAAEKpk4WtE4zk2tQ+URe6i/Edv37k/nCV2X2LSX7CiqwHyq6QwQq2qBSMfrLsr5jIH1A==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("95f65567-3392-47fd-ae6a-95d16dfdbfe2"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6f1505f7-1c0f-43d2-8ecd-f322ac66a892", "AQAAAAEAACcQAAAAEGL6ywPLtGQnzCDzJGqC253LJ2KP5k+R0EgJLnqDwU4IY6Ve8Alsl1m5rXFmKfIM/w==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a44dad0e-bd5f-4394-9f8d-ad5a31e5c24b"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f1796d2e-1270-4db3-8a5e-b9c34263fe3b", "AQAAAAEAACcQAAAAEMXA5zOAXuUFH1ywFtulMaeSPA7TROe6x0PdY65ZDhsFCRA2ZEG1z5MFWmljLU022g==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ec09bd2a-4c64-476d-9997-e732562b0ab1"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d672dee8-9247-4685-aed2-d7ef9102807b", "AQAAAAEAACcQAAAAEHJZ6d7ysaCL79feh+qVdkYMBye6BxIocSF7dFoSiSmnUJdXhaioX0SEH9PjiTqxCQ==" });
        }
    }
}
