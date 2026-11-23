using Ef_Core_Assignment_3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ef_Core_Assignment_3.Configurations
{
    internal class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.ToTable("Instructors");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(i => i.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(i => i.Bio)
                .HasMaxLength(500);

            builder.Property(i => i.JoinDate)
                .IsRequired();

            builder.HasData(
                new Instructor { Id = 1, FullName = "Ahmed Samir", Email = "ahmed.samir@learn.com", Bio = "Senior .NET developer and trainer.", JoinDate = new DateTime(2022, 1, 10) },
                new Instructor { Id = 2, FullName = "Sara Mahmoud", Email = "sara.mahmoud@learn.com", Bio = "Full-stack engineer.", JoinDate = new DateTime(2022, 6, 1) },
                new Instructor { Id = 3, FullName = "Khaled Nabil", Email = "khaled.nabil@learn.com", Bio = null, JoinDate = new DateTime(2023, 3, 15) }
            );
        }
    }
}
