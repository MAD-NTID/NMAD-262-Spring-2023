using System.Reflection.Metadata;
using System.Text.Json;
using RestAPIMVC.Services;

namespace RestAPIMVC.Exceptions;

public class ExceptionHandlerMiddleware
{
    public readonly RequestDelegate next;
    private ILoggerManager logger;

    public ExceptionHandlerMiddleware(ILoggerManager logger, RequestDelegate next)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            //handle the exception here
            await HandleExceptionAsync(context, e);
        }
    }


    public async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        //we assume it is a server/system exception
        int status = 500;
        string message = "An internal error occurred while processing your request";


        if (exception is IUserErrorException)
        {
            UserExceptionErrorException error = (UserExceptionErrorException)exception;
            status = error.GetStatusCode();
            message = error.GetMessage();
        }

        context.Response.ContentType = "application/json";
        var errorDetail = new
        {
            Message = message,
            StatusCode = status
        };

        //anything within the 500 range is a critical error and we want to know about it
        if (status >= 500)
        {
            string error = exception.Message + "-" + exception.StackTrace.ToString();
            this.logger.Error(error);
        }
        
        
        
        await context.Response.WriteAsync(JsonSerializer.Serialize(errorDetail));
    }
    
}