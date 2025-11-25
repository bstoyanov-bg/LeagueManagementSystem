using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

public class GlobalExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(HttpActionExecutedContext ctx)
    {
        HttpStatusCode status;

        if (ctx.Exception is ArgumentNullException)
            status = HttpStatusCode.BadRequest;

        else if (ctx.Exception is KeyNotFoundException)
            status = HttpStatusCode.NotFound;

        else if (ctx.Exception is UnauthorizedAccessException)
            status = HttpStatusCode.Unauthorized;

        else
            status = HttpStatusCode.InternalServerError;

        ctx.Response = ctx.Request.CreateResponse(
            status,
            new ErrorResponse
            {
                Message = ctx.Exception.Message,
                Details = ctx.Exception.GetType().Name,
                Timestamp = DateTime.UtcNow,
                Path = ctx.Request.RequestUri.AbsolutePath
            });
    }
}

public class ErrorResponse
{
    public string Message { get; set; }
    public string Details { get; set; }
    public DateTime Timestamp { get; set; }
    public string Path { get; set; }
}
