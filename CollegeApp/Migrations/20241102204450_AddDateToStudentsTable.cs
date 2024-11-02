using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CollegeApp.Migrations
{
    /// <inheritdoc />
    public partial class AddDateToStudentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Address", "DOB", "Email", "SutudentName" },
                values: new object[,]
                {
                    { 1, "Türkiye", new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "yunus@gmail.com", "yunus" },
                    { 2, "Türkiye", new DateTime(2024, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "yusuf@gmail.com", "yusuf" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
