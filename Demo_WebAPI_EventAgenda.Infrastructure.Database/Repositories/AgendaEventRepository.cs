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

        public AgendaEvent GetById(long id)
        {
            return _DbContext.AgendaEvents.Single(ae => ae.Id == id);
        }

        public AgendaEvent Insert(AgendaEvent data)
        {
            // Permet d'ajouter dans le context
            EntityEntry<AgendaEvent> element = _DbContext.AgendaEvents.Add(data);

            // Appliquer la modification du context dans la base de donnee 
            _DbContext.SaveChanges();

            // Renvoyé l'element ajouté à jours
            return element.Entity;

            
        }

        public AgendaEvent Update(long id, AgendaEvent data)
        {
            // Du au pattern "Domaind Driven Development", on devra code des méthodes "update" dans le model
            throw new NotImplementedException();
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
    }
}
