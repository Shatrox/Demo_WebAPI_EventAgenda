using Demo_WebAPI_EventAgenda.Domain.Models;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Request;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Response;

namespace Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Mappers
{
    public static class FaqMapper
    {
        public static FaqResponseDto ToResponseDto(this FAQ data)
            {
                return new FaqResponseDto()
                {
                    Id = data.Id,
                    Question = data.Question,
                    Answer = data.Answer,
                    IsHidden =  !data.IsVisible,
                    NbLikes = data.NbLikes,


                };
        }

        public static FAQ ToDomain(this FaqRequestDto data)
        {
            return new FAQ(data.Question, data.Answer);
        }
    }
}
