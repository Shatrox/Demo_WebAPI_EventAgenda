using Demo_WebAPI_EventAgenda.Domain.BusinessExceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Demo_WebAPI_EventAgenda.Presentation.WebAPI.ExceptionHandlers
{
    public class AgendaEventExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            // Check if the exception is not of type AgendaEventException
            if (exception is not AgendaEventException) 
               return false;

            int statusCode = (exception is AgendaNotFoundException) ? StatusCodes.Status404NotFound
                : (exception is AgendaEventCreateException) ? StatusCodes.Status422UnprocessableEntity
                : StatusCodes.Status400BadRequest;

            // Create a response object containing the error message and the agenda event data
            ProblemDetails problem = new ProblemDetails()
            {
                Title = "AgendaEvent error!",
                Detail = exception.Message,
                Status = statusCode


            };

            // Cloture de la requette
            // - Definition du Status de la reponse 
            httpContext.Response.StatusCode = statusCode;
            // - Envoyer la reponse
            await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);

            // Bool pour indicater que 
            return true;
        }
    }
}
