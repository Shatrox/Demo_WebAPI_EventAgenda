using Demo_WebAPI_EventAgenda.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Repositories
{
    public interface IMemberRepository
    {
        // Create
        Member CreateMember (Member data);
        Member GetByEmail(string email);

        // Allow to get password by email
        string? GetPasswordHashByEmail(string email); 




    }
}
