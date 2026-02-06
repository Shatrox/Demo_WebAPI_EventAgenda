using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Repositories;
using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Services;
using Demo_WebAPI_EventAgenda.Domain.BusinessExceptions;
using Demo_WebAPI_EventAgenda.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_WebAPI_EventAgenda.ApplicationCore.Services
{
    public class AgendaEventService : IAgendaEventService
    {
        private IAgendaEventRepository _agendaEventRepository; 

        //Dependencie injection 
        public AgendaEventService(IAgendaEventRepository agendaEventRepository)
        {
            _agendaEventRepository = agendaEventRepository;
        }

        public AgendaEvent Create(AgendaEvent data)
        {
            if (data.StartDate < DateTime.Today.AddDays(1)) // this line translate word by word in: if the date of the event is smaller then the date of tomorrow
            {
                // Generate an exception connected to the rules of your project
                throw new AgendaEventCreateException(data);
            }

            return _agendaEventRepository.Insert(data);
        }

        public void Delete(long id)
        {
            bool isDeleted = _agendaEventRepository.Delete(id);
            if (!isDeleted)
            {
                throw new AgendaNotFoundException();
            }
        }

        public AgendaEvent GetById(long id)
        {
            AgendaEvent? data = _agendaEventRepository.GetById(id);

            if(data is null)
            {
                throw new AgendaNotFoundException();
            }

            return data;

            // return _agendaEventRepository.GetById(id) ?? throw new AgendaNotFoundException();
        }

        public IEnumerable<AgendaEvent> GetAllByDate(DateTime date)
        {
            return _agendaEventRepository.GetByDate(date);
            //throw new NotImplementedException();
        }

        public IEnumerable<AgendaEvent> GetAllByDateRange(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                throw new ArgumentOutOfRangeException("The start date must be smaller than the end date");
            }

            return _agendaEventRepository.GetByDate(startDate, endDate);
            //throw new NotImplementedException();
        }

        public void UpdateDate(long id, DateTime startDate, DateTime? endDate)
        {
            // Retrieve the agenda event from the repository
            AgendaEvent? agendaEvent = _agendaEventRepository.GetById(id);
            if (agendaEvent is null)
            {
                throw new AgendaNotFoundException();
            }
            // Modify the date of the agenda event via a method of the domain model
            agendaEvent.ChangeDate(startDate, endDate);
            //Repercute the update in the repository
            _agendaEventRepository.Update(agendaEvent);
        }

        public IEnumerable<AgendaEvent> GetMany(int page, int nbElement)
        {
            if (page <= 0 || nbElement <= 0)
            {
                throw new ArgumentOutOfRangeException("The number of pages and elements most be superior to 0");
            }


            int offset = (page - 1) * nbElement;
            int limit = nbElement;



            return _agendaEventRepository.GetAll(offset, limit);
        }

        // FAQ Management Methods

      
    }
}
