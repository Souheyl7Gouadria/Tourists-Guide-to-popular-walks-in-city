using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Application.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingdataforDifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("a2f4c3d8-1e5b-4c3a-9f1e-1b2c3d4e5f60"), "Easy" },
                    { new Guid("b3f5d4e9-2f6c-5d4b-af2f-2c3d4e5f6a70"), "Medium" },
                    { new Guid("c4a6e5f0-3a7d-6e5c-1b3a-3d4e5f6a7b80"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("d5b7f6a1-4b8e-7f6d-2c4b-4e5f6a7b8c90"), "US-W", "West Coast", null },
                    { new Guid("e6c8a7b2-5c9f-8a7e-3d5c-5f6a7b8c9d01"), "US-E", "East Coast", null },
                    { new Guid("f7d9b8c3-6d0a-9b8f-4e6d-6a7b8c9d0e12"), "US-M", "Midwest", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("a2f4c3d8-1e5b-4c3a-9f1e-1b2c3d4e5f60"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("b3f5d4e9-2f6c-5d4b-af2f-2c3d4e5f6a70"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("c4a6e5f0-3a7d-6e5c-1b3a-3d4e5f6a7b80"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("d5b7f6a1-4b8e-7f6d-2c4b-4e5f6a7b8c90"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("e6c8a7b2-5c9f-8a7e-3d5c-5f6a7b8c9d01"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f7d9b8c3-6d0a-9b8f-4e6d-6a7b8c9d0e12"));
        }
    }
}
