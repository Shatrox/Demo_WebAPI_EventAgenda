using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Services;
using Demo_WebAPI_EventAgenda.Domain.Models;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Mappers;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Request;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo_WebAPI_EventAgenda.Presentation.WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            return Ok(_faqService.GetAll().Select(FaqMapper.ToResponseDto));
        }


        [HttpGet("search")]
        public IActionResult GetSearch([FromQuery] string[] terms)
        {
            return Ok(_faqService.GetBySearch(terms).Select(FaqMapper.ToResponseDto));
        }



        [HttpPost]
        [Authorize(Roles = "Admin")]
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

        

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateElement([FromRoute] long id, [FromQuery] FaqRequestPatchDto dto)
        {
            if (dto.Visibility is not null)
            {
                _faqService.UpdateVisibility(id, (bool)dto.Visibility);
            }
            return Accepted();
        }

        [HttpPost("{id}/like")]
        public IActionResult LikeElement(long id)
        {
            _faqService.AddLike(id);
            return Accepted();
        }





    }
}
