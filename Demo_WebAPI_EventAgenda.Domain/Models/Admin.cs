using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_WebAPI_EventAgenda.Domain.Models
{
    public class Admin
    {
        public long Id { get; private set; }
        public string Username { get; private set; }

        public Admin() { }


        public Admin(string username)
        {
            if (string.IsNullOrWhiteSpace(username) || username.Trim().Length < 3 || username.Trim().Length > 20)
            {
                throw new ArgumentException("Le nom d'utilisateur doit contenir entre 3 et 20 caracteres", nameof(username));
            }
            Username = username;
        }




    }

    
}
