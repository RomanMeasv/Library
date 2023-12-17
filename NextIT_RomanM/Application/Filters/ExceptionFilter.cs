using Microsoft.AspNetCore.Mvc.Filters;
using NextIT_RomanM.Core.Domain.Exceptions;
using NextIT_RomanM.Core.Domain.Responses;
using System.Diagnostics;

namespace NextIT_RomanM.Application.Filters
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            context.ExceptionHandled = true;
            const string baseErrorMessage = "Something went wrong.";
            var trace = Activity.Current?.Id ?? context?.HttpContext.TraceIdentifier;
            var exception = context!.Exception;

            string errorCode;
            int statusCode;
            string errorMessage;

            if (exception is AppException appException)
            {
                switch(appException)
                {
                    case BookNotFoundException bookNotFoundException:
                        errorCode = "NotFound";
                        statusCode = StatusCodes.Status404NotFound;
                        errorMessage = bookNotFoundException.Message;
                        break;
                    case LoginException loginException:
                        errorCode = "Wrong username or password.";
                        statusCode = StatusCodes.Status400BadRequest; 
                        errorMessage = loginException.Message; 
                        break;
                    default:
                        errorCode = "AppExceptionNotHandled";
                        statusCode = StatusCodes.Status500InternalServerError;
                        errorMessage = "AppException not handled in exception filter";
                        break;
                }
            } else
            {
                errorCode = string.Empty;
                statusCode = StatusCodes.Status500InternalServerError;
                errorMessage = baseErrorMessage;
            }

            context.HttpContext.Response.StatusCode = statusCode;
            await context.HttpContext.Response.WriteAsJsonAsync(new ErrorResponse(errorCode, statusCode, trace, errorMessage));
        }
    }
}
