using Demo_WebAPI_EventAgenda.Domain.Models;
using System.Diagnostics;
using System.Text.Json.Serialization;
namespace Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Response
{
    // DTO (Data Transfer Object) to transfer only one element
    public class FaqResponseDto
    {
        public required long Id { get; set; }
        public required string Question { get; set; }
        public required string Answer { get; set; }
        public required int NbLikes { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public required bool IsHidden { get; set; }

    }
}
