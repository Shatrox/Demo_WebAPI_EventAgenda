using Demo_WebAPI_EventAgenda.Domain.Enums;
using Demo_WebAPI_EventAgenda.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_WebAPI_EventAgenda.Infrastructure.Database.Configs
{
    public class MemberConfig : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            // Tables
            builder.ToTable("Members");

            // Primary Key
            builder.HasKey(m => m.Id)
                .HasName("PK_Members")
                .IsClustered(true); // IsClustered means the primary key is stored in a clustered index

            // Columns
            builder.Property(m => m.Id)
                .ValueGeneratedOnAdd();

            builder.Property(m => m.Pseudonyme)
                .HasMaxLength(50)
                .HasColumnName("Pseudonyme")
                .IsUnicode()
                .IsRequired(false);

            builder.Property(m => m.Email)
                .HasMaxLength(320)
                .HasColumnName("Email")
                .IsUnicode(false)
                .IsRequired();

            builder.Property(m => m.PasswordHash)
                .HasMaxLength(200)
                .HasColumnName("PasswordHash")
                .IsRequired();

            builder.Property(m => m.AllowNewsletter)
                .HasColumnName("AllowNewsletter")
                .IsRequired();

            builder.Property(m => m.Role)
                .HasConversion<string>() // Store the enum as a string in the database
                .HasDefaultValue(MemberRole.Peon) // Set the default value to "Peon" for the members already in the database
                .HasSentinel(0) // Set the sentinel value to 0, which is not defined in the enum, to prevent invalid data
                .HasMaxLength(20)
                .HasColumnName("Role")
                .IsUnicode(false)
                .IsRequired();

            // Indexes

            // ↓ Prevent duplicate emails
            builder.HasIndex(m => m.Email)
                .HasDatabaseName("IDX_Members__Email")
                .IsUnique();

            // ↓ Prevent duplicate pseudonymes
            builder.HasIndex(m => m.Pseudonyme)
                .HasDatabaseName("IDX_Members_Pseudonyme")
                .IsUnique();
        }
    }
}
