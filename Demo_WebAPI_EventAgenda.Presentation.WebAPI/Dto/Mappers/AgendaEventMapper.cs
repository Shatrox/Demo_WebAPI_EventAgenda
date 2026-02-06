using Demo_WebAPI_EventAgenda.Domain.Models;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Request;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Response;
using System.Runtime.CompilerServices;

namespace Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Mappers
{
    // ↓ Classe qui contiendra des methodes d'extension pour realiser le mapping
    public static class AgendaEventMapper
    {
        // Mapper pour convertir le modele (Domain) vers le "Dto" (Presentation)
        public static AgendaEventResponseDto ToResponseDto(this AgendaEvent data) 
        {

            return new AgendaEventResponseDto()
            {
                Id = data.Id,
                Name = data.Name,
                Desc = data.Desc,
                Location = data.Location,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                Category = data.Category.Name
            };



        }

        public static AgendaEventListResponseDto ToListResponseDto(this AgendaEvent data) 
        {
            return new AgendaEventListResponseDto()
            {
                Id = data.Id,
                Name = data.Name,
                StartDate = data.StartDate,
                EndDate = data.EndDate
            };
        }


        // Mapper pour convertir le "RequestDto" (Presentation vers le modele (Domain)
        public static AgendaEvent ToDomain(this AgendaEventRequestDto dto) 
        {
            return new AgendaEvent
            (
                dto.Name,
                dto.Desc,
                dto.Location,
                dto.StartDate,
                dto.EndDate,
                new EventCategory(dto.Category)

            );
        }
    }
}
