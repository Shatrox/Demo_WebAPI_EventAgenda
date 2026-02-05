using Demo_WebAPI_EventAgenda.Domain.Models;

namespace Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Response
{
    // DTO (Data Transfer Object) to transfer only one element
    public class AgendaEventResponseDto
    {
        public required long Id { get; set; }
        public required string Name { get; set; }
        public required string? Desc { get; set; }
        public required string? Location { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime? EndDate { get; set; }
        public required string Category { get; set; }
    }

    // DTO to transfer a list of elements
    public class AgendaEventListResponseDto
    {
        public required long Id { get; set; }
        public required string Name { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime? EndDate { get; set; }
    }
}
