using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Repositories;
using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Services;
using Demo_WebAPI_EventAgenda.Domain.Models;
using Soenneker.Hashing.Argon2;
using System;
using System.Collections.Generic;
using System.Text;
using Demo_WebAPI_EventAgenda.Domain.BusinessExceptions;

namespace Demo_WebAPI_EventAgenda.ApplicationCore.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository) 
        { 
            _memberRepository = memberRepository;
        }

        public Member Register(Member member)
        {
            if (string.IsNullOrEmpty(member.PasswordHash))
            { 
            
                throw new ArgumentNullException("Password is required to register a member.");


            }
            // Hash the password before saving the account
            // (The Method is async, to simplify the code, we will wait for the result synchronously, but in a real application, you should use async/await)
            string hashPwd = Argon2HashingUtil.Hash(member.PasswordHash).Result;

            // Recreate the member with the hashed password
            Member memberToInsert = new Member(
                member.Email,
                member.Pseudonyme,
                member.AllowNewsletter,
                hashPwd
                );

            

            // Create the account on the DB via the repository
            return _memberRepository.CreateMember(memberToInsert);
        }



        public Member Login(string email, string password)
        {
            string? hashPwd = _memberRepository.GetPasswordHashByEmail(email);
            if (hashPwd is null)
            {
                // The account does not exist
                throw new MemberBadCredentialException();
            }

            bool isValid = Argon2HashingUtil.Verify(password, hashPwd).Result;
            if (!isValid)
            {
                // The password is incorrect
                throw new MemberBadCredentialException();
            }

            return _memberRepository.GetByEmail(email);



        }
    }
}                       

  
