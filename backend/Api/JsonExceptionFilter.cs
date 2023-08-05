using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class JsonExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        dynamic result;
        var exception = context.Exception.InnerException ?? context.Exception;
        if (exception is FluentValidation.ValidationException)
        {
            result = new ObjectResult(new
            {
                code = 400,

                message = exception.Message,
            });
            result.StatusCode = 400;

        }
        else
        {
            result = new ObjectResult(new
            {
                code = 500,
                message = "A server error occurred.",
                detailedMessage = context.Exception.Message
            });
            result.StatusCode = 500;
        }


        context.Result = result;
    }
}
