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


        [HttpGet("search")]
        public IActionResult GetSearch([FromQuery] string[] terms)
        {
            return Ok(_faqService.GetBySearch(terms).Select(FaqMapper.ToResponseDto));
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

        [HttpPatch("{id}/show")]
        public IActionResult ShowElement(long id)
        {
            _faqService.UpdateVisibility(id, true);
            return Accepted();
        }

        [HttpPatch("{id}/hide")]
        public IActionResult HideElement(long id)
        {
            _faqService.UpdateVisibility(id, false);
            return Accepted();
        }

        [HttpPatch("{id}/like")]
        public IActionResult LikeElement(long id)
        {
            _faqService.AddLike(id);
            return Accepted();
        }





    }
}
