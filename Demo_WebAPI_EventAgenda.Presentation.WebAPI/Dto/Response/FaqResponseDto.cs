using Demo_WebAPI_EventAgenda.Domain.Models;
namespace Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Response
{
    // DTO (Data Transfer Object) to transfer only one element
    public class FaqResponseDto
    {
        public required long Id { get; set; }
        public required string Question { get; set; }
        public required string Answer { get; set; }
        public required bool IsVisible { get; set; }

    }
}
