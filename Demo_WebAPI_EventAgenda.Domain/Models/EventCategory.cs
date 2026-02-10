using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_WebAPI_EventAgenda.Domain.Models
{
    public class EventCategory
    {
        // Proprietes
        public long Id { get; private set; }
        public string Name { get; private set; } = default!; // default! = Non nullable, mais pas de valeur par defaut -> necessaire pour EntityFramework

        // Constructeur
        // Vide -> Necessaire pour EntityFrameWork
        private EventCategory(){ }

        // Parametres -> Avec Validation
        public EventCategory(string name)
        {
            if (name.Trim().Length < 3) 
            {
                throw new ArgumentException("Nom dela categorie doit contenir au moins 3 caracteres", nameof(name));
            }

            Name = name.Trim();
        }


    }
}
