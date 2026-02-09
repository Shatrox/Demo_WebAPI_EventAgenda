using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_WebAPI_EventAgenda.Domain.Models
{
    public class FAQ
    {
        public int Id { get; private set; }
        public string Question { get; private set; }
        public string Answer { get; private set; }
        public bool IsVisible { get; private set; }


        public FAQ() { }

        public FAQ(string question, string answer, bool isVisible)
        {
            if (string.IsNullOrWhiteSpace(question))
            {
                throw new ArgumentException("La question ne peut pas etre vide", nameof(question));
            }
            if (string.IsNullOrWhiteSpace(answer))
            {
                throw new ArgumentException("La reponse ne peut pas etre vide", nameof(answer));
            }

            Question = question;
            Answer = answer;
            IsVisible = isVisible;
        }
    }
}
