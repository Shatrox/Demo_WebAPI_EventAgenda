using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Services;
using Demo_WebAPI_EventAgenda.Domain.Models;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Mappers;
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
        [HttpGet("{id}")]
        [ProducesResponseType<AgendaEventResponseDto>(200)]
        public IActionResult GetById([FromRoute]long id)
        {
            // Recuperation des donnees depuis le service "ApplicationCore"
            AgendaEvent result = _agendaEventService.GetById(id);

            // TODO: Don't send the object directly, use a DTO (ResponseDTO) instead
            AgendaEventResponseDto dto = result.ToResponseDto(); // Using the extension method ToResponseDto (Mapper)

            // Renvoi la response sous forme d'un DTO
            return Ok(dto);
        }

        // Define an endpoint to add a new event to the DB

        [HttpPost]
        [ProducesResponseType<AgendaEventResponseDto>(201)]
        public IActionResult AddElement(AgendaEventRequestDto data) 
        {
            // Transforme data "RequestDto" vers le type model (Domain)
            AgendaEvent agendaEvent = data.ToDomain(); //Using the extension method ToDomain (Mapper)
            // Utilisation of the service (ApplicationCore) to add data
            AgendaEvent result = _agendaEventService.Create(agendaEvent);

            AgendaEventResponseDto dto = result.ToResponseDto(); //Using the extension method ToResponseDto (Mapper)

            return CreatedAtAction(             // Creation the response 201 "CREATED" 
                  nameof(GetById),              // → Endpoit to retrieve the data
                  new { id = result.Id },       // → Necessary data for the endpoint
                  dto                        // → Data of the created object 
                
                
            );


        
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public IActionResult Delete(int id)
        {
            _agendaEventService.Delete(id);
            
            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType<IEnumerable<AgendaEventListResponseDto>>(200)]
        public IActionResult GetAll([FromQuery] int page = 1, [FromQuery] int nbElement = 10 ) // You can give a value by default to page and nbElement 
        {
            IEnumerable<AgendaEvent> result = _agendaEventService.GetMany(page, nbElement);

            IEnumerable<AgendaEventListResponseDto> dtoList = result.Select(item => item.ToListResponseDto()); // Using the extension method ToListResponseDto (Mapper)

            return Ok(dtoList);
        }

        [HttpGet("date/{startDate}")]
        [ProducesResponseType<IEnumerable<AgendaEventListResponseDto>>(200)]
        public IActionResult GetByDate([FromRoute] DateTime startDate)
        {
            IEnumerable<AgendaEvent> result = _agendaEventService.GetAllByDate(startDate);

            IEnumerable<AgendaEventListResponseDto> dtoList = result.Select(AgendaEventMapper.ToListResponseDto); // Using the extension method ToListResponseDto (Mapper)

            return Ok(dtoList);
        }

        [HttpGet("date/{startDate}/to/{endDate}")]
        [ProducesResponseType<IEnumerable<AgendaEventListResponseDto>>(200)]
        public IActionResult GetByDate([FromRoute] DateTime startDate, [FromRoute] DateTime endDate)
        {
            IEnumerable<AgendaEvent> result = _agendaEventService.GetAllByDateRange(startDate, endDate);

            IEnumerable<AgendaEventListResponseDto> dtoList = result.Select(AgendaEventMapper.ToListResponseDto); // Using the extension method ToListResponseDto (Mapper)

            return Ok(dtoList); 
        }
        

    }
}
