﻿using Fina.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fina.API.Data.Mappings
{
    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transaction");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(t => t.Type)
                .IsRequired()
                .HasColumnType("SMALLINT");

            builder.Property(t => t.Amount)
                .IsRequired()
                .HasColumnType("DECIMAL");

            builder.Property(t => t.CreatedAt)
                .IsRequired();

            builder.Property(t => t.PaidOrReceivedAt)
                .IsRequired();

            builder.Property(t => t.UserId)
                .IsRequired()
                .HasColumnType("VARCHAR")
                .HasMaxLength(160);


        }
    }
}
