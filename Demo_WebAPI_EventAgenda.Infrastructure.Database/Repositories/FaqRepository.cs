using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Repositories;
using Demo_WebAPI_EventAgenda.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_WebAPI_EventAgenda.Infrastructure.Database.Repositories
{
    public class FaqRepository : IFaqRepository
    {
        private readonly AppDbContext _DbContext;

        public FaqRepository(AppDbContext dbContext)
        {
            _DbContext = dbContext;
        }   

        public FAQ CreateFAQ(FAQ data)
        {
            FAQ faq = _DbContext.FAQs.SingleOrDefault(f => f.Question == data.Question); // This 
            
            if (faq is not null)
            {
                throw new Exception("FAQ with the same question already exists.");
            }

            FAQ dataToInsert = new FAQ
            (
                data.Question,
                data.Answer,
                data.IsVisible
           );

            EntityEntry<FAQ> element = _DbContext.FAQs.Add(dataToInsert);

            _DbContext.SaveChanges();

            return element.Entity;
        }

        public FAQ? GetById(long id) // This method retrieves a FAQ by its ID, including the related Question and Answer entities.
        {
            return _DbContext.FAQs
                .SingleOrDefault(f => f.Id == id);
        }
    }
}
