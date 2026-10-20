using CleanArchitecture.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        // Representa al mensaje que esta ingresando al contexto de mi programa
        // Si ocurre algun error va a disparar una excepcion
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /**
         *  Metodo que evalua si sucede algun tipo de excepcion en el transcurso
         *  en el que el request pasa por validaciones o por diferentes contextos
         *  dento de la aplicación.
         */
        public async Task InvokeAsync(HttpContext context) 
        {
            try
            {
                // Si no hay excepcion que avance a los siguientes niveles
                await _next(context);
            } catch(Exception exception)
            {
                _logger.LogError(exception, "Ocurrio una exception: {Message}", exception.Message);
                var exceptionDetails = GetExceptionDetails(exception);
                var problemDetails = new ProblemDetails
                {
                    Status = exceptionDetails.Status,
                    Type = exceptionDetails.Type,
                    Title = exceptionDetails.Title,
                    Detail = exceptionDetails.Detail
                };

                if (exceptionDetails.Errors is not null)
                {
                    problemDetails.Extensions["errors"] = exceptionDetails.Errors;
                }

                context.Response.StatusCode = exceptionDetails.Status;

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }

        private static ExceptionDetails GetExceptionDetails(Exception exception)
        {
            return exception switch
            {
                ValidationException validationException => new ExceptionDetails(
                    StatusCodes.Status400BadRequest,
                    "ValidationFailure",
                    "Validacion de Error",
                    "Han ocurrido uno o mas errores de validación",
                    validationException.Errors),
                _ => new ExceptionDetails(
                    StatusCodes.Status500InternalServerError,
                    "ServerError",
                    "Error de Servidor",
                    "Un inesperado error a ocurrido en la App",
                    null)
            };
        }

        internal record ExceptionDetails(
            int Status,
            string Type,
            string Title,
            string Detail,
            IEnumerable<object> Errors);
    }
}
