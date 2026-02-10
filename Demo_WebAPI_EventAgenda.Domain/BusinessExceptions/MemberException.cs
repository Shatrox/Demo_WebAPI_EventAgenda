using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_WebAPI_EventAgenda.Domain.BusinessExceptions
{
    public class MemberException : Exception
    {
        public MemberException(string? message) : base(message){ }
    }

    public class MemberBadCredentialException : MemberException
    {
        public MemberBadCredentialException() : base("message")
        {
        }
    }
}
