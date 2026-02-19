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
            return new Member(result.Id, result.Email, result.Pseudonyme, result.AllowNewsletter, result.Role);
            
        }

        public Member GetByEmail(string email)
        {
            var result = _DbContext.Members
                .Single(m => m.Email == email); // this line translates word by word in: "Get the single member m where m.Email is equal to the email parameter"
            return new Member(result.Id, result.Email, result.Pseudonyme, result.AllowNewsletter, result.Role);
        }

        public string? GetPasswordHashByEmail(string email)
        { 
            return _DbContext.Members
                .Single(m => m.Email == email)? // this line translates word by word in: "Get the single member m where m.Email is equal to the email parameter"
                .PasswordHash;

        }
    }
}
