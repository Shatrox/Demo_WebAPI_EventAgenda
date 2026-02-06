using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Repositories;
using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Services;
using Demo_WebAPI_EventAgenda.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_WebAPI_EventAgenda.ApplicationCore.Services
{
    public class FaqService : IFaqService
    {
        private readonly IFaqRepository _faqRepository;

        public FaqService(IFaqRepository faqRepository)
        {
            _faqRepository = faqRepository;
        }

        public FAQ CreateFAQ(FAQ data)
        {
            // Here you can add any business logic or validation before creating the FAQ
            return _faqRepository.CreateFAQ(data);
        }

        public FAQ GetById(long id)
        {
            // Here you can add any business logic or validation before retrieving the FAQ
            return _faqRepository.GetById(id);
        }

    }
}
