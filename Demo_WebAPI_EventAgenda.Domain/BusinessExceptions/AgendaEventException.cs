using Demo_WebAPI_EventAgenda.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_WebAPI_EventAgenda.Domain.BusinessExceptions
{
    // Custom exception for AgendaEvent
    public class AgendaEventException : Exception
    {
        // Event Data related to the exception
        public AgendaEvent? AgendaEventData { get; set; }

        // Constructor with message and optional event data
        public AgendaEventException(string message, AgendaEvent? data = null) 
            : base(message) 
        { 
            AgendaEventData = data;
        }

    }

    // Specialized for error when trying to create an event
    public class AgendaEventCreateException : AgendaEventException
    {
        private const string INNER_MESSAGE = "Error while creating the event";
        public AgendaEventCreateException(AgendaEvent data)  
            : base(INNER_MESSAGE, data) { }

        
    }

    public class AgendaNotFoundException : AgendaEventException // Exception for the DELETE method when the event is not found
    {
        public AgendaNotFoundException()  
            : base("The requested event was not found.") { }
    }
}
