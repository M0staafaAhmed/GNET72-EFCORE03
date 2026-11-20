using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ef_Core_Assignment_3.Migrations
{
    /// <inheritdoc />
    public partial class DataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name", "ParentCategoryId" },
                values: new object[,]
                {
                    { 1, "All programming topics", "Programming", null },
                    { 2, "Design topics", "Design", null },
                    { 3, null, "Data Science", null }
                });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "Id", "Bio", "Email", "FullName", "JoinDate" },
                values: new object[,]
                {
                    { 1, "Senior .NET developer and trainer.", "ahmed.samir@learn.com", "Ahmed Samir", new DateTime(2022, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Full-stack engineer.", "sara.mahmoud@learn.com", "Sara Mahmoud", new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, null, "khaled.nabil@learn.com", "Khaled Nabil", new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name", "ParentCategoryId" },
                values: new object[,]
                {
                    { 4, "Server-side development", "Backend", 1 },
                    { 5, "Client-side development", "Frontend", 1 }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CategoryId", "Description", "InstructorId", "IsPublished", "Level", "Price", "PublishedDate", "Title" },
                values: new object[,]
                {
                    { 1, 3, "Build REST APIs.", 1, true, "Intermediate", 1200m, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "ASP.NET Core Web API" },
                    { 2, 1, "Start with C#.", 1, true, "Beginner", 600m, new DateTime(2023, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "C# Fundamentals" },
                    { 3, 2, "Deep dive into EF Core.", 2, true, "Advanced", 1500m, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Advanced Entity Framework Core" },
                    { 6, 2, null, 3, false, "Advanced", 2000m, null, "Microservices Architecture" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name", "ParentCategoryId" },
                values: new object[] { 6, null, "UI/UX", 4 });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CategoryId", "Description", "InstructorId", "IsPublished", "Level", "Price", "PublishedDate", "Title" },
                values: new object[] { 4, 4, null, 2, true, "Beginner", 800m, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "React for Beginners" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name", "ParentCategoryId" },
                values: new object[] { 7, "ASP.NET Core", "ASP.NET", 6 });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CategoryId", "Description", "InstructorId", "IsPublished", "Level", "Price", "PublishedDate", "Title" },
                values: new object[] { 5, 6, "Design fundamentals.", 3, false, "Beginner", 700m, null, "UI/UX Design Basics" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
