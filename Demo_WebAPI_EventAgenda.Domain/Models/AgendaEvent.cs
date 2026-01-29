using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_WebAPI_EventAgenda.Domain.Models
{
    public class AgendaEvent
    {
        // Proprietes
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string? Location { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public EventCategory Category { get; private set; }

        // Constructeur

        public AgendaEvent(){ }

        public AgendaEvent(string name, string? location, DateTime startDate, DateTime? endDate, EventCategory category)
        {
            if (string.IsNullOrWhiteSpace(name)) 
                throw new ArgumentNullException("Le nom de l'evenement doit contenir au moin un caractere", nameof(name));

            if (endDate is not null && endDate < startDate) 
                throw new ArgumentException("Les dates de l'evenement sont invalides");

            Name = name;
            Location = location;
            StartDate = startDate;
            EndDate = endDate;
            Category = category;
        }
    }
}
