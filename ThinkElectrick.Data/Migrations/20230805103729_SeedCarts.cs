using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThinkElectric.Data.Migrations
{
    public partial class SeedCarts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "UserId" },
                values: new object[] { new Guid("d6271c21-caa6-46a3-8776-d3152f6a1432"), new Guid("a44dad0e-bd5f-4394-9f8d-ad5a31e5c24b") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: new Guid("d6271c21-caa6-46a3-8776-d3152f6a1432"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("553d4978-b893-4670-8808-3c91d6165c82"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "29b78b1d-adcb-4d85-b5c5-95fb1afae60b", "AQAAAAEAACcQAAAAECrKR3xMyuVhll5J5SDjzXEU4mH0q6jxcC9oPC+oyNFlvilwLXZeStqxgc/RBcQ7bQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("95f65567-3392-47fd-ae6a-95d16dfdbfe2"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e65c3ccc-319c-4bdb-9c25-d45be3b25dbe", "AQAAAAEAACcQAAAAEBl/eUFdwQaMCtDdh9jqswO4WmTLettXxrXdfqbpk67g6fBQOdHLy7f9RUb+jbpZmA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a44dad0e-bd5f-4394-9f8d-ad5a31e5c24b"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "dcc0b1ae-9b27-46f8-8131-9be049ef8e7c", "AQAAAAEAACcQAAAAEODnInEnOJhl4eSWcfVXBN4vDcmW9262qbqwMhMIyxdJV4mC0ohgRBr5+XrWFZ9new==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ec09bd2a-4c64-476d-9997-e732562b0ab1"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "09b89283-2131-47c7-b357-8a32426e837d", "AQAAAAEAACcQAAAAEJhSDpr8BSNodCzTScUio+6wwY/l/493IeX1EdtNFwuKJuiG5V7XOgMCntVjUmBd7w==" });
        }
    }
}
