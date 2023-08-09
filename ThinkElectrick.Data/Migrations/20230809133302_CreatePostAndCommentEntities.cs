using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThinkElectric.Data.Migrations
{
    public partial class CreatePostAndCommentEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Posts");

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
    }
}
