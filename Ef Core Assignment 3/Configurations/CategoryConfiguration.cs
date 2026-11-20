using Ef_Core_Assignment_3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ef_Core_Assignment_3.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Description)
                .HasMaxLength(500);

            builder.HasOne(c => c.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new Category { Id = 1, Name = "Programming", Description = "All programming topics", ParentCategoryId = null },
                new Category { Id = 2, Name = "Design", Description = "Design topics", ParentCategoryId = null },
                new Category { Id = 3, Name = "Data Science", Description = null, ParentCategoryId = null },
                new Category { Id = 4, Name = "Backend", Description = "Server-side development", ParentCategoryId = 1 },
                new Category { Id = 5, Name = "Frontend", Description = "Client-side development", ParentCategoryId = 1 },
                new Category { Id = 6, Name = "UI/UX", Description = null, ParentCategoryId = 4 },
                new Category { Id = 7, Name = "ASP.NET", Description = "ASP.NET Core", ParentCategoryId = 6 }
            );
        }
    }
}
