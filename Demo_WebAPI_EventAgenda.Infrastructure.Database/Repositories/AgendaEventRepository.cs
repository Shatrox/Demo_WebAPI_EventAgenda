using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Repositories;
using Demo_WebAPI_EventAgenda.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_WebAPI_EventAgenda.Infrastructure.Database.Repositories
{
    public class AgendaEventRepository : IAgendaEventRepository
    {
        // ↓ Injection (DI) du DbContext dans le Repository (Props + Ctor)
        private readonly AppDbContext _DbContext;

        public AgendaEventRepository(AppDbContext dbContext) 
        { 
        
            _DbContext = dbContext;
        
        }

        //↓ Implementation du repository
        public IEnumerable<AgendaEvent> GetAll(int offset, int limit)
        {
            return _DbContext.AgendaEvents
                .AsNoTracking() // Permet de ne pas tracker les entités (gain de perf en lecture seule)
                .Skip(offset) // Permet de ne pas prendre les X premiers
                .Take(limit)  // Permet deselectionne les Y rows
                .ToList();
        }

        public AgendaEvent? GetById(long id)
        {
            return _DbContext.AgendaEvents
                .Include(ae => ae.Category)
                .SingleOrDefault(ae => ae.Id == id);
        }

        public AgendaEvent Insert(AgendaEvent data)
        {
            // Check si l'element existe déjà
            EventCategory? categoryInDB = _DbContext.EventCategories.SingleOrDefault(c => c.Name == data.Category.Name);

            // Recreer le element a ajouter en Db avec le lien vers la category si elle existe
            AgendaEvent dataToInsert = new AgendaEvent(
                data.Name,
                data.Desc,
                data.Location,
                data.StartDate,
                data.EndDate,
                categoryInDB ?? data.Category // Coalesce → Si la category existe on l'utilise sinon on utilise celle de l'element a ajouter
            );


            // Permet d'ajouter dans le context
            EntityEntry<AgendaEvent> element = _DbContext.AgendaEvents.Add(dataToInsert);

            // Appliquer la modification du context dans la base de donnee 
            _DbContext.SaveChanges();

            // Renvoyé l'element ajouté à jours
            return element.Entity;

            
        }

        public AgendaEvent Update(AgendaEvent data)
        {
            // Permet d'ajouter dans le context
            EntityEntry<AgendaEvent> result = _DbContext.AgendaEvents.Update(data);

            // Appliquer la modification du context dans la base de donnee 
            _DbContext.SaveChanges();

            // Renvoyé l'element ajouté à jours
            return result.Entity;
            
        }

        public bool Delete(long id)
        {
            AgendaEvent? target = GetById(id);

            if (target is null)
                return false;

            _DbContext.Remove(target);
            _DbContext.SaveChanges();

            return true;

        }

        public IEnumerable<AgendaEvent> GetByDate(DateTime startDate, DateTime? endDate = null)
        {
            DateTime currentDate = endDate ?? startDate;
           
             
                var result = _DbContext.AgendaEvents
                    .AsNoTracking()
                    .Where(ae => ae.StartDate <= currentDate && ae.EndDate >= startDate)
                    .ToList();

                return result;
            
        }
    }
}
