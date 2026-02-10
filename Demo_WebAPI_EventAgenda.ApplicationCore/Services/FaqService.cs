using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Repositories;
using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Services;
using Demo_WebAPI_EventAgenda.Domain.BusinessExceptions;
using Demo_WebAPI_EventAgenda.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_WebAPI_EventAgenda.ApplicationCore.Services
{
    public class FaqService : IFaqService
    {
        private readonly IFaqRepository _faqRepository;

        // Dependency injection of the repository
        public FaqService(IFaqRepository faqRepository)
        {
            _faqRepository = faqRepository;
        }

        public void AddLike(long id)
        {
            FAQ? faq = _faqRepository.GetById(id);

            if (faq is null)
                throw new FaqNotFoundException();

            faq.IncrLike();
            _faqRepository.UpdateFAQ(faq);
        }

        public FAQ CreateFAQ(FAQ data)
        {
            // Here you can add any business logic or validation before creating the FAQ
            return _faqRepository.CreateFAQ(data);
        }

        public IEnumerable<FAQ> GetAll(bool includesHidden = false)
        {
            return _faqRepository.Get(includesHidden, []);
        }

        public FAQ GetById(long id)
        {
            // Here you can add any business logic or validation before retrieving the FAQ
            FAQ data = _faqRepository.GetById(id);

            if (data is null)
            {
                throw new Exception($"FAQ with ID {id} not found.");
            }

            return data;
        }

        public IEnumerable<FAQ> GetBySearch(IEnumerable<string> terms, bool includesHidden = false)
        {
            if (terms.Count() == 0)
            {
                throw new ArgumentOutOfRangeException("La recherche doit contenir au moins un terme");
            }

            return _faqRepository.Get(includesHidden, terms);
        }

        public void UpdateVisibility(long id, bool visible)
        {
            FAQ? faq = _faqRepository.GetById(id);

            if (faq is null)
                throw new FaqNotFoundException();

            if (faq.IsVisible == visible)
                throw new FaqUpdateException($"La FAQ est déjà {(visible ? "visible" : "masqué")}");

            faq.ChangeVisibility(visible);
            _faqRepository.UpdateFAQ(faq);
        }
    }
}
