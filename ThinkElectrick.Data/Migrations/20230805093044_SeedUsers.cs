using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThinkElectric.Data.Migrations
{
    public partial class SeedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AddressId", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("553d4978-b893-4670-8808-3c91d6165c82"), 0, null, "29b78b1d-adcb-4d85-b5c5-95fb1afae60b", "companyuser2@abv.bg", false, "CompanyUser2", "CompanyUser2", false, null, "COMPANYUSER2@ABV.BG", "COMPANYUSER2@ABV.BG", "AQAAAAEAACcQAAAAECrKR3xMyuVhll5J5SDjzXEU4mH0q6jxcC9oPC+oyNFlvilwLXZeStqxgc/RBcQ7bQ==", "0777777777", false, "1b4f4eba-02fd-4319-8d83-b31e7e689ffb", false, "companyuser2@abv.bg" },
                    { new Guid("95f65567-3392-47fd-ae6a-95d16dfdbfe2"), 0, null, "e65c3ccc-319c-4bdb-9c25-d45be3b25dbe", "companyuser1@abv.bg", false, "CompanyUser1", "CompanyUser1", false, null, "COMPANYUSER1@ABV.BG", "COMPANYUSER1@ABV.BG", "AQAAAAEAACcQAAAAEBl/eUFdwQaMCtDdh9jqswO4WmTLettXxrXdfqbpk67g6fBQOdHLy7f9RUb+jbpZmA==", "0999999999", false, "4a89bb41-fdff-4521-a0dc-ca8c4fc3ac79", false, "companyuser1@abv.bg" },
                    { new Guid("a44dad0e-bd5f-4394-9f8d-ad5a31e5c24b"), 0, null, "dcc0b1ae-9b27-46f8-8131-9be049ef8e7c", "buyeruser@abv.bg", false, "BuyerUser", "BuyerUser", false, null, "BUYERUSER@ABV.BG", "BUYERUSER@ABV.BG", "AQAAAAEAACcQAAAAEODnInEnOJhl4eSWcfVXBN4vDcmW9262qbqwMhMIyxdJV4mC0ohgRBr5+XrWFZ9new==", "0666666666", false, "8e6461b0-51dc-464f-90c7-6cc66272dae9", false, "buyeruser@abv.bg" },
                    { new Guid("ec09bd2a-4c64-476d-9997-e732562b0ab1"), 0, null, "09b89283-2131-47c7-b357-8a32426e837d", "admin@abv.bg", false, "Admin", "Admin", false, null, "ADMIN@ABV.BG", "ADMIN@ABV.BG", "AQAAAAEAACcQAAAAEJhSDpr8BSNodCzTScUio+6wwY/l/493IeX1EdtNFwuKJuiG5V7XOgMCntVjUmBd7w==", "0888888888", false, "e6806919-0f8b-4ab0-86cd-186d4fda3c5b", false, "admin@abv.bg" }
                });
            //5d652ebf-7b4d-430c-9af8-0c02b0256575
            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "UserId", "ClaimType", "ClaimValue" },
                    values: new object[,]
                        {
                        { 1, new Guid("553d4978-b893-4670-8808-3c91d6165c82"), "companyUser", "" },
                        { 2, new Guid("553d4978-b893-4670-8808-3c91d6165c82"), "FullName", "CompanyUser2 CompanyUser2" },
                        { 3, new Guid("553d4978-b893-4670-8808-3c91d6165c82"), "companyId", "5d652ebf-7b4d-430c-9af8-0c02b0256575" },
                        { 4, new Guid("95f65567-3392-47fd-ae6a-95d16dfdbfe2"), "companyUser", "" },
                        { 5, new Guid("95f65567-3392-47fd-ae6a-95d16dfdbfe2"), "FullName", "CompanyUser1 CompanyUser1" },
                        { 6, new Guid("95f65567-3392-47fd-ae6a-95d16dfdbfe2"), "companyId", "c0781351-133e-4383-81e7-c95e20fa1273" },
                        { 7, new Guid("a44dad0e-bd5f-4394-9f8d-ad5a31e5c24b"), "FullName", "BuyerUser BuyerUser" },
                        { 8, new Guid("a44dad0e-bd5f-4394-9f8d-ad5a31e5c24b"), "cartId", "d6271c21-caa6-46a3-8776-d3152f6a1432" },
                        });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("553d4978-b893-4670-8808-3c91d6165c82"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("95f65567-3392-47fd-ae6a-95d16dfdbfe2"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a44dad0e-bd5f-4394-9f8d-ad5a31e5c24b"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ec09bd2a-4c64-476d-9997-e732562b0ab1"));
        }
    }
}
