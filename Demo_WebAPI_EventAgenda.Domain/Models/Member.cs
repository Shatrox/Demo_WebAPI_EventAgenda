using Demo_WebAPI_EventAgenda.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text;

namespace Demo_WebAPI_EventAgenda.Domain.Models
{
    public class Member
    {
        public long Id { get; private set; }
        public string Email { get; private set; } = default!;
        public string? Pseudonyme { get; private set; }
        public bool AllowNewsletter { get; private set; }
        public string? PasswordHash { get; private set; }
        public MemberRole Role { get; private set; } 




        private Member() { }


        public Member(string email, string? pseudonyme, bool allowNewsletter, string? passwordhash = null) 
        { 
        
            if (pseudonyme != null && (pseudonyme.Trim().Length < 3 || pseudonyme.Trim().Length > 50)) 
            {
                throw new ArgumentNullException("Le pseudonyme doit contenir au moins 3 caracteres et ne pas plus que 50 caracteres", nameof(pseudonyme));
                
            }

            if (string.IsNullOrWhiteSpace(email) || !MailAddress.TryCreate(email, out _))
            {
                throw new ArgumentException("L'email n'est pas valide", nameof(email));
            }

           

            Email = email;
            Pseudonyme = pseudonyme;
            AllowNewsletter = allowNewsletter;
            PasswordHash = passwordhash;
            Role = MemberRole.Peon  ;
        }

        public Member(long id, string email, string? pseudonyme, bool allowNewsletter, MemberRole role, string?passwordhash = null)
            :this(email, pseudonyme, allowNewsletter, passwordhash)
        {
            Id = id;
        }
    }

   



}
