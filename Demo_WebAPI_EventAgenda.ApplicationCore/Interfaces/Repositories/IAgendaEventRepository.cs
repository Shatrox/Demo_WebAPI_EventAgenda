using Demo_WebAPI_EventAgenda.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Repositories
{
    public interface IAgendaEventRepository
    {
        // CRUD Operations - Create, Read, Update, Delete
        AgendaEvent GetById(long id); // Read
        IEnumerable<AgendaEvent> GetAll(int offset, int limit); // offset = page number, limit = page size // Read range
        AgendaEvent Insert(AgendaEvent data); // Create
        AgendaEvent Update(AgendaEvent data); // Update
        bool Delete(long id); // Delete
        IEnumerable<AgendaEvent> GetByDate(DateTime startDate, DateTime? endDate = null); // Read by specific date


    }
}
