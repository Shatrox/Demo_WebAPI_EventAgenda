using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Repositories;
using Demo_WebAPI_EventAgenda.Domain.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_WebAPI_EventAgenda.Infrastructure.Database.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly AppDbContext _DbContext;

        public MemberRepository(AppDbContext dbcontext)
        {
            _DbContext = dbcontext;
        }

        public Member CreateMember(Member data)
        {
            // Addition of the new element in the DbSet
            EntityEntry<Member> element = _DbContext.Members.Add(data);

            // Apllication of the changes in the database
            _DbContext.SaveChanges();

            // Return the created element
            var result = element.Entity;
            return new Member(result.Email, result.Pseudonyme, result.AllowNewsletter);
            
        }

        public string? GetPasswordHashByEmail(string email)
        { 
            return _DbContext.Members
                .Single(m => m.Email == email)?
                .PasswordHash;

        }
    }
}
