﻿using Fina.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fina.API.Data.Mappings
{
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");    

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title).
                IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(c => c.Description)
                .IsRequired(false)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(c => c.UserId)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(160);
            
        }
    }
}
