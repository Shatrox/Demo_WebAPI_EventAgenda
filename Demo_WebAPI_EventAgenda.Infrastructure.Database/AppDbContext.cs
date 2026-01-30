using Demo_WebAPI_EventAgenda.Domain.Models;
using Demo_WebAPI_EventAgenda.Infrastructure.Database.Configs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Demo_WebAPI_EventAgenda.Infrastructure.Database
{
    // Configuration for the database context EFCore
    public class AppDbContext : DbContext
    {
        //Ensemble the tables (DbSet) that will be part of the database
        public DbSet<AgendaEvent> AgendaEvents { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }


        //Apply the configurations defined in separate classes
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Charge the classes that implement IEntityTypeConfiguration
            // Add all configurations by hand
            /*
            modelBuilder.ApplyConfiguration(new AgendaEventConfig());
            modelBuilder.ApplyConfiguration(new EventCategoryConfig());
            */
            // Or automatically load all configurations from the assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
           
        }


    }
}
