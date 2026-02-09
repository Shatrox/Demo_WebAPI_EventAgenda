using System.ComponentModel.DataAnnotations;

namespace Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Request
{
    public class FaqRequestDto
    {
        // Data Notation

        [Required]
        public required long Id { get; set; }
        [Required]          
        public required string Question { get; set; }
        [Required]
        public required string Answer { get; set; }
        public required bool IsVisible { get; set; }
    }
}
