using Ef_Core_Assignment_3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ef_Core_Assignment_3.Configurations
{
    internal class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.ToTable("Enrollments");

            builder.HasKey(e => new {e.CourseId, e.StudentId});

            builder.Property(e => e.EnrollmentDate).IsRequired();
            builder.Property(e => e.ProgressPercentage).IsRequired();
            builder.Property(e => e.Status).IsRequired().HasConversion<string>().HasMaxLength(20);
            builder.Property(e => e.FinalGrade).HasPrecision(5, 2);

            builder.ToTable(t => t.HasCheckConstraint(
                "CK_Enrollments_ProgressPercentage", 
                "[ProgressPercentage] >= 0 AND [ProgressPercentage] <= 100"));

            builder.HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
