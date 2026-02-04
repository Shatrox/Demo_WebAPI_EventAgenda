using Demo_WebAPI_EventAgenda.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Services
{
    public interface IAgendaEventService
    {
        AgendaEvent GetById (long id); // get event by its unique identifier

        IEnumerable<AgendaEvent> GetMany(int page, int nbElement); // same use as offset and limit
        IEnumerable<AgendaEvent> GetAllByDate(DateTime date); // get all events for a specific date
        IEnumerable<AgendaEvent> GetAllByDateRange(DateTime startDate, DateTime endDate); // get all events within a date range

        AgendaEvent Create(AgendaEvent data); // create a new event
        void UpdateDate(long id, DateTime startDate, DateTime? endDate); // update event dates
        void Delete(long id); // delete an event by its unique identifier 

    }
}
