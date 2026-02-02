using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Demo_WebAPI_EventAgenda.Domain.Models
{
    public class Member
    {
        public long Id { get; private set; }
        public string? Pseudonyme { get; private set; }
        public string Email { get; private set; }
        public string? PasswordHash { get; private set; }
        public bool AllowNewsletter { get; private set; }



        public Member() { }


        public Member(string pseudonyme, string email, bool allowNewsletter, string? passwordhash = null) 
        { 
        
            if (pseudonyme != null && (pseudonyme.Trim().Length < 3 || pseudonyme.Trim().Length > 50)) 
            {
                throw new ArgumentException("Le pseudonyme doit contenir au moins 3 caracteres et ne pas plus que 50 caracteres", nameof(pseudonyme));
                
            }

            if (string.IsNullOrWhiteSpace(email) && !MailAddress.TryCreate(email, out _))
            {
                throw new ArgumentException("L'email n'est pas valide", nameof(email));
            }

            if (string.IsNullOrWhiteSpace(passwordhash)) 
            {
                throw new ArgumentException("Le mot de passe n'est pas valide", nameof(passwordhash));
            }

            Pseudonyme = pseudonyme;
            Email = email;
            PasswordHash = passwordhash;
            AllowNewsletter = allowNewsletter;
        }

        public Member(string email, string? pseudonyme, bool allowNewsletter)
        {
            Email = email;
            Pseudonyme = pseudonyme;
            AllowNewsletter = allowNewsletter;
        }
    }

   



}
