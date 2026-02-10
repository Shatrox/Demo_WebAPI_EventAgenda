using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_WebAPI_EventAgenda.Domain.Models
{
    public class AgendaEvent
    {
        // Proprietes
        public long Id { get; private set; }
        public string Name { get; private set; } = default!; // default! = Non nullable, mais pas de valeur par defaut -> necessaire pour EntityFramework
        public string? Desc { get; private set; }
        public string? Location { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public EventCategory Category { get; private set; } = default!;

        // Constructeur

        private AgendaEvent(){ }

        public AgendaEvent(string name, string? desc, string? location, DateTime startDate, DateTime? endDate, EventCategory category)
        {
            if (string.IsNullOrWhiteSpace(name)) 
                throw new ArgumentNullException("Le nom de l'evenement doit contenir au moin un caractere", nameof(name));

            if (endDate is not null && endDate < startDate) 
                throw new ArgumentException("Les dates de l'evenement sont invalides");

            Name = name;
            Desc = desc;
            Location = location;
            StartDate = startDate;
            EndDate = endDate;
            Category = category;
        }

        // Method to change the date
        public AgendaEvent ChangeDate(DateTime startDateUpdate, DateTime? endDateUpdate)
        {
            if(endDateUpdate is not null && endDateUpdate < startDateUpdate)
                throw new ArgumentException("Les dates de l'evenement sont invalides");

            StartDate = startDateUpdate;
            EndDate = endDateUpdate;

            return this; // allows to chain the method call (optional)
        }
    }
}
