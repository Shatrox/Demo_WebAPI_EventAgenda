using System.ComponentModel.DataAnnotations;

namespace Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Request
{
    public class FaqRequestDto
    {
        // Data Notation

        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public required string Question { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public required string Answer { get; set; }
        
    }
}
