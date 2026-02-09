using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Services;
using Demo_WebAPI_EventAgenda.Domain.Models;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Mappers;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Request;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Response;
using Microsoft.AspNetCore.Mvc;

namespace Demo_WebAPI_EventAgenda.Presentation.WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaqController : ControllerBase 
    {
        private readonly IFaqService _faqService;

        public FaqController(IFaqService faqService)
        { 
            _faqService = faqService;
       
        }

        // Define endpoint to get an event from the DB by id
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] long id) 
        { 
            // Recuperation of data from the Service "ApplicationCore"
            FAQ result = _faqService.GetById(id);

            FaqResponseDto dto = result.ToResponseDto();

            return Ok(dto); 

        }

        [HttpPost]
        public IActionResult AddElement(FaqRequestDto data)
        { 
            FAQ faq = data.ToDomain();

            FAQ result = _faqService.CreateFAQ(faq);

            return CreatedAtAction(
                nameof(GetById), 
                new { id = result.Id }, 
                result
            );
        }

        
        
        



    }
}
