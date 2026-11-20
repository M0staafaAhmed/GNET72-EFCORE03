using Ef_Core_Assignment_3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ef_Core_Assignment_3.Configurations
{
    internal class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.Description)
                .HasMaxLength(1000);

            builder.Property(c => c.Price)
                .IsRequired()
                .HasPrecision(10, 2);

            builder.Property(c => c.Level)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(c => c.IsPublished)
                .IsRequired();

            builder.Property(c => c.PublishedDate)
                .IsRequired(false);

            builder.HasOne(c => c.Instructor)
                .WithMany(i => i.Courses)
                .HasForeignKey(c => c.InstructorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Category)
                .WithMany(cat => cat.Courses)
                .HasForeignKey(c => c.CategoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new Course { Id = 1, Title = "ASP.NET Core Web API", Description = "Build REST APIs.", Price = 1200m, Level = CourseLevel.Intermediate, IsPublished = true, PublishedDate = new DateTime(2024, 1, 5), InstructorId = 1, CategoryId = 3 },
                new Course { Id = 2, Title = "C# Fundamentals", Description = "Start with C#.", Price = 600m, Level = CourseLevel.Beginner, IsPublished = true, PublishedDate = new DateTime(2023, 11, 20), InstructorId = 1, CategoryId = 1 },
                new Course { Id = 3, Title = "Advanced Entity Framework Core", Description = "Deep dive into EF Core.", Price = 1500m, Level = CourseLevel.Advanced, IsPublished = true, PublishedDate = new DateTime(2024, 2, 12), InstructorId = 2, CategoryId = 2 },
                new Course { Id = 4, Title = "React for Beginners", Description = null, Price = 800m, Level = CourseLevel.Beginner, IsPublished = true, PublishedDate = new DateTime(2024, 3, 1), InstructorId = 2, CategoryId = 4 },
                new Course { Id = 5, Title = "UI/UX Design Basics", Description = "Design fundamentals.", Price = 700m, Level = CourseLevel.Beginner, IsPublished = false, PublishedDate = null, InstructorId = 3, CategoryId = 6 },
                new Course { Id = 6, Title = "Microservices Architecture", Description = null, Price = 2000m, Level = CourseLevel.Advanced, IsPublished = false, PublishedDate = null, InstructorId = 3, CategoryId = 2 }
            );


        }
    }
}
