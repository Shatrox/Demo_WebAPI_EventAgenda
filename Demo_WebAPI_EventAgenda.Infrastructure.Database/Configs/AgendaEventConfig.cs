using Demo_WebAPI_EventAgenda.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_WebAPI_EventAgenda.Infrastructure.Database.Configs
{
    public class AgendaEventConfig : IEntityTypeConfiguration<AgendaEvent>
    {
        public void Configure(EntityTypeBuilder<AgendaEvent> builder)
        {
            // Tables
            builder.ToTable("Agenda_Events");

            // Clef
            builder.HasKey(ae => ae.Id)
                .HasName("PK_Agenda_Events")
                .IsClustered(true);

            // Collumns
            builder.Property(ae => ae.Id)
                .ValueGeneratedOnAdd();

            builder.Property(ae => ae.Name)
                .HasMaxLength(250)
                .IsUnicode() // NVARCHAR
                .IsRequired();

            builder.Property(ae => ae.Location)
                .HasMaxLength(100)
                .IsUnicode() // NVARCHAR
                .IsRequired(false);

            builder.Property(ae => ae.StartDate)
                .IsRequired(true)
                .HasColumnType("DATETIME2")
                .HasColumnName("Start Date");

            builder.Property(ae => ae.EndDate)
                .IsRequired (false)
                .HasColumnType("DATETIME2")
                .HasColumnName("End Date");

            builder.Property(ae => ae.Category)
                .IsRequired(true);

            // Indexes

            //Prevent duplicate events with same name, location and start date
            builder.HasIndex(ae => new { ae.Name, ae.Location, ae.StartDate })
                .IsUnique()
                .HasDatabaseName("IDX_Agenda_Events__Name_Loc_Date");

            // Relations

            builder
                .HasOne(ae => ae.Category) // Link "ae -> c" 
                .WithMany()               // Link "c -> ae"
                .HasForeignKey("Category_Id") // Foreign Key in AgendaEvent table
                .HasConstraintName("FK_Agenda_Events_Event__Categories")
                .IsRequired();



        }

    }
}
