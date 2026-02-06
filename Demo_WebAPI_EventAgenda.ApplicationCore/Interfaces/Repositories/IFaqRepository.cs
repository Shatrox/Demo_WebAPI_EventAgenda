using Demo_WebAPI_EventAgenda.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Repositories
{
    public interface IFaqRepository
    {
        // CRUD Operations - Create, Read, ?Update, ?Delete
         FAQ GetById(long id); // Read
         FAQ CreateFAQ(FAQ data); // This creates a new FAQ entry 
        
    }
}
