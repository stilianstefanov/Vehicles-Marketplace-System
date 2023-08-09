using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThinkElectric.Data.Migrations
{
    public partial class AddedIsDeletedToPostAndComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Comments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("553d4978-b893-4670-8808-3c91d6165c82"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d3dfd395-f745-4125-a411-a6cf6f115189", "AQAAAAEAACcQAAAAECxcDrkl8t+zMW2fQuOEIpmCp7Ar7xlkyo/bpsesHJjpg3umYMMEeViWY/ZY/UHbtg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("95f65567-3392-47fd-ae6a-95d16dfdbfe2"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "504ed5e5-cb2c-43b8-9d12-11c3c8634bca", "AQAAAAEAACcQAAAAEDPhpFAu1BZq0j1T6yrRNVMALJ+hUa23Nu7nN4ZGUMoBPUcHUHYtN6aYMbYMGwxuGw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a44dad0e-bd5f-4394-9f8d-ad5a31e5c24b"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "853f875b-c446-4e04-bf8e-b0e74e2d9127", "AQAAAAEAACcQAAAAEMeDp8EieSbdicA20aRSeP3mzR7Xux2fV7z2e0Q4Fj9drHTeXWbfzwdUtNFFxJGJDg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ec09bd2a-4c64-476d-9997-e732562b0ab1"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d6b07744-e4df-4d7f-8b06-29390033564a", "AQAAAAEAACcQAAAAEInw4hp7up8kPPnwCphWM3Rmm+TTTNkSgL9cQeNB8uUkOIUmHTqqORWgBi8qbMGY6w==" });
        }
    }
}
