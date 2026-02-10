using Demo_WebAPI_EventAgenda.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Services
{
    public interface IMemberService
    {
        Member Login(string email, string password); // Create Login
        Member Register(Member member); // Create a Member
    }
}
