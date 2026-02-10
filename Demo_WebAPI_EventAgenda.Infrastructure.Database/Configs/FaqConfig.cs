using Demo_WebAPI_EventAgenda.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_WebAPI_EventAgenda.Infrastructure.Database.Configs
{
    public class FaqConfig : IEntityTypeConfiguration<FAQ>
    {
        public void Configure(EntityTypeBuilder<FAQ> builder)
        {
            // Tables
            builder.ToTable("FAQs");

            // Primary Key
            builder.HasKey(f => f.Id)
                .HasName("PK_FAQs")
                .IsClustered(true);

            // Collumns
            builder.Property(f => f.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

            builder.Property(f=> f.Question)
                .HasColumnName("Question")
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(500);

            builder.Property(f => f.Answer)
                .HasColumnName("Answer")
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(2000);

            builder.Property(f => f.IsVisible)
                .HasColumnName("IsVisible")
                .IsRequired();

            // Indexes

            // Allows fast retrieval of FAQs based on specific keywords in the question, improving search performance.
            builder.HasIndex(f => f.Question)
                .HasDatabaseName("IDX_FAQs_Question");
                
        }
    }
}
