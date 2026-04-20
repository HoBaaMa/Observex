using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Observex.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedingRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("089b76d0-bf3b-455a-9604-a8c287e17db2"), "76C6AEBB-A8BB-4DE2-B7BE-5422D77EB7DE", "Worker", "WORKER" },
                    { new Guid("bc3b846f-2562-4493-8959-b663a7804517"), "821E75EF-EC7F-446F-AB73-3AB7EC2B5DC2", "Safety Officer", "SAFETY OFFICER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("089b76d0-bf3b-455a-9604-a8c287e17db2"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("bc3b846f-2562-4493-8959-b663a7804517"));
        }
    }
}
