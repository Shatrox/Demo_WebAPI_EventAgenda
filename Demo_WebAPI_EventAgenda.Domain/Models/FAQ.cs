using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_WebAPI_EventAgenda.Domain.Models
{
    public class FAQ
    {
        public int Id { get; private set; }
        public string Question { get; private set; } = default!; // default! = Non nullable, mais pas de valeur par defaut -> necessaire pour EntityFramework
        public string Answer { get; private set; } = default!; // default! = Non nullable, mais pas de valeur par defaut -> necessaire pour EntityFramework
        public bool IsVisible { get; private set; }
        public int NbLikes { get; private set; }


        private FAQ() { }

        public FAQ(string question, string answer, bool isVisible = true)
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
            NbLikes = 0;
        }

        public FAQ ChangeVisibility(bool visible)
        {
            IsVisible = visible;
            return this;
        }

        public FAQ IncrLike()
        {
            NbLikes++;
            return this;
        }
    }
}
