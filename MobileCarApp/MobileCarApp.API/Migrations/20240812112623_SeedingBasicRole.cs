using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MobileCarApp.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingBasicRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a628c6d6-1721-4916-a9fe-7b457aec111f", null, "Administrator", "ADMINISTRATOR" },
                    { "cde0108b-e6ff-4971-9123-225183934a7d", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "9137d278-b467-4653-b166-310194cdf2fa", 0, "d85427dd-8bd1-4442-b30d-075f71ce4991", "user@localhost.com", true, false, null, "USER@LOCALHOST.COM", "USER", "AQAAAAIAAYagAAAAEJDC0tNuvNrZEY/cbZorAsBZDvnSkadERCyx54ErB38Mv2J1Uwlg35CjNzoTUYiejw==", null, false, "44387d47-25bd-4aae-bc88-848c282a92f3", false, "User" },
                    { "b0e35b3d-3f80-4b51-8fc5-384849622625", 0, "440da67a-b4f9-436a-b54c-0ae5d902efa1", "admin@localhost.com", true, false, null, "ADMIN@LOCALHOST.COM", "ADMIN", "AQAAAAIAAYagAAAAEE9W+9FvCyuKHtACE5+vx9R+kv1KMFiM8mNYyeXLu9KMhJ7HZKPQuFlT6hSBpRPuqQ==", null, false, "26a286ac-8793-4a55-8a3b-7e34e1a4a59c", false, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "cde0108b-e6ff-4971-9123-225183934a7d", "9137d278-b467-4653-b166-310194cdf2fa" },
                    { "a628c6d6-1721-4916-a9fe-7b457aec111f", "b0e35b3d-3f80-4b51-8fc5-384849622625" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "cde0108b-e6ff-4971-9123-225183934a7d", "9137d278-b467-4653-b166-310194cdf2fa" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a628c6d6-1721-4916-a9fe-7b457aec111f", "b0e35b3d-3f80-4b51-8fc5-384849622625" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a628c6d6-1721-4916-a9fe-7b457aec111f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cde0108b-e6ff-4971-9123-225183934a7d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9137d278-b467-4653-b166-310194cdf2fa");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b0e35b3d-3f80-4b51-8fc5-384849622625");
        }
    }
}
