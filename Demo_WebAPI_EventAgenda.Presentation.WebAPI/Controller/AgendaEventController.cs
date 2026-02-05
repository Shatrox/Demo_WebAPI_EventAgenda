using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Services;
using Demo_WebAPI_EventAgenda.Domain.Models;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Request;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo_WebAPI_EventAgenda.Presentation.WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaEventController : ControllerBase
    {
        // Dependency injection of the service (application core)
        private readonly IAgendaEventService _agendaEventService;

        //Inject the service via the constructor
        public AgendaEventController(IAgendaEventService agendaEventService)
        {
            _agendaEventService = agendaEventService;
        }

        // Define an endpoint to get an event from the DB by id
        [HttpGet("{:id}")]
        public IActionResult GetById([FromRoute]long id)
        {
            AgendaEvent result = _agendaEventService.GetById(id);

            // TODO: Don't send the object directly, use a DTO (ResponseDTO) instead
            AgendaEventResponseDto dto = new AgendaEventResponseDto()
            {
                Id = result.Id,
                Name = result.Name,
                Desc = result.Desc,
                Location = result.Location,
                StartDate = result.StartDate,
                EndDate = result.EndDate,
                Category = result.Category.Name
            };

            return Ok(dto);
        }

        // Define an endpoint to add a new event to the DB

        [HttpPost]

        public IActionResult AddElement(AgendaEventRequestDto data) 
        {
            // Transforme data "RequestDto" vers le type model (Domain)
            AgendaEvent agendaEvent = new AgendaEvent(
                data.Name,
                data.Desc,
                data.Location,
                data.StartDate,
                data.EndDate,
                new EventCategory(data.Category)
            );
            // Utilisation of the service (ApplicationCore) to add data
            AgendaEvent result = _agendaEventService.Create(agendaEvent);

            AgendaEventResponseDto dto = new AgendaEventResponseDto()
            {
                Id = result.Id,
                Name = result.Name,
                Desc = result.Desc,
                Location = result.Location,
                StartDate = result.StartDate,
                EndDate = result.EndDate,
                Category = result.Category.Name
            };

            return CreatedAtAction(             // Creation the response 201 "CREATED" 
                  nameof(GetById),              // → Endpoit to retrieve the data
                  new { Id = result.Id },       // → Necessary data for the endpoint
                  dto                        // → Data of the created object 
                
                
            );
        
        }
    }
}
