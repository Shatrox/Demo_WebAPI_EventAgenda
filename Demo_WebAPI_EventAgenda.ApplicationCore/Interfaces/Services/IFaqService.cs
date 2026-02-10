using Demo_WebAPI_EventAgenda.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Services
{
    public interface IFaqService
    {
        public FAQ GetById(long id); // Read
        public FAQ CreateFAQ(FAQ data); // This creates a new FAQ entry
        public IEnumerable<FAQ> GetAll(bool includesHidden = false);
        public IEnumerable<FAQ> GetBySearch(IEnumerable<string> terms, bool includesHidden = false);
        public void UpdateVisibility(long id, bool visible);
        public void AddLike(long id);

    }
}
